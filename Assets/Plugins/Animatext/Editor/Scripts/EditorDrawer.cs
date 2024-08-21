// Copyright (C) 2020 - 2024 Seeley Studio. All Rights Reserved.

#pragma warning disable IDE0017, IDE0090, IDE0300

using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Animatext.Editor
{
    public static class EditorDrawer
    {
        private static bool _editorStyle;
        private static GUISkin _skin;
        private static GUIStyle _styleBannerBackground;
        private static GUIStyle _styleBannerText;
        private static GUIStyle _styleBannerTitle;
        private static GUIStyle _styleButton;
        private static GUIStyle _styleCircle;
        private static GUIStyle _styleCircleL;
        private static GUIStyle _styleCircleR;
        private static GUIStyle _styleDocumentation;
        private static GUIStyle _styleDropdown;
        private static GUIStyle _styleLabel;
        private static GUIStyle _styleList;
        private static GUIStyle _styleMore;
        private static GUIStyle _styleNewEffect;
        private static GUIStyle _styleSettings;
        private static readonly string _contentNewEffect = "New Effect";
        private static readonly string _contentNewHelper = "New Helper";
        private static readonly string _contentDocumentation = "Documentation";
        private static readonly string _contentSettings = "Settings";
        private static readonly List<int[]> _optionValuesList = new List<int[]>();
        private static readonly List<string[]> _optionNamesList = new List<string[]>();
        private static readonly GUIContent _contentEnd = new GUIContent("End");
        private static readonly GUIContent _contentEnd1 = new GUIContent("E..");
        private static readonly GUIContent _contentEnd2 = new GUIContent("En..");
        private static readonly GUIContent _contentPause = new GUIContent("Pause");
        private static readonly GUIContent _contentPause1 = new GUIContent("P..");
        private static readonly GUIContent _contentPause2 = new GUIContent("Pa..");
        private static readonly GUIContent _contentPause3 = new GUIContent("Pau..");
        private static readonly GUIContent _contentPlay = new GUIContent("Play");
        private static readonly GUIContent _contentPlay1 = new GUIContent("P..");
        private static readonly GUIContent _contentPlay2 = new GUIContent("Pl..");
        private static readonly GUIContent _contentPlay3 = new GUIContent("Pla..");
        private static readonly GUIContent _contentProceed = new GUIContent("Proceed");
        private static readonly GUIContent _contentProceed1 = new GUIContent("P");
        private static readonly GUIContent _contentProceed2 = new GUIContent("Pr..");
        private static readonly GUIContent _contentProceed3 = new GUIContent("Pro..");
        private static readonly GUIContent _contentProceed4 = new GUIContent("Proc..");
        private static readonly GUIContent _contentProceed5 = new GUIContent("Proce..");
        private static readonly GUIContent _contentStart = new GUIContent("Start");
        private static readonly GUIContent _contentStart1 = new GUIContent("S..");
        private static readonly GUIContent _contentStart2 = new GUIContent("St..");
        private static readonly GUIContent _contentStart3 = new GUIContent("Sta..");
        private static readonly GUIContent _contentStop = new GUIContent("Stop");
        private static readonly GUIContent _contentStop1 = new GUIContent("S..");
        private static readonly GUIContent _contentStop2 = new GUIContent("St..");
        private static readonly GUIContent[] _contentsA = new GUIContent[] { _contentStart, _contentPlay, _contentProceed, _contentPause, _contentEnd, _contentStop };
        private static readonly GUIContent[] _contentsB = new GUIContent[] { _contentStart, _contentPlay, _contentProceed5, _contentPause, _contentEnd, _contentStop };
        private static readonly GUIContent[] _contentsC = new GUIContent[] { _contentStart, _contentPlay, _contentProceed4, _contentPause3, _contentEnd, _contentStop };
        private static readonly GUIContent[] _contentsD = new GUIContent[] { _contentStart3, _contentPlay3, _contentProceed3, _contentPause3, _contentEnd, _contentStop };
        private static readonly GUIContent[] _contentsE = new GUIContent[] { _contentStart2, _contentPlay2, _contentProceed2, _contentPause2, _contentEnd2, _contentStop2 };
        private static readonly GUIContent[] _contentsF = new GUIContent[] { _contentStart1, _contentPlay1, _contentProceed1, _contentPause1, _contentEnd1, _contentStop1 };

        public static bool editorStyle
        {
            get { return _editorStyle; }
        }

        public static void Init()
        {
            if (_skin == null)
            {
                _skin = AssetDatabase.LoadAssetAtPath<GUISkin>(EditorPath.skinPath);

                _styleBannerBackground = _skin.GetStyle("BannerBackground");
                _styleBannerText = _skin.GetStyle("BannerText");
                _styleBannerTitle = _skin.GetStyle("BannerTitle");
                _styleCircle = _skin.GetStyle("Circle");
                _styleCircleL = _skin.GetStyle("CircleL");
                _styleCircleR = _skin.GetStyle("CircleR");
                _styleDocumentation = _skin.GetStyle("Documentation");
                _styleList = _skin.GetStyle("List");
                _styleMore = _skin.GetStyle("More");
                _styleNewEffect = _skin.GetStyle("New");
                _styleSettings = _skin.GetStyle("Settings");
            }

            if (!_editorStyle)
            {
                try
                {
                    if (EditorStyles.miniButton != null && EditorStyles.label != null)
                    {
                        _styleDropdown = new GUIStyle(EditorStyles.miniButton);
                        _styleDropdown.fixedWidth = 24;
                        _styleDropdown.fixedHeight = 24;

                        _styleLabel = new GUIStyle(EditorStyles.label);
                        _styleLabel.normal.textColor = EditorStyles.miniButton.normal.textColor;

                        _styleButton = new GUIStyle(EditorStyles.miniButton);
                        _styleButton.fixedHeight = 24;

#if !UNITY_2019_3_OR_NEWER
                        if (!EditorGUIUtility.isProSkin)
                        {
                            _styleDropdown.border.top = 2;
                            _styleButton.border.top = 2;
                        }
#endif
                        _editorStyle = true;
                    }
                    else
                    {
                        _styleDropdown = _skin.button;
                        _styleLabel = _skin.label;
                        _styleButton = _skin.button;
                    }
                }
                catch
                {
                    _styleDropdown = _skin.button;
                    _styleLabel = _skin.label;
                    _styleButton = _skin.button;
                }
            }
        }

        public static void BeginDrawer()
        {
            GUILayout.BeginHorizontal();
            GUILayout.Space(20);
            GUILayout.BeginVertical();
        }

        public static void EndDrawer()
        {
            GUILayout.EndVertical();
            GUILayout.Space(10);
            GUILayout.EndHorizontal();
        }

        public static void DrawBanner()
        {
            float smallValue;
            float middleValue;
            float largeValue;
            float smallCircle;
            float middleCircle;

            if (EditorGUIUtility.currentViewWidth > 345)
            {
                smallValue = EditorGUIUtility.currentViewWidth / 3 - 48;
                middleValue = EditorGUIUtility.currentViewWidth / 5 - 36;
                largeValue = 64;
                smallCircle = 10;
                middleCircle = 20;
            }
            else
            {
                smallValue = Mathf.Max(12, EditorGUIUtility.currentViewWidth / 3 - 48);
                middleValue = Mathf.Max(6, EditorGUIUtility.currentViewWidth * 2 / 5 - 105);
                largeValue = Mathf.Max(0, EditorGUIUtility.currentViewWidth / 3 - 51);
                smallCircle = 0;
                middleCircle = 0;
            }

            GUILayout.BeginVertical();
            {
                GUILayout.Label(GUIContent.none, _styleBannerBackground, GUILayout.Height(64));
                GUILayout.Space(-64);

                GUILayout.Label(GUIContent.none, _styleBannerText, GUILayout.Width(EditorGUIUtility.currentViewWidth - 35), GUILayout.Height(64));
                GUILayout.Space(-64);

                GUILayout.BeginHorizontal(GUILayout.Height(64));
                {
                    GUILayout.Label(GUIContent.none, _styleCircleL, GUILayout.Width(largeValue), GUILayout.Height(largeValue));
                    GUILayout.FlexibleSpace();

                    GUILayout.BeginVertical();
                    {
                        GUILayout.Space(64 - largeValue);
                        GUILayout.Label(GUIContent.none, _styleCircleR, GUILayout.Width(largeValue), GUILayout.Height(largeValue));
                    }
                    GUILayout.EndVertical();
                }
                GUILayout.EndHorizontal();

                GUILayout.Space(-64);

                GUILayout.BeginHorizontal(GUILayout.Height(64));
                {
                    GUILayout.Space(middleValue);

                    GUILayout.BeginVertical(GUILayout.Height(64));
                    {
                        GUILayout.FlexibleSpace();
                        GUILayout.Label(GUIContent.none, _styleCircle, GUILayout.Width(middleCircle), GUILayout.Height(middleCircle));
                        GUILayout.Space(6);
                    }
                    GUILayout.EndVertical();

                    GUILayout.FlexibleSpace();

                    GUILayout.BeginVertical(GUILayout.Height(64));
                    {
                        GUILayout.Space(6);
                        GUILayout.Label(GUIContent.none, _styleCircle, GUILayout.Width(middleCircle), GUILayout.Height(middleCircle));
                    }
                    GUILayout.EndVertical();

                    GUILayout.Space(middleValue);
                }
                GUILayout.EndHorizontal();

                GUILayout.Space(-64);

                GUILayout.BeginHorizontal(GUILayout.Height(64));
                {
                    GUILayout.Space(smallValue);

                    GUILayout.BeginVertical(GUILayout.Height(64));
                    {
                        GUILayout.Space(8);
                        GUILayout.Label(GUIContent.none, _styleCircle, GUILayout.Width(smallCircle), GUILayout.Height(smallCircle));

                    }
                    GUILayout.EndVertical();

                    GUILayout.FlexibleSpace();

                    GUILayout.BeginVertical(GUILayout.Height(64));
                    {
                        GUILayout.FlexibleSpace();
                        GUILayout.Label(GUIContent.none, _styleCircle, GUILayout.Width(smallCircle), GUILayout.Height(smallCircle));
                        GUILayout.Space(8);
                    }
                    GUILayout.EndVertical();

                    GUILayout.Space(smallValue);
                }
                GUILayout.EndHorizontal();

                GUILayout.Space(-64);

                GUILayout.BeginHorizontal();
                {
                    GUILayout.FlexibleSpace();
                    GUILayout.Label(GUIContent.none, _styleBannerTitle, GUILayout.Width(160), GUILayout.Height(64));
                    GUILayout.FlexibleSpace();
                }
                GUILayout.EndHorizontal();
            }
            GUILayout.EndVertical();
        }

        public static bool DrawButton(float width, string text, GUIStyle style)
        {
            bool result = false;

            GUILayout.BeginVertical();
            {
                if (GUILayout.Button(GUIContent.none, _styleButton, GUILayout.Width(width), GUILayout.Height(24)))
                {
                    result = true;

                    GUI.FocusControl(null);
                }

                GUILayout.Space(-22);

                GUILayout.BeginHorizontal();
                {
                    GUILayout.Space(26);

                    GUILayout.Label(text, _styleLabel, GUILayout.Height(16));
                }
                GUILayout.EndHorizontal();

                GUILayout.Space(-18);

                GUILayout.BeginHorizontal();
                {
                    GUILayout.Space(8);
                    GUILayout.Label(GUIContent.none, style, GUILayout.Width(16), GUILayout.Height(16));
                }
                GUILayout.EndHorizontal();
            }

            GUILayout.EndVertical();

            return result;
        }

        public static bool DrawNewEffectButton()
        {
            return DrawButton(94, _contentNewEffect, _styleNewEffect);
        }

        public static bool DrawNewHelperButton()
        {
            return DrawButton(99, _contentNewHelper, _styleNewEffect);
        }

        public static bool DrawSettingsButton()
        {
            return DrawButton(78, _contentSettings, _styleSettings);
        }

        public static bool DrawDocumentationButton(int viewWidth)
        {
            if (EditorGUIUtility.currentViewWidth > viewWidth)
            {
                return DrawButton(118, _contentDocumentation, _styleDocumentation);
            }
            else
            {
                return DrawButton(26, string.Empty, _styleDocumentation);
            }
        }

        public static bool DrawBar(string text)
        {
            bool result = false;

            GUILayout.Space(-3);

            GUILayout.BeginVertical();
            {
                if (GUILayout.Button(GUIContent.none, _styleButton, GUILayout.ExpandWidth(true), GUILayout.Height(24)))
                {
                    result = true;

                    GUI.FocusControl(null);
                }

                GUILayout.Space(-22);

                GUILayout.BeginHorizontal();
                {
                    GUILayout.Space(26);

                    GUILayout.Label(text, _styleLabel, GUILayout.Width(Mathf.Max(128, EditorGUIUtility.currentViewWidth - 100)), GUILayout.Height(16));
                }
                GUILayout.EndHorizontal();

                GUILayout.Space(-18);

                GUILayout.BeginHorizontal();
                {
                    GUILayout.Space(8);
                    GUILayout.Label(GUIContent.none, _styleList, GUILayout.Width(16), GUILayout.Height(16));
                }
                GUILayout.EndHorizontal();
            }
            GUILayout.EndVertical();

            return result;
        }

        public static bool DrawBarButton()
        {
            bool result = false;

            GUILayout.Space(2);

            if (EditorGUILayout.DropdownButton(GUIContent.none, FocusType.Passive, _styleDropdown, GUILayout.Height(24)))
            {
                result = true;
            }

            GUILayout.Space(-20);

            GUILayout.BeginHorizontal();
            {
#if !UNITY_2019_3_OR_NEWER
                GUILayout.Space(2);
#endif
                GUILayout.Space(8);

                GUILayout.Label(GUIContent.none, _styleMore, GUILayout.Width(12), GUILayout.Height(12));
            }
            GUILayout.EndHorizontal();

            return result;
        }

        public static int DrawEventBar(Rect rect, int selected)
        {
            GUIContent[] contents;

            if (EditorGUIUtility.currentViewWidth > 455)
            {
                contents = _contentsA;
            }
            else if (EditorGUIUtility.currentViewWidth > 407)
            {
                contents = _contentsB;
            }
            else if (EditorGUIUtility.currentViewWidth > 353)
            {
                contents = _contentsC;
            }
            else if (EditorGUIUtility.currentViewWidth > 335)
            {
                contents = _contentsD;
            }
            else if (EditorGUIUtility.currentViewWidth > 299)
            {
                contents = _contentsE;
            }
            else
            {
                contents = _contentsF;
            }

            rect.x += (int)EditorGUIUtility.currentViewWidth % 6 + 60;
            rect.width -= (int)EditorGUIUtility.currentViewWidth % 6 + 60;

            return GUI.Toolbar(rect, selected, contents);
        }

        public static void DrawPreset(SerializedProperty serializedProperty)
        {
            UnityEditor.Editor editor = UnityEditor.Editor.CreateEditor(EditorUtility.InstanceIDToObject(serializedProperty.objectReferenceInstanceIDValue));

            if (editor != null)
            {
                GUILayout.Space(5);

                if (EditorApplication.isPlaying)
                {
                    GUI.enabled = true;
                }

                editor.OnInspectorGUI();
            }
        }

        public static bool DrawPresetButton()
        {
            GUILayout.Space(2);

            return EditorGUILayout.DropdownButton(GUIContent.none, FocusType.Passive);
        }

        public static int DrawPresetPopup(int index, int count)
        {
            if (count >= _optionNamesList.Count)
            {
                for (int i = _optionNamesList.Count; i <= count; i++)
                {
                    string[] optionNames = new string[i];

                    for (int j = 0; j < i; j++)
                    {
                        optionNames[j] = " Preset " + j;
                    }

                    _optionNamesList.Add(optionNames);
                }
            }

            if (count >= _optionValuesList.Count)
            {
                for (int i = _optionValuesList.Count; i <= count; i++)
                {
                    int[] optionValues = new int[i];

                    for (int j = 0; j < i; j++)
                    {
                        optionValues[j] = j;
                    }

                    _optionValuesList.Add(optionValues);
                }
            }

            float intPopupWidth = EditorGUIUtility.labelWidth;

#if UNITY_2019_3_OR_NEWER
            intPopupWidth -= 24;
#elif UNITY_2019_1 || UNITY_2019_2
            intPopupWidth -= 28;
#else
            intPopupWidth -= 34;
#endif
            return EditorGUILayout.IntPopup(index, _optionNamesList[count], _optionValuesList[count], GUILayout.Width(intPopupWidth));
        }
    }
}