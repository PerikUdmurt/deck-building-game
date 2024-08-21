﻿// Copyright (C) 2020 - 2024 Seeley Studio. All Rights Reserved.

#pragma warning disable IDE0090

using UnityEngine;

namespace Animatext.Effects
{
    [CreateAssetMenu(menuName = "Animatext Preset/Coherence - Word/Bounce/Bounce - B05", fileName = "New CWBounceB05 Preset", order = 369)]
    public sealed class CWBounceB05 : DefaultTemplateEffect
    {
        public float singleTime = 1;
        public SortType sortType;
        public float startRotation = 0;
        public Vector2 startScale = Vector2.one;
        public Vector2 startSkew = Vector2.zero;
        public float rotation = 45;
        public Vector2 scale = Vector2.zero;
        public Vector2 skew = new Vector2(0, 45);
        public AnchorType anchorType = AnchorType.Center;
        public Vector2 anchorOffset = Vector2.zero;
        public int bounces = 2;
        public float bounciness = 0.5f;
        public EasingType easingType;

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

                progress = EasingUtility.Ease(progress, easingType);
                progress = EasingUtility.Bounce(progress, bounces, bounciness);

                Vector2 anchorPoint = words[i].GetAnchorPoint(anchorType) + anchorOffset;

                words[i].Rotate(Mathf.LerpUnclamped(startRotation, rotation, progress), anchorPoint);
                words[i].Scale(Vector2.LerpUnclamped(startScale, scale, progress), anchorPoint);
                words[i].Skew(Vector2.LerpUnclamped(startSkew, skew, progress), anchorPoint);
            }
        }
    }
}