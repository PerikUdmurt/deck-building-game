﻿// Copyright (C) 2020 - 2024 Seeley Studio. All Rights Reserved.

#pragma warning disable IDE0090

using UnityEngine;

namespace Animatext.Effects
{
    [CreateAssetMenu(menuName = "Animatext Preset/Transition - Range/Basic/Basic - B01", fileName = "New TRBasicB01 Preset", order = 369)]
    public sealed class TRBasicB01 : DefaultTemplateEffect
    {
        public float singleTime = 1;
        public Vector2 position = new Vector2(0, 100);
        public float rotation = 180;
        public Vector2 scale = Vector2.zero;
        public AnchorType anchorType = AnchorType.Center;
        public Vector2 anchorOffset = Vector2.zero;
        public EasingType easingType;
        [FadeMode] public ColorMode fadeMode = ColorMode.Multiply;
        public FloatRange fadeRange = new FloatRange(0, 0.5f);

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

            if (progress <= fadeRange.start)
            {
                range.Opacify(0, fadeMode);
            }
            else
            {
                if (progress >= fadeRange.end)
                {
                    range.Opacify(1, fadeMode);
                }
                else
                {
                    range.Opacify(Mathf.InverseLerp(fadeRange.start, fadeRange.end, progress), fadeMode);
                }

                progress = 1 - EasingUtility.Ease(progress, easingType);

                Vector2 anchorPoint = range.GetAnchorPoint(anchorType) + anchorOffset;

                range.Move(position * progress);
                range.Rotate(rotation * progress, anchorPoint);
                range.Scale(Vector2.LerpUnclamped(Vector2.one, scale, progress), anchorPoint);
            }
        }
    }
}