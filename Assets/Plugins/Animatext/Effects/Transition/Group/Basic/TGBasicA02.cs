﻿// Copyright (C) 2020 - 2024 Seeley Studio. All Rights Reserved.

#pragma warning disable IDE0090

using UnityEngine;

namespace Animatext.Effects
{
    [CreateAssetMenu(menuName = "Animatext Preset/Transition - Group/Basic/Basic - A02", fileName = "New TGBasicA02 Preset", order = 369)]
    public sealed class TGBasicA02 : DefaultTemplateEffect
    {
        public float singleTime = 1;
        public SortType sortType;
        public float rotation = 45;
        public AnchorType anchorType = AnchorType.Center;
        public Vector2 anchorOffset = Vector2.zero;
        public EasingType easingType;
        [FadeMode] public ColorMode fadeMode = ColorMode.Multiply;
        public FloatRange fadeRange = new FloatRange(0, 0.5f);

        public override InfoFlags infoFlags
        {
            get { return InfoFlags.Group; }
        }

        protected override int unitCount
        {
            get { return groupCount; }
        }

        protected override float unitTime
        {
            get { return singleTime; }
        }

        protected override void Animate()
        {
            for (int i = 0; i < groupCount; i++)
            {
                float progress = GetCurrentProgress(SortUtility.Rank(i, groupCount, sortType));

                if (progress <= fadeRange.start)
                {
                    groups[i].Opacify(0, fadeMode);
                }
                else
                {
                    if (progress >= fadeRange.end)
                    {
                        groups[i].Opacify(1, fadeMode);
                    }
                    else
                    {
                        groups[i].Opacify(Mathf.InverseLerp(fadeRange.start, fadeRange.end, progress), fadeMode);
                    }

                    progress = 1 - EasingUtility.Ease(progress, easingType);

                    groups[i].Rotate(rotation * progress, groups[i].GetAnchorPoint(anchorType) + anchorOffset);
                }
            }
        }
    }
}