﻿// Copyright (C) 2020 - 2024 Seeley Studio. All Rights Reserved.

using UnityEngine;

namespace Animatext.Effects
{
    [CreateAssetMenu(menuName = "Animatext Preset/Coherence - Line/Step/Step - A02", fileName = "New CLStepA02 Preset", order = 369)]
    public sealed class CLStepA02 : DefaultTemplateEffect
    {
        public float singleTime = 1;
        public SortType sortType;
        public float startRotation = 0;
        public float rotation = 45;
        public AnchorType anchorType = AnchorType.Center;
        public Vector2 anchorOffset = Vector2.zero;
        public int steps = 5;
        public StepType stepType = StepType.Round;
        public EasingType easingType;
        public bool continuousEasing = true;

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
                progress = EasingUtility.EaseStep(progress, steps, stepType);
                progress = EasingUtility.Basic(progress, continuousEasing);

                lines[i].Rotate(Mathf.LerpUnclamped(startRotation, rotation, progress), lines[i].GetAnchorPoint(anchorType) + anchorOffset);
            }
        }
    }
}