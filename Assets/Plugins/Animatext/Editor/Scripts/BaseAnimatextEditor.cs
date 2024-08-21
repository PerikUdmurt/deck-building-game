// Copyright (C) 2020 - 2024 Seeley Studio. All Rights Reserved.

#pragma warning disable IDE0017, IDE0090

using Animatext.Effects;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Animatext.Editor
{
    [CustomEditor(typeof(BaseAnimatext), true)]
    public class BaseAnimatextEditor : UnityEditor.Editor
    {
        private static readonly GUIContent _contentAdd = new GUIContent("Add");
        private static readonly GUIContent _contentDelete = new GUIContent("Delete");
        private static readonly GUIContent _contentMoveUp = new GUIContent("Move Up");
        private static readonly GUIContent _contentMoveDown = new GUIContent("Move Down");
        private static readonly GUIContent _contentAutoEnd = new GUIContent("Auto End", "Ends automatically when the effect is completed to play in the play state.");
        private static readonly GUIContent _contentAutoPlay = new GUIContent("Auto Play", "Plays automatically when the effect is in the start state.");
        private static readonly GUIContent _contentAutoStart = new GUIContent("Auto Start", "Starts automatically when the effect is updated or refreshed in the stop state.");
        private static readonly GUIContent _contentAutoStop = new GUIContent("Auto Stop", "Stops automatically when the effect is in the end state.");
        private static readonly GUIContent _contentRefreshMode = new GUIContent("Refresh Mode", "Refresh mode determines the state of the effect after refreshed.");

        private bool _settingsOn;
        private BaseAnimatext _target;
        private SerializedProperty _effectsProperty;
        private SerializedProperty _settingsProperty;
        private EffectInspectorObject _effectInspectorObject;
        private List<EffectPropertyInfo> _effectPropertyInfoList;

        protected virtual void OnEnable()
        {
            _target = target as BaseAnimatext;

            _effectsProperty = serializedObject.FindProperty("_effects");
            _settingsProperty = serializedObject.FindProperty("_settings");

            _effectInspectorObject = CreateInstance<EffectInspectorObject>();
            _effectPropertyInfoList = new List<EffectPropertyInfo>();

            if (!EditorDrawer.editorStyle)
            {
                EditorDrawer.Init();
            }

            Undo.undoRedoPerformed += UndoRedoPerformed;
        }

        protected virtual void OnDisable()
        {
            Undo.undoRedoPerformed -= UndoRedoPerformed;
        }

        protected virtual void OnChildSettingsGUI() { }

        public override void OnInspectorGUI()
        {
            if (!EditorDrawer.editorStyle)
            {
                EditorDrawer.Init();
            }

            serializedObject.Update();

            GUILayout.Space(10);

            EditorDrawer.DrawBanner();

            GUILayout.Space(10);

            DrawMenu();

            GUILayout.Space(10);

            if (CheckInspectorInfoDirty())
            {
                UpdateInspectorInfo();

                UpdatePropertyInfo();
            }

            else if (CheckPropertyInfoDirty())
            {
                UpdatePropertyInfo();
            }

            for (int i = 0; i < _effectsProperty.arraySize; i++)
            {
                DrawEffect(i);

                GUILayout.Space(10);
            }

            _effectInspectorObject.text = _target.inputText;

            serializedObject.ApplyModifiedProperties();
        }

        private void UndoRedoPerformed()
        {
            if (_target == null) return;

            serializedObject.Update();

            UpdatePropertyInfo();

            if (_target.enabled && _target.gameObject.activeInHierarchy)
            {
                _target.SendMessage("SetComponentDirty");
            }

            if (Application.isPlaying && _target.settings.retainedText != RetainedText.InputText)
            {
                if (_target.inputText != _effectInspectorObject.text && _effectInspectorObject.text != null)
                {
                    Debug.LogWarning("<Animatext> When the retained text is undone in editor playing mode, the text for effect execution is the text in the component prior to the undoing instead of the text input prior to the undoing.");
                }
            }
        }

        private void DrawMenu()
        {
            GUILayout.BeginHorizontal();
            {
                GUILayout.Space(-3);

                if (EditorDrawer.DrawNewEffectButton())
                {
                    AddEffect(0);
                }

                GUILayout.FlexibleSpace();

                if (EditorDrawer.DrawDocumentationButton(345))
                {
                    Application.OpenURL(EditorPath.documentationURL);
                }

                if (EditorDrawer.DrawSettingsButton())
                {
                    _settingsOn = !_settingsOn;
                }

                GUILayout.Space(-3);
            }
            GUILayout.EndHorizontal();

            if (_settingsOn)
            {
                if (EditorApplication.isPlaying)
                {
                    GUI.enabled = false;
                }

                GUILayout.Space(15);

                EditorDrawer.BeginDrawer();
                {
                    EditorGUILayout.PropertyField(_settingsProperty);

                    OnChildSettingsGUI();
                }
                EditorDrawer.EndDrawer();

                if (EditorApplication.isPlaying)
                {
                    GUI.enabled = true;
                }
            }
        }

        private void DrawEffect(int index)
        {
            EffectInspectorInfo effectInspectorInfo = _effectInspectorObject.list[index];
            EffectPropertyInfo effectPropertyInfo = _effectPropertyInfoList[index];

            string effectTitle = "Effect " + index;

            HashSet<string> tags = _target.effects[index].GetTags();

            foreach (var tag in tags)
            {
                if (!string.IsNullOrEmpty(tag))
                {
                    effectTitle += " - " + tag;
                }
            }

            GUILayout.BeginHorizontal();
            {
                if (EditorDrawer.DrawBar(effectTitle))
                {
                    effectInspectorInfo.isOn = !effectInspectorInfo.isOn;
                }

                Rect effectRect = EditorGUILayout.BeginVertical(GUILayout.Width(24), GUILayout.Height(24));
                {
                    if (EditorDrawer.DrawBarButton())
                    {
                        effectRect.x -= 134;
                        effectRect.y += 2;

                        GenericMenu menu = new GenericMenu();

                        menu.AddItem(_contentAdd, false, AddEffect, index + 1);
                        menu.AddItem(_contentDelete, false, DeleteEffect, index);
                        menu.AddItem(_contentMoveUp, false, MoveEffect, new Vector2Int(index, index - 1));
                        menu.AddItem(_contentMoveDown, false, MoveEffect, new Vector2Int(index, index + 1));

                        menu.DropDown(effectRect);

                        GUI.FocusControl(null);
                    }
                }
                EditorGUILayout.EndVertical();

                GUILayout.Space(-2);
            }
            GUILayout.EndHorizontal();

            if (effectInspectorInfo.isOn)
            {
                if (EditorApplication.isPlaying)
                {
                    GUI.enabled = false;
                }

                GUILayout.Space(10);

                EditorDrawer.BeginDrawer();
                {
                    GUILayout.BeginHorizontal();
                    {
                        EditorGUILayout.PropertyField(effectPropertyInfo.autoStart, _contentAutoStart);
                        EditorGUILayout.PropertyField(effectPropertyInfo.autoEnd, _contentAutoEnd);
                        
                    }
                    GUILayout.EndHorizontal();

                    GUILayout.BeginHorizontal();
                    {
                        EditorGUILayout.PropertyField(effectPropertyInfo.autoPlay, _contentAutoPlay);
                        EditorGUILayout.PropertyField(effectPropertyInfo.autoStop, _contentAutoStop);
                    }
                    GUILayout.EndHorizontal();

                    GUILayout.Space(5);

                    EditorGUILayout.PropertyField(effectPropertyInfo.refreshMode, _contentRefreshMode);

                    GUILayout.Space(5);

                    GUILayout.BeginHorizontal();
                    {
                        Rect presetRect = EditorGUILayout.BeginVertical(GUILayout.Width(20), GUILayout.Height(20));
                        {
                            if (EditorDrawer.DrawPresetButton())
                            {
                                presetRect.x += 2;
                                presetRect.y -= 2;

                                GenericMenu menu = new GenericMenu();

                                menu.AddItem(_contentAdd, false, AddPreset, new Vector2Int(index, effectInspectorInfo.currentIndex + 1));
                                menu.AddItem(_contentDelete, false, DeletePreset, new Vector2Int(index, effectInspectorInfo.currentIndex));
                                menu.AddItem(_contentMoveUp, false, MovePreset, new Vector3Int(index, effectInspectorInfo.currentIndex, effectInspectorInfo.currentIndex - 1));
                                menu.AddItem(_contentMoveDown, false, MovePreset, new Vector3Int(index, effectInspectorInfo.currentIndex, effectInspectorInfo.currentIndex + 1));

                                menu.DropDown(presetRect);

                                GUI.FocusControl(null);
                            }
                        }
                        EditorGUILayout.EndVertical();

                        GUILayout.Space(-3);

                        if (EditorApplication.isPlaying)
                        {
                            GUI.enabled = true;
                        }

                        int currentIndex = EditorDrawer.DrawPresetPopup(effectInspectorInfo.currentIndex, effectPropertyInfo.presets.arraySize);

                        if (currentIndex != effectInspectorInfo.currentIndex)
                        {
                            effectInspectorInfo.currentIndex = currentIndex;
                            effectPropertyInfo.currentDirty = true;
                        }

                        if (EditorApplication.isPlaying)
                        {
                            GUI.enabled = false;
                        }

                        if (effectPropertyInfo.currentDirty)
                        {
                            if (effectInspectorInfo.currentIndex < effectPropertyInfo.presets.arraySize && effectInspectorInfo.currentIndex >= 0)
                            {
                                effectPropertyInfo.currentPreset = effectPropertyInfo.presets.GetArrayElementAtIndex(effectInspectorInfo.currentIndex);
                            }
                            else
                            {
                                effectInspectorInfo.currentIndex = effectPropertyInfo.presets.arraySize - 1;

                                if (effectInspectorInfo.currentIndex >= 0)
                                {
                                    effectPropertyInfo.currentPreset = effectPropertyInfo.presets.GetArrayElementAtIndex(effectInspectorInfo.currentIndex);
                                }
                                else
                                {
                                    effectPropertyInfo.currentPreset = null;
                                }
                            }

                            effectPropertyInfo.currentDirty = false;
                        }

                        if (effectPropertyInfo.currentPreset != null)
                        {
                            EditorGUILayout.PropertyField(effectPropertyInfo.currentPreset, GUIContent.none);
                        }
                        else
                        {
                            EditorGUILayout.ObjectField(null, typeof(BaseEffect), false);
                        }
                    }
                    GUILayout.EndHorizontal();

                    if (effectPropertyInfo.currentPreset != null)
                    {
                        EditorDrawer.DrawPreset(effectPropertyInfo.currentPreset);
                    }
                }
                EditorDrawer.EndDrawer();

                if (EditorApplication.isPlaying)
                {
                    GUI.enabled = true;
                }
            }
        }

        private bool CheckInspectorInfoDirty()
        {
            bool isDirty = false;

            if (_target.effects.Count == _effectInspectorObject.list.Count)
            {
                for (int i = 0; i < _effectInspectorObject.list.Count; i++)
                {
                    if (_target.effects[i] != _effectInspectorObject.list[i].effect)
                    {
                        isDirty = true;
                        break;
                    }
                }
            }
            else
            {
                isDirty = true;
            }

            return isDirty;
        }

        private bool CheckPropertyInfoDirty()
        {
            return _effectInspectorObject.list.Count != _effectPropertyInfoList.Count;
        }

        private void UpdateInspectorInfo()
        {
            Dictionary<Effect, EffectInspectorInfo> dictionary = new Dictionary<Effect, EffectInspectorInfo>();

            for (int i = 0; i < _effectInspectorObject.list.Count; i++)
            {
                if (!dictionary.ContainsKey(_effectInspectorObject.list[i].effect))
                {
                    dictionary.Add(_effectInspectorObject.list[i].effect, _effectInspectorObject.list[i]);
                }
            }

            _effectInspectorObject.list.Clear();

            for (int i = 0; i < _target.effects.Count; i++)
            {
                if (dictionary.ContainsKey(_target.effects[i]))
                {
                    _effectInspectorObject.list.Add(dictionary[_target.effects[i]]);
                }
                else
                {
                    EffectInspectorInfo effectInspectorInfo = new EffectInspectorInfo();

                    effectInspectorInfo.isOn = false;
                    effectInspectorInfo.currentIndex = 0;
                    effectInspectorInfo.effect = _target.effects[i];

                    _effectInspectorObject.list.Add(effectInspectorInfo);
                }
            }
        }

        private void UpdatePropertyInfo()
        {
            if (_effectInspectorObject.list.Count < _effectPropertyInfoList.Count)
            {
                _effectPropertyInfoList.RemoveRange(_effectInspectorObject.list.Count, _effectPropertyInfoList.Count - _effectInspectorObject.list.Count);
            }
            else if(_effectInspectorObject.list.Count > _effectPropertyInfoList.Count)
            {
                for (int i = _effectPropertyInfoList.Count; i < _effectInspectorObject.list.Count; i++)
                {
                    EffectPropertyInfo effectPropertyInfo = new EffectPropertyInfo();

                    effectPropertyInfo.effect = _effectsProperty.GetArrayElementAtIndex(i);
                    effectPropertyInfo.presets = effectPropertyInfo.effect.FindPropertyRelative("_presets");
                    effectPropertyInfo.autoEnd = effectPropertyInfo.effect.FindPropertyRelative("_autoEnd");
                    effectPropertyInfo.autoStart = effectPropertyInfo.effect.FindPropertyRelative("_autoStart");
                    effectPropertyInfo.autoPlay = effectPropertyInfo.effect.FindPropertyRelative("_autoPlay");
                    effectPropertyInfo.autoStop = effectPropertyInfo.effect.FindPropertyRelative("_autoStop");
                    effectPropertyInfo.refreshMode = effectPropertyInfo.effect.FindPropertyRelative("_refreshMode");
                    effectPropertyInfo.currentDirty = true;
                    effectPropertyInfo.currentPreset = null;

                    _effectPropertyInfoList.Add(effectPropertyInfo);
                }
            }

            for (int i = 0; i < _effectPropertyInfoList.Count; i++)
            {
                _effectPropertyInfoList[i].currentDirty = true;
            }
        }

        private void AddEffect(object obj)
        {
            int index = (int)obj;

            Object[] objects = new Object[2];

            objects[0] = _target;
            objects[1] = _effectInspectorObject;

            Undo.RecordObjects(objects, "Add Effect");

            _target.effects.Insert(index, new Effect(new BaseEffect[1]));
            _effectsProperty.serializedObject.Update();

            for (int i = index; i < _effectPropertyInfoList.Count; i++)
            {
                _effectPropertyInfoList[i].currentDirty = true;
            }

            EffectPropertyInfo effectPropertyInfo = new EffectPropertyInfo();

            effectPropertyInfo.effect = _effectsProperty.GetArrayElementAtIndex(_effectsProperty.arraySize - 1);
            effectPropertyInfo.presets = effectPropertyInfo.effect.FindPropertyRelative("_presets");
            effectPropertyInfo.autoEnd = effectPropertyInfo.effect.FindPropertyRelative("_autoEnd");
            effectPropertyInfo.autoStart = effectPropertyInfo.effect.FindPropertyRelative("_autoStart");
            effectPropertyInfo.autoPlay = effectPropertyInfo.effect.FindPropertyRelative("_autoPlay");
            effectPropertyInfo.autoStop = effectPropertyInfo.effect.FindPropertyRelative("_autoStop");
            effectPropertyInfo.refreshMode = effectPropertyInfo.effect.FindPropertyRelative("_refreshMode");
            effectPropertyInfo.currentDirty = true;
            effectPropertyInfo.currentPreset = null;

            _effectPropertyInfoList.Add(effectPropertyInfo);

            EffectInspectorInfo effectInspectorInfo = new EffectInspectorInfo();

            effectInspectorInfo.isOn = true;
            effectInspectorInfo.currentIndex = 0;
            effectInspectorInfo.effect = _target.effects[index];

            _effectInspectorObject.list.Insert(index, effectInspectorInfo);

            EditorUtility.SetDirty(_target);
        }

        private void DeleteEffect(object obj)
        {
            int index = (int)obj;

            Object[] objects = new Object[2];

            objects[0] = _target;
            objects[1] = _effectInspectorObject;

            Undo.RecordObjects(objects, "Delete Effect");

            _target.effects.RemoveAt(index);

            _effectPropertyInfoList.RemoveAt(_effectPropertyInfoList.Count - 1);

            for (int i = index; i < _effectPropertyInfoList.Count; i++)
            {
                _effectPropertyInfoList[i].currentDirty = true;
            }

            _effectInspectorObject.list.RemoveAt(index);

            EditorUtility.SetDirty(_target);
        }

        private void MoveEffect(object obj)
        {
            Vector2Int vector2Int = (Vector2Int)obj;

            if (vector2Int.y < 0 || vector2Int.y >= _effectsProperty.arraySize)
            {
                return;
            }

            Object[] objects = new Object[2];

            objects[0] = _target;
            objects[1] = _effectInspectorObject;

            Undo.RecordObjects(objects, "Move Effect");

            Effect effect = _target.effects[vector2Int.x];

            _target.effects.RemoveAt(vector2Int.x);
            _target.effects.Insert(vector2Int.y, effect);

            if (vector2Int.x < vector2Int.y)
            {
                for (int i = vector2Int.x; i < vector2Int.y; i++)
                {
                    _effectPropertyInfoList[i].currentDirty = true;
                }
            }
            else if (vector2Int.x > vector2Int.y)
            {
                for (int i = vector2Int.x; i > vector2Int.y; i--)
                {
                    _effectPropertyInfoList[i].currentDirty = true;
                }
            }

            EffectInspectorInfo effectInspectorInfo = _effectInspectorObject.list[vector2Int.x];

            _effectInspectorObject.list.RemoveAt(vector2Int.x);
            _effectInspectorObject.list.Insert(vector2Int.y, effectInspectorInfo);

            EditorUtility.SetDirty(_target);
        }

        private void AddPreset(object obj)
        {
            Vector2Int vector2Int = (Vector2Int)obj;

            EffectPropertyInfo effectPropertyInfo = _effectPropertyInfoList[vector2Int.x];

            effectPropertyInfo.currentDirty = true;

            effectPropertyInfo.presets.arraySize++;
            effectPropertyInfo.presets.GetArrayElementAtIndex(effectPropertyInfo.presets.arraySize - 1).objectReferenceValue = null;
            effectPropertyInfo.presets.MoveArrayElement(effectPropertyInfo.presets.arraySize - 1, vector2Int.y);
            effectPropertyInfo.presets.serializedObject.ApplyModifiedProperties();

            EffectInspectorInfo effectInspectorInfo = _effectInspectorObject.list[vector2Int.x];

            effectInspectorInfo.currentIndex = vector2Int.y;

            EditorUtility.SetDirty(_target);
        }

        private void DeletePreset(object obj)
        {
            Vector2Int vector2Int = (Vector2Int)obj;

            if (vector2Int.y < 0)
            {
                return;
            }

            EffectPropertyInfo effectPropertyInfo = _effectPropertyInfoList[vector2Int.x];

            effectPropertyInfo.currentDirty = true;

            int presetCount = effectPropertyInfo.presets.arraySize;

            effectPropertyInfo.presets.DeleteArrayElementAtIndex(vector2Int.y);

            if (effectPropertyInfo.presets.arraySize == presetCount)
            {
                effectPropertyInfo.presets.DeleteArrayElementAtIndex(vector2Int.y);
            }

            effectPropertyInfo.presets.serializedObject.ApplyModifiedProperties();

            EditorUtility.SetDirty(_target);
        }

        private void MovePreset(object obj)
        {
            Vector3Int vector3Int = (Vector3Int)obj;

            EffectPropertyInfo effectPropertyInfo = _effectPropertyInfoList[vector3Int.x];

            if (vector3Int.z < 0 || vector3Int.z >= effectPropertyInfo.presets.arraySize)
            {
                return;
            }

            effectPropertyInfo.currentDirty = true;

            effectPropertyInfo.presets.MoveArrayElement(vector3Int.y, vector3Int.z);
            effectPropertyInfo.presets.serializedObject.ApplyModifiedProperties();

            EffectInspectorInfo effectInspectorInfo = _effectInspectorObject.list[vector3Int.x];

            effectInspectorInfo.currentIndex = vector3Int.z;

            EditorUtility.SetDirty(_target);
        }
    }
}