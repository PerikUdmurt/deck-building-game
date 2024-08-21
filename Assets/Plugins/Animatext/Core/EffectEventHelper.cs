// Copyright (C) 2020 - 2024 Seeley Studio. All Rights Reserved.

using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.Events;

namespace Animatext
{
    [Serializable]
    public class EffectEventHelper
    {
        public BaseAnimatext animatext;
        public List<int> indexList;
        public UnityEvent onEndEvent;
        public UnityEvent onPauseEvent;
        public UnityEvent onPlayEvent;
        public UnityEvent onProceedEvent;
        public UnityEvent onStartEvent;
        public UnityEvent onStopEvent;

        public EffectEventHelper() { }

        public EffectEventHelper(params int[] array)
        {
            indexList = array.ToList();
        }

        public EffectEventHelper(BaseAnimatext animatext, params int[] array)
        {
            this.animatext = animatext;

            indexList = array.ToList();
        }

        public void Add(Effect effect)
        {
            if (effect == null) return;

            if (onEndEvent != null)
            {
                effect.onEnd += onEndEvent.Invoke;
            }

            if (onPauseEvent != null)
            {
                effect.onPause += onPauseEvent.Invoke;
            }

            if (onPlayEvent != null)
            {
                effect.onPlay += onPlayEvent.Invoke;
            }

            if (onProceedEvent != null)
            {
                effect.onProceed += onProceedEvent.Invoke;
            }

            if (onStartEvent != null)
            {
                effect.onStart += onStartEvent.Invoke;
            }

            if (onStopEvent != null)
            {
                effect.onStop += onStopEvent.Invoke;
            }
        }

        public void AddEvents()
        {
            if (animatext == null || indexList == null) return;

            int count = animatext.effects.Count;

            if (indexList.Contains(-1))
            {
                for (int i = 0; i < count; i++)
                {
                    Add(animatext.effects[i]);
                }
            }
            else
            {
                for (int i = 0; i < indexList.Count; i++)
                {
                    int index = indexList[i];

                    if (index < count && index > -1)
                    {
                        Add(animatext.effects[index]);
                    }
                }
            }
        }

        public void Remove(Effect effect)
        {
            if (effect == null) return;

            if (onEndEvent != null)
            {
                effect.onEnd -= onEndEvent.Invoke;
            }

            if (onPauseEvent != null)
            {
                effect.onPause -= onPauseEvent.Invoke;
            }

            if (onPlayEvent != null)
            {
                effect.onPlay -= onPlayEvent.Invoke;
            }

            if (onProceedEvent != null)
            {
                effect.onProceed -= onProceedEvent.Invoke;
            }

            if (onStartEvent != null)
            {
                effect.onStart -= onStartEvent.Invoke;
            }

            if (onStopEvent != null)
            {
                effect.onStop -= onStopEvent.Invoke;
            }
        }

        public void RemoveEvents()
        {
            if (animatext == null || indexList == null) return;

            int count = animatext.effects.Count;

            if (indexList.Contains(-1))
            {
                for (int i = 0; i < count; i++)
                {
                    Remove(animatext.effects[i]);
                }
            }
            else
            {
                for (int i = 0; i < indexList.Count; i++)
                {
                    int index = indexList[i];

                    if (index < count && index > -1)
                    {
                        Remove(animatext.effects[index]);
                    }
                }
            }
        }
    }
}