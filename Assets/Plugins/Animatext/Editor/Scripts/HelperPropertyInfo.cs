// Copyright (C) 2020 - 2024 Seeley Studio. All Rights Reserved.

using System;
using UnityEditor;

namespace Animatext.Editor
{
    [Serializable]
    public class HelperPropertyInfo
    {
        public SerializedProperty helper;
        public SerializedProperty animatext;
        public SerializedProperty indexList;
        public SerializedProperty onEndEvent;
        public SerializedProperty onPauseEvent;
        public SerializedProperty onPlayEvent;
        public SerializedProperty onProceedEvent;
        public SerializedProperty onStartEvent;
        public SerializedProperty onStopEvent;
    }
}