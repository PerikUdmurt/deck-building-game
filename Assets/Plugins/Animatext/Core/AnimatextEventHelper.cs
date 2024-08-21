// Copyright (C) 2020 - 2024 Seeley Studio. All Rights Reserved.

using System.Collections.Generic;
using UnityEngine;

namespace Animatext
{
    [DefaultExecutionOrder(-9)]
    [AddComponentMenu("Miscellaneous/Animatext/Animatext - Event Helper")]
    public class AnimatextEventHelper : MonoBehaviour
    {
        [SerializeField] private List<EffectEventHelper> _helpers = new List<EffectEventHelper>();

        public List<EffectEventHelper> helpers
        {
            get { return _helpers; }
        }

        private void OnEnable()
        {
            AddEvents();
        }

        private void OnDisable()
        {
            RemoveEvents();
        }

        public void AddEvents()
        {
            if (TryGetComponent(out BaseAnimatext animatext))
            {
                for (int i = 0; i < _helpers.Count; i++)
                {
                    if (_helpers[i].animatext == null)
                    {
                        _helpers[i].animatext = animatext;
                    }
                }
            }

            for (int i = 0; i < _helpers.Count; i++)
            {
                _helpers[i].AddEvents();
            }
        }

        public void RemoveEvents()
        {
            for (int i = 0; i < _helpers.Count; i++)
            {
                _helpers[i].RemoveEvents();
            }
        }
    }
}