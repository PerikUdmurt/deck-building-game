// Copyright (C) 2020 - 2024 Seeley Studio. All Rights Reserved.

#pragma warning disable IDE0017, IDE0090

using Animatext.Effects;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Animatext.Examples
{
    public class EventHelperScript : BaseExampleScript
    {
        public GameObject titleD;

        private void Start()
        {
            SetExampleD(titleD);
        }

        private void SetExampleD(GameObject gameObject)
        {
            TRBasicA01 presetA = ScriptableObject.CreateInstance<TRBasicA01>();

            presetA.tag = "AT1";
            presetA.startInterval = 0;
            presetA.reverse = false;
            presetA.loopCount = 0;
            presetA.loopInterval = 2;
            presetA.loopBackInterval = 0;
            presetA.pingpongLoop = false;
            presetA.continuousLoop = false;
            presetA.interval = 1;
            presetA.singleTime = 1;
            presetA.position = new Vector2(0, 30);
            presetA.easingType = EasingType.Linear;
            presetA.fadeMode = ColorMode.Multiply;
            presetA.fadeRange = new FloatRange(0, 0.5f);

            Effect effectA = new Effect(presetA);

            effectA.autoStart = true;
            effectA.autoPlay = true;

            TRBasicA01 presetB = ScriptableObject.CreateInstance<TRBasicA01>();

            presetB.tag = "AT2";
            presetB.startInterval = 0;
            presetB.reverse = false;
            presetB.loopCount = 0;
            presetB.loopInterval = 3;
            presetB.loopBackInterval = 0;
            presetB.pingpongLoop = false;
            presetB.continuousLoop = false;
            presetB.interval = 1;
            presetB.singleTime = 1;
            presetB.position = new Vector2(0, 30);
            presetB.easingType = EasingType.Linear;
            presetB.fadeMode = ColorMode.Multiply;
            presetB.fadeRange = new FloatRange(0, 0.5f);

            Effect effectB = new Effect(presetB);

            effectB.autoStart = true;
            effectB.autoPlay = true;

            AnimatextUGUI animatext = gameObject.AddComponent<AnimatextUGUI>();

            animatext.effects.Add(effectA);
            animatext.effects.Add(effectB);

            EffectEventHelper eventHelper = new EffectEventHelper();

            eventHelper.animatext = animatext;
            eventHelper.indexList = new List<int>() { 0, 1 };
            eventHelper.onStartEvent = new UnityEvent();
            eventHelper.onStartEvent.AddListener(DebugExampleD);
            eventHelper.AddEvents();

            animatext.SetText("<AT1>Ani</AT1><AT1>ma</AT1><AT2><color=\"#3087db\"><c>text</color></AT2>");
        }

        public void DebugExampleA()
        {
            Debug.Log("Example A - Animatext Event Helper 0 - Effect 0 - On Start Event");
        }

        public void DebugExampleB()
        {
            Debug.Log("Example B - Animatext Event Helper 0 - All Effects - On Start Event");
        }

        public void DebugExampleC()
        {
            Debug.Log("Example C - Example Helper - Animatext Event Helper 0 - Effect 0, 1 - On Start Event");
        }

        public void DebugExampleD()
        {
            Debug.Log("Example D - Effect Event Helper - Effect 0, 1 - On Start Event");
        }
    }
}