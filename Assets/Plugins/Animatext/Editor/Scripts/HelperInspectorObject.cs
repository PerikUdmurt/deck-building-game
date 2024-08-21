// Copyright (C) 2020 - 2024 Seeley Studio. All Rights Reserved.

using System;
using System.Collections.Generic;
using UnityEngine;

namespace Animatext.Editor
{
    [Serializable]
    public class HelperInspectorObject : ScriptableObject
    {
        public string text;
        public readonly List<HelperInspectorInfo> list = new List<HelperInspectorInfo>();
    }
}