﻿// Copyright (C) 2020 - 2024 Seeley Studio. All Rights Reserved.

#pragma warning disable IDE0090

using UnityEngine;

namespace Animatext.Effects
{
    [CreateAssetMenu(menuName = "Animatext Preset/Transition - Word/Basic/Basic - A01", fileName = "New TWBasicA01 Preset", order = 369)]
    public sealed class TWBasicA01 : DefaultTemplateEffect
    {
        public float singleTime = 1;
        public SortType sortType;
        public Vector2 position = new Vector2(0, 100);
        public EasingType easingType;
        [FadeMode] public ColorMode fadeMode = ColorMode.Multiply;
        public FloatRange fadeRange = new FloatRange(0, 0.5f);

        public override InfoFlags infoFlags
        {
            get { return InfoFlags.Word; }
        }

        protected override int unitCount
        {
            get { return wordCount; }
        }

        protected override float unitTime
        {
            get { return singleTime; }
        }

        protected override void Animate()
        {
            for (int i = 0; i < wordCount; i++)
            {
                float progress = GetCurrentProgress(SortUtility.Rank(i, wordCount, sortType));

                if (progress <= fadeRange.start)
                {
                    words[i].Opacify(0, fadeMode);
                }
                else
                {
                    if (progress >= fadeRange.end)
                    {
                        words[i].Opacify(1, fadeMode);
                    }
                    else
                    {
                        words[i].Opacify(Mathf.InverseLerp(fadeRange.start, fadeRange.end, progress), fadeMode);
                    }

                    progress = 1 - EasingUtility.Ease(progress, easingType);

                    words[i].Move(position * progress);
                }
            }
        }
    }
}