// Copyright (C) 2020 - 2024 Seeley Studio. All Rights Reserved.

#pragma warning disable IDE0017, IDE0090

using Animatext.Effects;
using System.Collections;
using UnityEngine;

namespace Animatext.Examples
{
    public class EventsScript : BaseExampleScript
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
            effectA = SetExampleA(titleA);
            effectB = SetExampleB(titleB);
            effectC = SetExampleC(titleC);
            effectD = SetExampleD(titleD);
            effectE = SetExampleE(titleE);
        }

        private Effect SetExampleA(GameObject gameObject)
        {
            if (gameObject == null) return null;

            TCBasicA01 preset = ScriptableObject.CreateInstance<TCBasicA01>();

            preset.tag = string.Empty;
            preset.startInterval = 0;
            preset.reverse = false;
            preset.loopCount = 0;
            preset.loopInterval = 1;
            preset.loopBackInterval = 0;
            preset.pingpongLoop = false;
            preset.continuousLoop = false;
            preset.interval = 0.5f;
            preset.singleTime = 1;
            preset.position = new Vector2(0, 45);
            preset.easingType = EasingType.Linear;
            preset.fadeMode = ColorMode.Multiply;
            preset.fadeRange = new FloatRange(0, 1);

            Effect effect = new Effect(preset);

            effect.autoStart = false;
            effect.autoPlay = false;
            effect.onStart += LogOnStart;

            BaseAnimatext animatext = gameObject.AddComponent<AnimatextUGUI>();

            animatext.effects.Add(effect);
            animatext.Refresh(true);
            
            StartCoroutine(StartEffects(animatext));

            return effect;
        }

        private Effect SetExampleB(GameObject gameObject)
        {
            if (gameObject == null) return null;

            TCBasicA01 preset = ScriptableObject.CreateInstance<TCBasicA01>();

            preset.tag = string.Empty;
            preset.startInterval = 0;
            preset.reverse = false;
            preset.loopCount = 0;
            preset.loopInterval = 1;
            preset.loopBackInterval = 0;
            preset.pingpongLoop = false;
            preset.continuousLoop = false;
            preset.interval = 0.5f;
            preset.singleTime = 1;
            preset.position = new Vector2(0, 45);
            preset.easingType = EasingType.Linear;
            preset.fadeMode = ColorMode.Multiply;
            preset.fadeRange = new FloatRange(0, 1);

            Effect effect = new Effect(preset);

            effect.autoStart = false;
            effect.autoPlay = false;
            effect.onPlay += LogOnPlay;
            effect.onProceed += LogOnProceed;

            BaseAnimatext animatext = gameObject.AddComponent<AnimatextUGUI>();

            animatext.effects.Add(effect);
            animatext.Refresh(true);
            
            StartCoroutine(PlayEffects(animatext));

            return effect;
        }

        private Effect SetExampleC(GameObject gameObject)
        {
            if (gameObject == null) return null;

            TCBasicA01 preset = ScriptableObject.CreateInstance<TCBasicA01>();

            preset.tag = string.Empty;
            preset.startInterval = 0;
            preset.reverse = false;
            preset.loopCount = 0;
            preset.loopInterval = 1;
            preset.loopBackInterval = 0;
            preset.pingpongLoop = false;
            preset.continuousLoop = false;
            preset.interval = 0.5f;
            preset.singleTime = 1;
            preset.position = new Vector2(0, 45);
            preset.easingType = EasingType.Linear;
            preset.fadeMode = ColorMode.Multiply;
            preset.fadeRange = new FloatRange(0, 1);

            Effect effect = new Effect(preset);

            effect.autoStart = false;
            effect.autoPlay = false;
            effect.onPause += LogOnPause;
            effect.time = 2.5f;

            BaseAnimatext animatext = gameObject.AddComponent<AnimatextUGUI>();

            animatext.effects.Add(effect);
            animatext.Refresh(true);
            
            StartCoroutine(PauseEffects(animatext));

            return effect;
        }

        private Effect SetExampleD(GameObject gameObject)
        {
            if (gameObject == null) return null;

            TCBasicA01 preset = ScriptableObject.CreateInstance<TCBasicA01>();

            preset.tag = string.Empty;
            preset.startInterval = 0;
            preset.reverse = false;
            preset.loopCount = 0;
            preset.loopInterval = 1;
            preset.loopBackInterval = 0;
            preset.pingpongLoop = false;
            preset.continuousLoop = false;
            preset.interval = 0.5f;
            preset.singleTime = 1;
            preset.position = new Vector2(0, 45);
            preset.easingType = EasingType.Linear;
            preset.fadeMode = ColorMode.Multiply;
            preset.fadeRange = new FloatRange(0, 1);

            Effect effect = new Effect(preset);

            effect.autoStart = false;
            effect.autoPlay = false;
            effect.onEnd += LogOnEnd;

            BaseAnimatext animatext = gameObject.AddComponent<AnimatextUGUI>();

            animatext.effects.Add(effect);
            animatext.Refresh(true);
            
            StartCoroutine(EndEffects(animatext));

            return effect;
        }

        private Effect SetExampleE(GameObject gameObject)
        {
            if (gameObject == null) return null;

            TCBasicA01 preset = ScriptableObject.CreateInstance<TCBasicA01>();

            preset.tag = string.Empty;
            preset.startInterval = 0;
            preset.reverse = false;
            preset.loopCount = 0;
            preset.loopInterval = 1;
            preset.loopBackInterval = 0;
            preset.pingpongLoop = false;
            preset.continuousLoop = false;
            preset.interval = 0.5f;
            preset.singleTime = 1;
            preset.position = new Vector2(0, 45);
            preset.easingType = EasingType.Linear;
            preset.fadeMode = ColorMode.Multiply;
            preset.fadeRange = new FloatRange(0, 1);

            Effect effect = new Effect(preset);

            effect.autoStart = false;
            effect.autoPlay = false;
            effect.onStop += LogOnStop;

            BaseAnimatext animatext = gameObject.AddComponent<AnimatextUGUI>();

            animatext.effects.Add(effect);
            animatext.Refresh(true);
            
            StartCoroutine(StopEffects(animatext));

            return effect;
        }

        private IEnumerator StartEffects(BaseAnimatext animatext)
        {
            while (true)
            {
                yield return null;

                if (Time.frameCount >= 3)
                {
                    animatext.StartEffects();

                    break;
                }
            }
        }

        private IEnumerator PlayEffects(BaseAnimatext animatext)
        {
            while (true)
            {
                yield return null;

                if (Time.frameCount >= 3)
                {
                    animatext.PlayEffects();

                    break;
                }
            }
        }

        private IEnumerator PauseEffects(BaseAnimatext animatext)
        {
            while (true)
            {
                yield return null;

                if (Time.frameCount >= 3)
                {
                    animatext.PlayEffects();
                    animatext.PauseEffects();

                    break;
                }
            }
        }

        private IEnumerator EndEffects(BaseAnimatext animatext)
        {
            while (true)
            {
                yield return null;

                if (Time.frameCount >= 3)
                {
                    animatext.EndEffects();

                    break;
                }
            }
        }

        private IEnumerator StopEffects(BaseAnimatext animatext)
        {
            while (true)
            {
                yield return null;

                if (Time.frameCount >= 3)
                {
                    animatext.PlayEffects();
                    animatext.StopEffects();

                    break;
                }
            }
        }

        private void LogOnStart()
        {
            Debug.Log("OnStart - EffectState." + effectA.state);
        }

        private void LogOnPlay()
        {
            Debug.Log("OnPlay - EffectState." + effectB.state);
        }

        private void LogOnProceed()
        {
            Debug.Log("OnProceed - EffectState." + effectB.state);
        }

        private void LogOnPause()
        {
            Debug.Log("OnPause - EffectState." + effectC.state);
        }

        private void LogOnEnd()
        {
            Debug.Log("OnEnd - EffectState." + effectD.state);
        }

        private void LogOnStop()
        {
            Debug.Log("OnStop - EffectState." + effectE.state);
        }
    }
}