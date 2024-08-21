// Copyright (C) 2020 - 2024 Seeley Studio. All Rights Reserved.

#pragma warning disable IDE0017, IDE0090

using Animatext.Effects;
using UnityEngine;

namespace Animatext.Examples
{
    public class AutoStateScript : BaseExampleScript
    {
        private Effect effectA;
        private Effect effectB;
        private Effect effectC;
        private Effect effectD;
        private Effect effectE;

        public GameObject titleA;
        public GameObject titleB;
        public GameObject titleC;
        public GameObject titleD;
        public GameObject titleE;

        private void Start()
        {
            effectA = SetExampleA(titleA, false, false, false, false);
            effectB = SetExampleA(titleB, true, false, false, false);
            effectC = SetExampleA(titleC, true, true, false, false);
            effectD = SetExampleB(titleD, true, true, true, false);
            effectE = SetExampleB(titleE, true, true, true, true);
        }

        public void Update()
        {
            if (Time.frameCount < 2) return;

            if (titleA.activeInHierarchy && effectA.state == EffectState.Stop)
            {
                Debug.Log("No Auto State - EffectState." + effectA.state);
            }

            if (titleB.activeInHierarchy && effectB.state == EffectState.Start)
            {
                Debug.Log("AutoStart - EffectState." + effectB.state);
            }

            if (titleC.activeInHierarchy && effectC.state == EffectState.Play)
            {
                Debug.Log("AutoPlay - EffectState." + effectC.state);
            }

            if (titleD.activeInHierarchy && effectD.state == EffectState.End)
            {
                Debug.Log("AutoEnd - EffectState." + effectD.state);
            }

            if (titleE.activeInHierarchy && effectE.state == EffectState.Stop)
            {
                Debug.Log("AutoStop - EffectState." + effectE.state);
            }
        }

        private Effect SetExampleA(GameObject gameObject, bool autoStart, bool autoPlay, bool autoEnd, bool autoStop)
        {
            if (gameObject == null) return null;

            TRBasicA01 preset = ScriptableObject.CreateInstance<TRBasicA01>();

            preset.tag = string.Empty;
            preset.startInterval = 0;
            preset.reverse = false;
            preset.loopCount = 0;
            preset.loopInterval = 1;
            preset.loopBackInterval = 0;
            preset.pingpongLoop = true;
            preset.continuousLoop = false;
            preset.interval = 0.5f;
            preset.singleTime = 1;
            preset.position = new Vector2(0, 24);
            preset.easingType = EasingType.Linear;
            preset.fadeMode = ColorMode.Multiply;
            preset.fadeRange = new FloatRange(0, 1);

            Effect effect = new Effect(preset);

            effect.autoStart = autoStart;
            effect.autoPlay = autoPlay;
            effect.autoEnd = autoEnd;
            effect.autoStop = autoStop;
            effect.refreshMode = RefreshMode.Start;

            AnimatextUGUI animatext = gameObject.AddComponent<AnimatextUGUI>();

            animatext.effects.Add(effect);
            animatext.Refresh(true);

            return effect;
        }

        private Effect SetExampleB(GameObject gameObject, bool autoStart, bool autoPlay, bool autoEnd, bool autoStop)
        {
            if (gameObject == null) return null;

            TRBasicA01 preset = ScriptableObject.CreateInstance<TRBasicA01>();

            preset.tag = string.Empty;
            preset.startInterval = 0;
            preset.reverse = false;
            preset.loopCount = 2;
            preset.loopInterval = 1;
            preset.loopBackInterval = 0;
            preset.pingpongLoop = true;
            preset.continuousLoop = false;
            preset.interval = 0.5f;
            preset.singleTime = 1;
            preset.position = new Vector2(0, 24);
            preset.easingType = EasingType.Linear;
            preset.fadeMode = ColorMode.Multiply;
            preset.fadeRange = new FloatRange(0, 1);

            Effect effect = new Effect(preset);

            effect.autoStart = autoStart;
            effect.autoPlay = autoPlay;
            effect.autoEnd = autoEnd;
            effect.autoStop = autoStop;
            effect.refreshMode = RefreshMode.Start;

            AnimatextUGUI animatext = gameObject.AddComponent<AnimatextUGUI>();

            animatext.effects.Add(effect);
            animatext.Refresh(true);

            return effect;
        }

    }
}