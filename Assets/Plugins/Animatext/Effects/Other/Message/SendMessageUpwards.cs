﻿// Copyright (C) 2020 - 2024 Seeley Studio. All Rights Reserved.

using UnityEngine;

namespace Animatext.Effects
{
    [CreateAssetMenu(menuName = "Animatext Preset/Other/Message/Send Upwards", fileName = "New SendMessageUpwards Preset", order = 369)]
    public sealed class SendMessageUpwards : BaseEffect
    {
        public float executionTime = 5;
        public string methodName = string.Empty;

        public override InfoFlags infoFlags
        {
            get { return InfoFlags.None; }
        }

        protected override void Animate()
        {
            if (lastTime < executionTime && time >= executionTime)
            {
                animatext.SendMessageUpwards(methodName, SendMessageOptions.DontRequireReceiver);
            }
        }
    }
}