// Copyright (C) 2020 - 2024 Seeley Studio. All Rights Reserved.

using Animatext.Effects;
using System.Collections.Generic;
using UnityEngine;

namespace Animatext
{
    public class ExecutionEffect
    {
        public BaseEffect[] presets;
        public Effect effect;
        public BaseAnimatext animatext;
        public int rangeCount;
        public Range[] ranges;
        public int executionRangeCount;
        public ExecutionRange[] executionRanges;
        public bool effectTimeDirty;
        public bool totalIntervalDirty;
        public float effectTime;
        public float totalInterval;
        public bool isEnd;
        public bool isProceed;

        private float GetEffectTime()
        {
            float effectTime = 0;

            for (int i = 0; i < executionRangeCount; i++)
            {
                effectTime = Mathf.Max(effectTime, executionRanges[i].GetRangeTime());
            }

            return effectTime;
        }

        private float GetTotalInterval()
        {
            float totalInterval = 0;

            for (int i = 0; i < rangeCount; i++)
            {
                int startIndex = ranges[i].startIndex;
                int endIndex = ranges[i].endIndex;

                float executionTotalInterval;

                if (startIndex > endIndex)
                {
                    executionTotalInterval = 0;
                }
                else
                {
                    executionTotalInterval = executionRanges[endIndex].startInterval + executionRanges[endIndex].executionInterval;

                    for (int j = endIndex - 1; j >= startIndex; j--)
                    {
                        executionTotalInterval += executionRanges[j].rangeInterval;

                        if (executionTotalInterval < executionRanges[j].executionInterval)
                        {
                            executionTotalInterval = executionRanges[j].executionInterval;
                        }

                        executionTotalInterval += executionRanges[j].startInterval;
                    }

                    if (executionRanges[startIndex].openingIntervalType != IntervalType.Replace && executionRanges[startIndex].openingIntervalType != IntervalType.Cover && startIndex > 0)
                    {
                        executionTotalInterval += executionRanges[startIndex - 1].rangeInterval;
                    }
                }

                totalInterval = Mathf.Max(totalInterval, executionTotalInterval);
            }

            return totalInterval;
        }

        public void Execute(float time)
        {
            bool checkEnd = true;
            float proceedTime = time;
            float frontInterval = 0;

            for (int i = 0; i < executionRangeCount; i++)
            {
                executionRanges[i].EarlyExecute(ref proceedTime, ref frontInterval);
            }

            if (totalIntervalDirty)
            {
                totalInterval = GetTotalInterval();
                totalIntervalDirty = false;
            }

            for (int i = 0; i < executionRangeCount; i++)
            {
                executionRanges[i].BeforeExecute();
                checkEnd &= executionRanges[i].isEnd;
            }

            if (effectTimeDirty)
            {
                effectTime = GetEffectTime();
                effectTimeDirty = false;
            }

            for (int i = 0; i < executionRangeCount; i++)
            {
                executionRanges[i].Execute();
            }

            isEnd = checkEnd;
            isProceed = proceedTime > 0;
        }

        public void SetIntervalDirty()
        {
            totalIntervalDirty = true;
            effectTimeDirty = true;
        }

        public void SetTimeDirty()
        {
            effectTimeDirty = true;
        }

        public void UpdateEffectInfo(BaseEffect[] presets, Effect effect, BaseAnimatext animatext)
        {
            this.presets = presets;
            this.effect = effect;
            this.animatext = animatext;
        }

        public void UpdateExecutionData(ExecutionRange[] executionData)
        {
            executionRanges = executionData;

            if (executionRanges == null)
            {
                executionRangeCount = 0;
                rangeCount = 0;
            }
            else
            {
                executionRangeCount = executionRanges.Length;

                int startIndex = 0;
                List<Range> executionRangeList = new List<Range>();

                for (int i = 0; i < executionRangeCount; i++)
                {
                    switch (executionRanges[i].openingIntervalType)
                    {
                        case IntervalType.Replace:
                        case IntervalType.Cover:
                            executionRangeList.Add(new Range(startIndex, i - 1));
                            startIndex = i;
                            break;

                        default:
                            break;
                    }

                    switch (executionRanges[i].closingIntervalType)
                    {
                        case IntervalType.Replace:
                        case IntervalType.Cover:
                            executionRangeList.Add(new Range(startIndex, i));
                            startIndex = i + 1;
                            break;

                        default:
                            break;
                    }
                }

                if (startIndex != executionRangeCount)
                {
                    executionRangeList.Add(new Range(startIndex, executionRangeCount - 1));
                }

                ranges = executionRangeList.ToArray();
                rangeCount = ranges.Length;
            }

            SetIntervalDirty();
        }
    }
}