// Copyright (C) 2020 - 2024 Seeley Studio. All Rights Reserved.

#pragma warning disable IDE0017, IDE0090

using Animatext.Effects;
using UnityEngine;

namespace Animatext.Examples
{
    public class EffectStateScript : BaseExampleScript
    {
        public GameObject titleA1;
        public GameObject titleA2;
        public GameObject titleA3;
        public GameObject titleA4;
        public GameObject titleA5;
        public GameObject titleB1;
        public GameObject titleB2;
        public GameObject titleB3;
        public GameObject titleB4;
        public GameObject titleB5;

        private void Start()
        {
            SetExample(titleA1, EffectState.Stop, false);
            SetExample(titleA2, EffectState.Start, false);
            SetExample(titleA3, EffectState.Play, false);
            SetExample(titleA4, EffectState.Pause, false);
            SetExample(titleA5, EffectState.End, false);
            SetExample(titleB1, EffectState.Stop, true);
            SetExample(titleB2, EffectState.Start, true);
            SetExample(titleB3, EffectState.Play, true);
            SetExample(titleB4, EffectState.Pause, true);
            SetExample(titleB5, EffectState.End, true);
        }

        private void SetExample(GameObject gameObject, EffectState effectState, bool reverse)
        {
            if (gameObject == null) return;

            TRBasicA01 preset = ScriptableObject.CreateInstance<TRBasicA01>();

            preset.tag = string.Empty;
            preset.startInterval = 0;
            preset.reverse = reverse;
            preset.loopCount = 0;
            preset.loopInterval = 1;
            preset.loopBackInterval = 0;
            preset.pingpongLoop = false;
            preset.continuousLoop = false;
            preset.interval = 0.5f;
            preset.singleTime = 1;
            preset.position = new Vector2(0, 24);
            preset.easingType = EasingType.Linear;
            preset.fadeMode = ColorMode.Multiply;
            preset.fadeRange = new FloatRange(0, 1);

            Effect effect = new Effect(preset);

            effect.autoStart = false;
            effect.autoPlay = false;
            effect.refreshMode = RefreshMode.Start;
            effect.time = 0;

            AnimatextUGUI animatext = gameObject.AddComponent<AnimatextUGUI>();

            animatext.effects.Add(effect);
            animatext.Refresh(true);

            switch (effectState)
            {
                case EffectState.Stop:
                    break;

                case EffectState.Start:
                    effect.Start();
                    break;

                case EffectState.Play:
                    effect.Play();
                    break;

                case EffectState.Pause:
                    effect.time = 0.5f;
                    effect.Play();
                    effect.Pause();
                    break;

                case EffectState.End:
                    effect.End();
                    break;

                default:
                    break;
            }
        }
    }
}