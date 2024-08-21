// Copyright (C) 2020 - 2024 Seeley Studio. All Rights Reserved.

#pragma warning disable IDE0017, IDE0090

using System.Collections.Generic;
using System.Text;
using UnityEditor;
using UnityEngine;

namespace Animatext.Editor
{
    [CustomEditor(typeof(AnimatextEventHelper), true)]
    public class AnimatextEventHelperEditor : UnityEditor.Editor
    {
        private static readonly GUIContent _contentAdd = new GUIContent("Add");
        private static readonly GUIContent _contentDelete = new GUIContent("Delete");
        private static readonly GUIContent _contentMoveUp = new GUIContent("Move Up");
        private static readonly GUIContent _contentMoveDown = new GUIContent("Move Down");
        private static readonly GUIContent _contentAnimatext = new GUIContent("Animatext", "The Animatext component of the effects. If the value of \"Animatext\" is null, it will automatically get the Animatext component of the current game object at runtime.");
        private static readonly GUIContent _contentIndexList = new GUIContent("Index List", "The index list of the effects in the Animatext component. If the index list contains -1, it means the index list contains all the indexes of the effects in the Animatext component.");
        private static readonly GUIContent _contentEvents = new GUIContent("Events", "The events of the effects.");

        private AnimatextEventHelper _target;
        private BaseAnimatext _animatext;
        private SerializedProperty _helpersProperty;
        private HelperInspectorObject _helperInspectorObject;
        private List<HelperPropertyInfo> _helperPropertyInfoList;

        protected virtual void OnEnable()
        {
            _target = target as AnimatextEventHelper;
            _animatext = _target.GetComponent<BaseAnimatext>();

            _helpersProperty = serializedObject.FindProperty("_helpers");

            _helperInspectorObject = CreateInstance<HelperInspectorObject>();
            _helperPropertyInfoList = new List<HelperPropertyInfo>();
        }

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

            for (int i = 0; i < _helpersProperty.arraySize; i++)
            {
                DrawHelper(i);

                GUILayout.Space(10);
            }


            serializedObject.ApplyModifiedProperties();
        }

        private void DrawMenu()
        {
            GUILayout.BeginHorizontal();
            {
                GUILayout.Space(-3);

                if (EditorDrawer.DrawNewHelperButton())
                {
                    AddHelper(0);
                }

                GUILayout.FlexibleSpace();

                if (EditorDrawer.DrawDocumentationButton(-1))
                {
                    Application.OpenURL(EditorPath.documentationURL);
                }

                GUILayout.Space(-3);
            }
            GUILayout.EndHorizontal();
        }

        private void DrawHelper(int index)
        {
            HelperInspectorInfo helperInspectorInfo = _helperInspectorObject.list[index];
            HelperPropertyInfo helperPropertyInfo = _helperPropertyInfoList[index];

            string helperTitle = "Helper " + index;
            string indexText = GetIndexText(helperPropertyInfo.indexList);

            if (helperInspectorInfo.helper.animatext != null && _animatext != helperInspectorInfo.helper.animatext)
            {
                helperTitle += " - GameOject : " + helperInspectorInfo.helper.animatext.gameObject.name;
            }

            if (indexText.Contains("-1"))
            {
                helperTitle += " - All Effects ";
            }
            else if (indexText != string.Empty)
            {
                helperTitle += " - Effect : " + indexText;
            }

            GUILayout.BeginHorizontal();
            {
                if (EditorDrawer.DrawBar(helperTitle))
                {
                    helperInspectorInfo.isOn = !helperInspectorInfo.isOn;
                }

                Rect helperRect = EditorGUILayout.BeginVertical(GUILayout.Width(24), GUILayout.Height(24));
                {
                    if (EditorDrawer.DrawBarButton())
                    {
                        helperRect.x -= 134;
                        helperRect.y += 2;

                        GenericMenu menu = new GenericMenu();

                        menu.AddItem(_contentAdd, false, AddHelper, index + 1);
                        menu.AddItem(_contentDelete, false, DeleteHelper, index);
                        menu.AddItem(_contentMoveUp, false, MoveHelper, new Vector2Int(index, index - 1));
                        menu.AddItem(_contentMoveDown, false, MoveHelper, new Vector2Int(index, index + 1));

                        menu.DropDown(helperRect);

                        GUI.FocusControl(null);
                    }
                }
                EditorGUILayout.EndVertical();

                GUILayout.Space(-2);
            }
            GUILayout.EndHorizontal();

            if (helperInspectorInfo.isOn)
            {
                if (EditorApplication.isPlaying && _target.enabled)
                {
                    GUI.enabled = false;
                }

                GUILayout.Space(10);

                EditorDrawer.BeginDrawer();
                {
                    EditorGUILayout.PropertyField(helperPropertyInfo.animatext, _contentAnimatext);
                    
                    string newIndexText = EditorGUILayout.TextField(_contentIndexList, indexText);

                    if (newIndexText != indexText)
                    {
                        SetIndexText(helperPropertyInfo.indexList, newIndexText);
                    }

                    GUILayout.Space(5);

                    Rect eventRect = EditorGUILayout.BeginHorizontal();
                    {
                        EditorGUILayout.LabelField(_contentEvents);
                        helperInspectorInfo.toolbarID = EditorDrawer.DrawEventBar(eventRect, helperInspectorInfo.toolbarID);
                    }
                    EditorGUILayout.EndHorizontal();

                    GUILayout.Space(5);

                    SerializedProperty eventProperty;

                    switch (helperInspectorInfo.toolbarID)
                    {
                        case 0:
                            eventProperty = helperPropertyInfo.onStartEvent;
                            break;

                        case 1:
                            eventProperty = helperPropertyInfo.onPlayEvent;
                            break;

                        case 2:
                            eventProperty = helperPropertyInfo.onProceedEvent;
                            break;

                        case 3:
                            eventProperty = helperPropertyInfo.onPauseEvent;
                            break;

                        case 4:
                            eventProperty = helperPropertyInfo.onEndEvent;
                            break;

                        case 5:
                            eventProperty = helperPropertyInfo.onStopEvent;
                            break;

                        default:
                            eventProperty = helperPropertyInfo.onStartEvent;
                            break;
                    }

                    EditorGUILayout.PropertyField(eventProperty);
                }
                EditorDrawer.EndDrawer();

                if (EditorApplication.isPlaying && _target.enabled)
                {
                    GUI.enabled = true;
                }
            }
        }

        private bool CheckInspectorInfoDirty()
        {
            bool isDirty = false;

            if (_target.helpers.Count == _helperInspectorObject.list.Count)
            {
                for (int i = 0; i < _helperInspectorObject.list.Count; i++)
                {
                    if (_target.helpers[i] != _helperInspectorObject.list[i].helper)
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
            return _helperInspectorObject.list.Count != _helperPropertyInfoList.Count;
        }

        private void UpdateInspectorInfo()
        {
            Dictionary<EffectEventHelper, HelperInspectorInfo> dictionary = new Dictionary<EffectEventHelper, HelperInspectorInfo>();

            for (int i = 0; i < _helperInspectorObject.list.Count; i++)
            {
                if (!dictionary.ContainsKey(_helperInspectorObject.list[i].helper))
                {
                    dictionary.Add(_helperInspectorObject.list[i].helper, _helperInspectorObject.list[i]);
                }
            }

            _helperInspectorObject.list.Clear();

            for (int i = 0; i < _target.helpers.Count; i++)
            {
                if (dictionary.ContainsKey(_target.helpers[i]))
                {
                    _helperInspectorObject.list.Add(dictionary[_target.helpers[i]]);
                }
                else
                {
                    HelperInspectorInfo helperInspectorInfo = new HelperInspectorInfo();

                    helperInspectorInfo.isOn = false;
                    helperInspectorInfo.toolbarID = 0;
                    helperInspectorInfo.helper = _target.helpers[i];

                    _helperInspectorObject.list.Add(helperInspectorInfo);
                }
            }
        }

        private void UpdatePropertyInfo()
        {
            if (_helperInspectorObject.list.Count < _helperPropertyInfoList.Count)
            {
                _helperPropertyInfoList.RemoveRange(_helperPropertyInfoList.Count, _helperPropertyInfoList.Count - _helperPropertyInfoList.Count);
            }
            else if (_helperInspectorObject.list.Count > _helperPropertyInfoList.Count)
            {
                for (int i = _helperPropertyInfoList.Count; i < _helperInspectorObject.list.Count; i++)
                {
                    HelperPropertyInfo helperPropertyInfo = new HelperPropertyInfo();

                    helperPropertyInfo.helper = _helpersProperty.GetArrayElementAtIndex(i);
                    helperPropertyInfo.animatext = helperPropertyInfo.helper.FindPropertyRelative("animatext");
                    helperPropertyInfo.indexList = helperPropertyInfo.helper.FindPropertyRelative("indexList");
                    helperPropertyInfo.onEndEvent = helperPropertyInfo.helper.FindPropertyRelative("onEndEvent");
                    helperPropertyInfo.onPauseEvent = helperPropertyInfo.helper.FindPropertyRelative("onPauseEvent");
                    helperPropertyInfo.onPlayEvent = helperPropertyInfo.helper.FindPropertyRelative("onPlayEvent");
                    helperPropertyInfo.onProceedEvent = helperPropertyInfo.helper.FindPropertyRelative("onProceedEvent");
                    helperPropertyInfo.onStartEvent = helperPropertyInfo.helper.FindPropertyRelative("onStartEvent");
                    helperPropertyInfo.onStopEvent = helperPropertyInfo.helper.FindPropertyRelative("onStopEvent");

                    _helperPropertyInfoList.Add(helperPropertyInfo);
                }
            }
        }

        private void AddHelper(object obj)
        {
            int index = (int)obj;

            Object[] objects = new Object[2];

            objects[0] = _target;
            objects[1] = _helperInspectorObject;

            Undo.RecordObjects(objects, "Add Helper");

            _target.helpers.Insert(index, new EffectEventHelper(_animatext));
            _helpersProperty.serializedObject.Update();

            HelperPropertyInfo helperPropertyInfo = new HelperPropertyInfo();

            helperPropertyInfo.helper = _helpersProperty.GetArrayElementAtIndex(_helpersProperty.arraySize - 1);
            helperPropertyInfo.animatext = helperPropertyInfo.helper.FindPropertyRelative("animatext");
            helperPropertyInfo.indexList = helperPropertyInfo.helper.FindPropertyRelative("indexList");
            helperPropertyInfo.onEndEvent = helperPropertyInfo.helper.FindPropertyRelative("onEndEvent");
            helperPropertyInfo.onPauseEvent = helperPropertyInfo.helper.FindPropertyRelative("onPauseEvent");
            helperPropertyInfo.onPlayEvent = helperPropertyInfo.helper.FindPropertyRelative("onPlayEvent");
            helperPropertyInfo.onProceedEvent = helperPropertyInfo.helper.FindPropertyRelative("onProceedEvent");
            helperPropertyInfo.onStartEvent = helperPropertyInfo.helper.FindPropertyRelative("onStartEvent");
            helperPropertyInfo.onStopEvent = helperPropertyInfo.helper.FindPropertyRelative("onStopEvent");

            _helperPropertyInfoList.Add(helperPropertyInfo);

            HelperInspectorInfo helperInspectorInfo = new HelperInspectorInfo();

            helperInspectorInfo.isOn = true;
            helperInspectorInfo.toolbarID = 0;
            helperInspectorInfo.helper = _target.helpers[index];

            _helperInspectorObject.list.Insert(index, helperInspectorInfo);

            EditorUtility.SetDirty(_target);
        }

        private void DeleteHelper(object obj)
        {
            int index = (int)obj;

            Object[] objects = new Object[2];

            objects[0] = _target;
            objects[1] = _helperInspectorObject;

            Undo.RecordObjects(objects, "Delete Helper");

            _target.helpers.RemoveAt(index);

            _helperPropertyInfoList.RemoveAt(_helperPropertyInfoList.Count - 1);

            _helperInspectorObject.list.RemoveAt(index);

            EditorUtility.SetDirty(_target);
        }

        private void MoveHelper(object obj)
        {
            Vector2Int vector2Int = (Vector2Int)obj;

            if (vector2Int.y < 0 || vector2Int.y >= _helpersProperty.arraySize)
            {
                return;
            }

            Object[] objects = new Object[2];

            objects[0] = _target;
            objects[1] = _helperInspectorObject;

            Undo.RecordObjects(objects, "Move Helper");

            EffectEventHelper helper = _target.helpers[vector2Int.x];

            _target.helpers.RemoveAt(vector2Int.x);
            _target.helpers.Insert(vector2Int.y, helper);

            HelperInspectorInfo helperInspectorInfo = _helperInspectorObject.list[vector2Int.x];

            _helperInspectorObject.list.RemoveAt(vector2Int.x);
            _helperInspectorObject.list.Insert(vector2Int.y, helperInspectorInfo);

            EditorUtility.SetDirty(_target);
        }

        private string GetIndexText(SerializedProperty property)
        {
            StringBuilder builder = new StringBuilder();

            if (property.arraySize > 0)
            {
                builder.Append(property.GetArrayElementAtIndex(0).intValue);
            }

            for (int i = 1; i < property.arraySize; i++)
            {
                builder.Append(',');
                builder.Append(' ');
                builder.Append(property.GetArrayElementAtIndex(i).intValue);
            }

            if (property.arraySize > 0)
            {
                builder.Append(' ');
            }

            return builder.ToString();
        }

        private void SetIndexText(SerializedProperty property, string indexText)
        {
            string[] texts = indexText.Split(',', ' ');
            SortedSet<int> indexSet = new SortedSet<int>();

            if (texts != null)
            {
                for (int i = 0; i < texts.Length; i++)
                {
                    if (texts[i] == string.Empty) continue;

                    if (int.TryParse(texts[i], out int result))
                    {
                        if (result >= -1)
                        {
                            indexSet.Add(result);
                        }
                    }
                }
            }

            property.arraySize = indexSet.Count;

            int propertyIndex = 0;
            foreach (int index in indexSet)
            {
                property.GetArrayElementAtIndex(propertyIndex).intValue = index;
                propertyIndex++;
            }
        }
    }
}