﻿// Copyright (C) 2020 - 2024 Seeley Studio. All Rights Reserved.

#pragma warning disable IDE0090

using UnityEngine;

namespace Animatext.Effects
{
    // [CreateAssetMenu(menuName = "Animatext Preset/Coherence - Line/Elastic/Elastic - A05", fileName = "New CLElasticA05 Preset", order = 369)]
    public sealed class CLElasticA05 : DefaultTemplateEffect
    {
        public float singleTime = 1;
        public SortType sortType;
        public float startRotation = 0;
        public float rotation = 180;
        public AnchorType anchorType = AnchorType.Center;
        public Vector2 anchorOffset = new Vector2(0, 50);
        public int oscillations = 2;
        public float stiffness = 5;
        public EasingType easingType;

        public override InfoFlags infoFlags
        {
            get { return InfoFlags.Line; }
        }

        protected override int unitCount
        {
            get { return lineCount; }
        }

        protected override float unitTime
        {
            get { return singleTime; }
        }

        protected override void Animate()
        {
            for (int i = 0; i < lineCount; i++)
            {
                float progress = GetCurrentProgress(SortUtility.Rank(i, lineCount, sortType));

                progress = EasingUtility.Ease(progress, easingType);
                progress = EasingUtility.Elastic(progress, oscillations, stiffness);

                lines[i].Move(PositionUtility.Rotate(lines[i].anchor.center, Mathf.LerpUnclamped(startRotation, rotation, progress), lines[i].GetAnchorPoint(anchorType) + anchorOffset) - lines[i].anchor.center);
            }
        }
    }
}