// Copyright (C) 2020 - 2024 Seeley Studio. All Rights Reserved.

#pragma warning disable IDE1006

using UnityEditor;
using UnityEngine;

namespace Animatext.Editor
{
    public class EditorPath : ScriptableObject
    {
        private static bool _initPath;
        private static string _basePath;

        public static string basePath
        {
            get
            {
                if (!_initPath)
                {
                    var obj = CreateInstance<EditorPath>();
                    MonoScript monoScript = MonoScript.FromScriptableObject(obj);
                    string assetPath = AssetDatabase.GetAssetPath(monoScript);
                    DestroyImmediate(obj);

                    int index = assetPath.IndexOf("Editor");

                    if (index >= 0)
                    {
                        _initPath = true;
                        _basePath = assetPath.Substring(0, assetPath.IndexOf("Editor"));
                    }
                    else
                    {
                        return "Assets/Plugins/Animatext/";
                    }
                }

                return _basePath;
            }
        }

        public static string skinPath
        {
            get
            {
                if (EditorGUIUtility.isProSkin)
                {
                    return basePath + "Editor/Themes/SkinDark.guiskin";
                }
                else
                {
                    return basePath + "Editor/Themes/SkinLight.guiskin";
                }
            }
        }

        public static string documentationURL
        {
            get
            {
                return Application.dataPath + basePath.Substring(6) + "Editor/Documentation/Pages/Manual/Index.html";
            }
        }
    }
}