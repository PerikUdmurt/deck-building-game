﻿// Copyright (C) 2020 - 2024 Seeley Studio. All Rights Reserved.

using UnityEngine;

namespace Animatext.Effects
{
    [CreateAssetMenu(menuName = "Animatext Preset/Other/Animatext/Disable", fileName = "New DisableAnimatext Preset", order = 369)]
    public sealed class DisableAnimatext : BaseEffect
    {
        public float executionTime = 5;

        public override InfoFlags infoFlags
        {
            get { return InfoFlags.None; }
        }

        protected override void Animate()
        {
            if (lastTime < executionTime && time >= executionTime)
            {
                animatext.enabled = false;
            }
        }
    }
}