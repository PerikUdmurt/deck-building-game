﻿// Copyright (C) 2020 - 2024 Seeley Studio. All Rights Reserved.

#pragma warning disable IDE0090

using UnityEngine;

namespace Animatext.Effects
{
    [CreateAssetMenu(menuName = "Animatext Preset/Coherence - Range/Basic/Basic - A04", fileName = "New CRBasicA04 Preset", order = 369)]
    public sealed class CRBasicA04 : DefaultTemplateEffect
    {
        public float singleTime = 1;
        public Vector2 startSkew = Vector2.zero;
        public Vector2 skew = new Vector2(0, 45);
        public AnchorType anchorType = AnchorType.Center;
        public Vector2 anchorOffset = Vector2.zero;
        public EasingType easingType;
        public bool continuousEasing = true;

        public override InfoFlags infoFlags
        {
            get { return InfoFlags.Range; }
        }

        protected override int unitCount
        {
            get { return 1; }
        }

        protected override float unitTime
        {
            get { return singleTime; }
        }

        protected override void Animate()
        {
            float progress = GetCurrentProgress(0);

            progress = EasingUtility.Ease(progress, easingType);
            progress = EasingUtility.Basic(progress, continuousEasing);

            range.Skew(Vector2.LerpUnclamped(startSkew, skew, progress), range.GetAnchorPoint(anchorType) + anchorOffset);
        }
    }
}