// Copyright (C) 2020 - 2024 Seeley Studio. All Rights Reserved.

using System.Collections.Generic;
using UnityEngine;

namespace Animatext.Editor
{
    public class EffectInspectorObject : ScriptableObject
    {
        public string text;
        public readonly List<EffectInspectorInfo> list = new List<EffectInspectorInfo>();
    }
}