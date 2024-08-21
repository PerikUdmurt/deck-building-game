﻿// Copyright (C) 2020 - 2024 Seeley Studio. All Rights Reserved.

using Animatext.Effects;
using UnityEngine;

namespace Animatext.Examples
{
    public class CBasicA02Script : BaseEffectScript
    {
        private void Start()
        {
            CCBasicA02 a1 = ScriptableObject.CreateInstance<CCBasicA02>();

            a1.tag = tagName;
            a1.startInterval = startInverval;
            a1.reverse = true;
            a1.loopCount = 0;
            a1.loopInterval = 0.4f;
            a1.loopBackInterval = 0;
            a1.pingpongLoop = false;
            a1.continuousLoop = false;
            a1.interval = 0.275f;
            a1.singleTime = 0.575f;
            a1.sortType = SortType.SidesToMiddleFront;
            a1.startRotation = 0;
            a1.rotation = 75;
            a1.anchorType = AnchorType.Center;
            a1.anchorOffset = Vector2.zero;
            a1.easingType = EasingType.Linear;
            a1.continuousEasing = true;

            presetsA1 = new BaseEffect[] { a1 };

            CCBasicA02 a2 = ScriptableObject.CreateInstance<CCBasicA02>();

            a2.tag = tagName;
            a2.startInterval = startInverval;
            a2.reverse = false;
            a2.loopCount = 0;
            a2.loopInterval = 0.4f;
            a2.loopBackInterval = 0;
            a2.pingpongLoop = false;
            a2.continuousLoop = false;
            a2.interval = 0.275f;
            a2.singleTime = 0.575f;
            a2.sortType = SortType.MiddleToSidesBack;
            a2.startRotation = 0;
            a2.rotation = -75;
            a2.anchorType = AnchorType.Center;
            a2.anchorOffset = Vector2.zero;
            a2.easingType = EasingType.QuadInOut;
            a2.continuousEasing = true;

            presetsA2 = new BaseEffect[] { a2 };

            CCBasicA02 a3 = ScriptableObject.CreateInstance<CCBasicA02>();

            a3.tag = tagName;
            a3.startInterval = startInverval;
            a3.reverse = false;
            a3.loopCount = 0;
            a3.loopInterval = 0.5f;
            a3.loopBackInterval = 0;
            a3.pingpongLoop = false;
            a3.continuousLoop = false;
            a3.interval = 0.2f;
            a3.singleTime = 1.3f;
            a3.sortType = SortType.BackToFront;
            a3.startRotation = 0;
            a3.rotation = -360;
            a3.anchorType = AnchorType.Center;
            a3.anchorOffset = Vector2.zero;
            a3.easingType = EasingType.QuadOut;
            a3.continuousEasing = false;

            presetsA3 = new BaseEffect[] { a3 };

            CCBasicA02 a4 = ScriptableObject.CreateInstance<CCBasicA02>();

            a4.tag = tagName;
            a4.startInterval = startInverval;
            a4.reverse = true;
            a4.loopCount = 0;
            a4.loopInterval = 0.5f;
            a4.loopBackInterval = 0;
            a4.pingpongLoop = false;
            a4.continuousLoop = false;
            a4.interval = 0.2f;
            a4.singleTime = 1.3f;
            a4.sortType = SortType.FrontToBack;
            a4.startRotation = 0;
            a4.rotation = 360;
            a4.anchorType = AnchorType.Center;
            a4.anchorOffset = Vector2.zero;
            a4.easingType = EasingType.QuadIn;
            a4.continuousEasing = false;

            presetsA4 = new BaseEffect[] { a4 };

            CWBasicA02 b1 = ScriptableObject.CreateInstance<CWBasicA02>();

            b1.tag = tagName;
            b1.startInterval = startInverval;
            b1.reverse = true;
            b1.loopCount = 0;
            b1.loopInterval = 0.5f;
            b1.loopBackInterval = 0;
            b1.pingpongLoop = false;
            b1.continuousLoop = false;
            b1.interval = 0.5f;
            b1.singleTime = 1;
            b1.sortType = SortType.BackToFront;
            b1.startRotation = 0;
            b1.rotation = 9;
            b1.anchorType = AnchorType.TopLeft;
            b1.anchorOffset = Vector2.zero;
            b1.easingType = EasingType.Linear;
            b1.continuousEasing = true;

            presetsB1 = new BaseEffect[] { b1 };

            CWBasicA02 b2 = ScriptableObject.CreateInstance<CWBasicA02>();

            b2.tag = tagName;
            b2.startInterval = startInverval;
            b2.reverse = false;
            b2.loopCount = 0;
            b2.loopInterval = 0.5f;
            b2.loopBackInterval = 0;
            b2.pingpongLoop = false;
            b2.continuousLoop = false;
            b2.interval = 0.5f;
            b2.singleTime = 1;
            b2.sortType = SortType.FrontToBack;
            b2.startRotation = 0;
            b2.rotation = 9;
            b2.anchorType = AnchorType.TopRight;
            b2.anchorOffset = Vector2.zero;
            b2.easingType = EasingType.QuadInOut;
            b2.continuousEasing = true;

            presetsB2 = new BaseEffect[] { b2 };

            CWBasicA02 b3 = ScriptableObject.CreateInstance<CWBasicA02>();

            b3.tag = tagName;
            b3.startInterval = startInverval;
            b3.reverse = true;
            b3.loopCount = 0;
            b3.loopInterval = 0.5f;
            b3.loopBackInterval = 0;
            b3.pingpongLoop = true;
            b3.continuousLoop = false;
            b3.interval = 0.75f;
            b3.singleTime = 0.75f;
            b3.sortType = SortType.BackToFront;
            b3.startRotation = 0;
            b3.rotation = -9;
            b3.anchorType = AnchorType.BottomRight;
            b3.anchorOffset = Vector2.zero;
            b3.easingType = EasingType.QuadOut;
            b3.continuousEasing = true;

            presetsB3 = new BaseEffect[] { b3 };

            CWBasicA02 b4 = ScriptableObject.CreateInstance<CWBasicA02>();

            b4.tag = tagName;
            b4.startInterval = startInverval;
            b4.reverse = false;
            b4.loopCount = 0;
            b4.loopInterval = 0.5f;
            b4.loopBackInterval = 0;
            b4.pingpongLoop = true;
            b4.continuousLoop = false;
            b4.interval = 0.75f;
            b4.singleTime = 0.75f;
            b4.sortType = SortType.FrontToBack;
            b4.startRotation = 0;
            b4.rotation = -9;
            b4.anchorType = AnchorType.BottomLeft; 
            b4.anchorOffset = Vector2.zero;
            b4.easingType = EasingType.QuadIn;
            b4.continuousEasing = true;

            presetsB4 = new BaseEffect[] { b4 };

            CLBasicA02 c1 = ScriptableObject.CreateInstance<CLBasicA02>();

            c1.tag = tagName;
            c1.startInterval = startInverval;
            c1.reverse = false;
            c1.loopCount = 0;
            c1.loopInterval = 0.5f;
            c1.loopBackInterval = 0;
            c1.pingpongLoop = true;
            c1.continuousLoop = false;
            c1.interval = 1.5f;
            c1.singleTime = 1.5f;
            c1.sortType = SortType.FrontToBack;
            c1.startRotation = 0;
            c1.rotation = -6;
            c1.anchorType = AnchorType.BottomLeft;
            c1.anchorOffset = Vector2.zero;
            c1.easingType = EasingType.QuadIn;
            c1.continuousEasing = true;

            presetsC1 = new BaseEffect[] { c1 };

            CLBasicA02 c2 = ScriptableObject.CreateInstance<CLBasicA02>();

            c2.tag = tagName;
            c2.startInterval = startInverval;
            c2.reverse = true;
            c2.loopCount = 0;
            c2.loopInterval = 0.5f;
            c2.loopBackInterval = 0;
            c2.pingpongLoop = true;
            c2.continuousLoop = false;
            c2.interval = 1.5f;
            c2.singleTime = 1.5f;
            c2.sortType = SortType.FrontToBack;
            c2.startRotation = 0;
            c2.rotation = -6;
            c2.anchorType = AnchorType.BottomRight;
            c2.anchorOffset = Vector2.zero;
            c2.easingType = EasingType.QuadOut;
            c2.continuousEasing = true;

            presetsC2 = new BaseEffect[] { c2 };

            CLBasicA02 c3 = ScriptableObject.CreateInstance<CLBasicA02>();

            c3.tag = tagName;
            c3.startInterval = startInverval;
            c3.reverse = false;
            c3.loopCount = 0;
            c3.loopInterval = 0.5f;
            c3.loopBackInterval = 0;
            c3.pingpongLoop = false;
            c3.continuousLoop = false;
            c3.interval = 1.5f;
            c3.singleTime = 1.5f;
            c3.sortType = SortType.FrontToBack;
            c3.startRotation = 0;
            c3.rotation = 6;
            c3.anchorType = AnchorType.TopRight;
            c3.anchorOffset = Vector2.zero;
            c3.easingType = EasingType.QuadInOut;
            c3.continuousEasing = true;

            presetsC3 = new BaseEffect[] { c3 };

            CLBasicA02 c4 = ScriptableObject.CreateInstance<CLBasicA02>();

            c4.tag = tagName;
            c4.startInterval = startInverval;
            c4.reverse = true;
            c4.loopCount = 0;
            c4.loopInterval = 0.5f;
            c4.loopBackInterval = 0;
            c4.pingpongLoop = false;
            c4.continuousLoop = false;
            c4.interval = 1.5f;
            c4.singleTime = 1.5f;
            c4.sortType = SortType.FrontToBack;
            c4.startRotation = 0;
            c4.rotation = 6;
            c4.anchorType = AnchorType.TopLeft;
            c4.anchorOffset = Vector2.zero;
            c4.easingType = EasingType.Linear;
            c4.continuousEasing = true;

            presetsC4 = new BaseEffect[] { c4 };

            CGBasicA02 d1 = ScriptableObject.CreateInstance<CGBasicA02>();

            d1.tag = tagName;
            d1.startInterval = startInverval;
            d1.reverse = true;
            d1.loopCount = 0;
            d1.loopInterval = 0.4f;
            d1.loopBackInterval = 0;
            d1.pingpongLoop = false;
            d1.continuousLoop = false;
            d1.interval = 0.6f;
            d1.singleTime = 0.6f;
            d1.sortType = SortType.BackToFront;
            d1.startRotation = 0;
            d1.rotation = 180;
            d1.anchorType = AnchorType.Center;
            d1.anchorOffset = Vector2.zero;
            d1.easingType = EasingType.Linear;
            d1.continuousEasing = false;

            presetsD1 = new BaseEffect[] { d1 };

            CGBasicA02 d2 = ScriptableObject.CreateInstance<CGBasicA02>();

            d2.tag = tagName;
            d2.startInterval = startInverval;
            d2.reverse = false;
            d2.loopCount = 0;
            d2.loopInterval = 0.4f;
            d2.loopBackInterval = 0;
            d2.pingpongLoop = false;
            d2.continuousLoop = false;
            d2.interval = 0.6f;
            d2.singleTime = 0.6f;
            d2.sortType = SortType.FrontToBack;
            d2.startRotation = 0;
            d2.rotation = -180;
            d2.anchorType = AnchorType.Center;
            d2.anchorOffset = Vector2.zero;
            d2.easingType = EasingType.QuadInOut;
            d2.continuousEasing = false;

            presetsD2 = new BaseEffect[] { d2 };

            CGBasicA02 d3 = ScriptableObject.CreateInstance<CGBasicA02>();

            d3.tag = tagName;
            d3.startInterval = startInverval;
            d3.reverse = false;
            d3.loopCount = 0;
            d3.loopInterval = 0.4f;
            d3.loopBackInterval = 0;
            d3.pingpongLoop = false;
            d3.continuousLoop = false;
            d3.interval = 0.45f;
            d3.singleTime = 1.35f;
            d3.sortType = SortType.SidesToMiddleFront;
            d3.startRotation = 0;
            d3.rotation = 60;
            d3.anchorType = AnchorType.Center;
            d3.anchorOffset = Vector2.zero;
            d3.easingType = EasingType.QuadOut;
            d3.continuousEasing = true;

            presetsD3 = new BaseEffect[] { d3 };

            CGBasicA02 d4 = ScriptableObject.CreateInstance<CGBasicA02>();

            d4.tag = tagName;
            d4.startInterval = startInverval;
            d4.reverse = true;
            d4.loopCount = 0;
            d4.loopInterval = 0.4f;
            d4.loopBackInterval = 0;
            d4.pingpongLoop = false;
            d4.continuousLoop = false;
            d4.interval = 0.45f;
            d4.singleTime = 1.35f;
            d4.sortType = SortType.SidesToMiddleBack;
            d4.startRotation = 0;
            d4.rotation = -60;
            d4.anchorType = AnchorType.Center;
            d4.anchorOffset = Vector2.zero;
            d4.easingType = EasingType.QuadIn;
            d4.continuousEasing = true;

            presetsD4 = new BaseEffect[] { d4 };

            AddAnimatexts();
        }
    }
}