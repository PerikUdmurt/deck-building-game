﻿// Copyright (C) 2020 - 2024 Seeley Studio. All Rights Reserved.

using UnityEngine;

namespace Animatext.Effects
{
    [CreateAssetMenu(menuName = "Animatext Preset/Coherence - Character/Elastic/Elastic - A03", fileName = "New CCElasticA03 Preset", order = 369)]
    public sealed class CCElasticA03 : DefaultTemplateEffect
    {
        public float singleTime = 1;
        public SortType sortType;
        public Vector2 startScale = Vector2.one;
        public Vector2 scale = Vector2.zero;
        public AnchorType anchorType = AnchorType.Center;
        public Vector2 anchorOffset = Vector2.zero;
        public int oscillations = 2;
        public float stiffness = 5;
        public EasingType easingType;

        public override InfoFlags infoFlags
        {
            get { return InfoFlags.Character; }
        }

        protected override int unitCount
        {
            get { return characterCount; }
        }

        protected override float unitTime
        {
            get { return singleTime; }
        }

        protected override void Animate()
        {
            for (int i = 0; i < characterCount; i++)
            {
                float progress = GetCurrentProgress(SortUtility.Rank(i, characterCount, sortType));

                progress = EasingUtility.Ease(progress, easingType);
                progress = EasingUtility.Elastic(progress, oscillations, stiffness);

                characters[i].Scale(Vector2.LerpUnclamped(startScale, scale, progress), characters[i].GetAnchorPoint(anchorType) + anchorOffset);
            }
        }
    }
}