﻿// Copyright (C) 2020 - 2024 Seeley Studio. All Rights Reserved.

using UnityEngine;

namespace Animatext.Effects
{
    [CreateAssetMenu(menuName = "Animatext Preset/Coherence - Range/Fade/Fade - A03", fileName = "New CRFadeA03 Preset", order = 369)]
    public sealed class CRFadeA03 : DefaultTemplateEffect
    {
        public float singleTime = 1;
        public float startOpacity = 1;
        public float opacity = 0;
        public int steps = 5;
        public StepType stepType = StepType.Round;
        public EasingType easingType;
        [FadeMode] public ColorMode fadeMode = ColorMode.Multiply;

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
            progress = EasingUtility.EaseStep(progress, steps, stepType);
            progress = EasingUtility.Basic(progress, true);

            range.Opacify(Mathf.LerpUnclamped(startOpacity, opacity, progress), fadeMode);
        }
    }
}