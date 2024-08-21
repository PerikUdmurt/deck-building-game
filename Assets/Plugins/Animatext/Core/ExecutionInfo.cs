﻿// Copyright (C) 2020 - 2024 Seeley Studio. All Rights Reserved.

using Animatext.Effects;
using UnityEngine;

namespace Animatext
{
    public class ExecutionInfo
    {
        public ExecutionEffect executionEffect;
        public float lastTime;
        public float time;
        public int charCount;
        public UnitInfo[] chars;
        public int characterCount;
        public UnitInfo[] characters;
        public int wordCount;
        public UnitInfo[] words;
        public int lineCount;
        public UnitInfo[] lines;
        public int groupCount;
        public UnitInfo[] groups;
        public bool rangeAnchor;
        public UnitInfo range;
        public BaseEffect preset;
        public float frontInterval;
        public float startInterval;
        public float rangeInterval;
        public float executionInterval;
        public float endInterval;
        public bool isEnd;

        public void BeforeExecute()
        {
            preset.BeforeExecute(this);
        }

        public void EarlyExecute(float time, float frontInterval)
        {
            lastTime = this.time;
            this.time = Mathf.Max(0, time);
            this.frontInterval = frontInterval;

            preset.EarlyExecute(this);
        }

        public void Execute()
        {
            preset.Execute(this);
        }

        public void SetEndInterval(float interval)
        {
            if (endInterval != interval)
            {
                endInterval = interval;
                executionEffect.SetTimeDirty();
            }

            isEnd = endInterval <= time;
        }

        public void SetExecutionInterval(float interval)
        {
            if (executionInterval != interval)
            {
                executionInterval = interval;
                executionEffect.SetIntervalDirty();
            }
        }

        public void SetStartInterval(float interval)
        {
            if (startInterval != interval)
            {
                startInterval = interval;
                executionEffect.SetIntervalDirty();
            }
        }

        public void SetRangeInterval(float interval)
        {
            if (rangeInterval != interval)
            {
                rangeInterval = interval;
                executionEffect.SetIntervalDirty();
            }
        }

        public void UpdateAnchor()
        {
            for (int i = 0; i < charCount; i++)
            {
                chars[i].UpdateAnchor();
            }

            for (int i = 0; i < characterCount; i++)
            {
                characters[i].UpdateAnchor();
            }

            for (int i = 0; i < wordCount; i++)
            {
                words[i].UpdateAnchor();
            }

            for (int i = 0; i < lineCount; i++)
            {
                lines[i].UpdateAnchor();
            }

            for (int i = 0; i < groupCount; i++)
            {
                groups[i].UpdateAnchor();
            }

            if (rangeAnchor)
            {
                range.UpdateAnchor();
            }
        }
    }
}