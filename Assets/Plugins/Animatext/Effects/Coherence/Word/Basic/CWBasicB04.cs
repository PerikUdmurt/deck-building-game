﻿// Copyright (C) 2020 - 2024 Seeley Studio. All Rights Reserved.

#pragma warning disable IDE0090

using UnityEngine;

namespace Animatext.Effects
{
    [CreateAssetMenu(menuName = "Animatext Preset/Coherence - Word/Basic/Basic - B04", fileName = "New CWBasicB04 Preset", order = 369)]
    public sealed class CWBasicB04 : DefaultTemplateEffect
    {
        public float singleTime = 1;
        public SortType sortType;
        public Vector2 startPosition = Vector2.zero;
        public float startRotation = 0;
        public Vector2 startScale = Vector2.one;
        public Vector2 position = new Vector2(0, 100);
        public float rotation = 180;
        public Vector2 scale = Vector2.zero;
        public AnchorType anchorType = AnchorType.Center;
        public Vector2 anchorOffset = Vector2.zero;
        public EasingType easingType;
        public bool continuousEasing = true;

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
                progress = EasingUtility.Basic(progress, continuousEasing);

                Vector2 anchorPoint = words[i].GetAnchorPoint(anchorType) + anchorOffset;

                words[i].Scale(Vector2.LerpUnclamped(startScale, scale, progress), anchorPoint);
                words[i].Rotate(Mathf.LerpUnclamped(startRotation, rotation, progress), anchorPoint);
                words[i].Move(Vector2.LerpUnclamped(startPosition, position, progress));
            }
        }
    }
}