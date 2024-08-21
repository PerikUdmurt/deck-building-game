﻿// Copyright (C) 2020 - 2024 Seeley Studio. All Rights Reserved.

#pragma warning disable IDE0090, IDE1006

using Animatext.Effects;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Animatext
{
    [Serializable]
    public class Effect
    {
        private bool _proceed;
        private ExecutionEffect _executionEffect;
        [SerializeField] private BaseEffect[] _presets;
        [SerializeField] private bool _autoEnd;
        [SerializeField] private bool _autoPlay;
        [SerializeField] private bool _autoStart;
        [SerializeField] private bool _autoStop;
        [SerializeField] private RefreshMode _refreshMode;
        [SerializeField] private EffectState _state;
        [SerializeField] private float _speed = 1f;
        [SerializeField] private float _time;

        public Effect(params BaseEffect[] presets)
        {
            _presets = presets;
            _autoStart = true;
            _autoPlay = true;
            _refreshMode = RefreshMode.Start;
        }

        public bool autoEnd
        {
            get { return _autoEnd; }
            set { _autoEnd = value; }
        }

        public bool autoPlay
        {
            get { return _autoPlay; }
            set { _autoPlay = value; }
        }

        public bool autoStart
        {
            get { return _autoStart; }
            set { _autoStart = value; }
        }

        public bool autoStop
        {
            get { return _autoStop; }
            set { _autoStop = value; }
        }

        public EffectState state
        {
            get { return _state; }
        }

        public RefreshMode refreshMode
        {
            get { return _refreshMode; }
            set { _refreshMode = value; }
        }

        public float speed
        {
            get { return _speed; }
            set { _speed = value; }
        }

        public float time
        {
            get { return _time; }
            set { _time = value; }
        }

        public event Action onEnd;
        public event Action onPause;
        public event Action onPlay;
        public event Action onProceed;
        public event Action onStart;
        public event Action onStop;

        private void BaseExecute(float time)
        {
            _executionEffect.Execute(time);

            if (_executionEffect.isProceed && !_proceed)
            {
                _proceed = true;

                onProceed?.Invoke();
            }

            if (_executionEffect.isEnd && _autoEnd && _state == EffectState.Play)
            {
                End();
            }
        }

        private void Reset()
        {
            _time = 0;
            _proceed = false;
        }

        public void End()
        {
            if (_state != EffectState.End)
            {
                _state = EffectState.End;

                onEnd?.Invoke();
            }
        }

        public void Execute()
        {
            switch (_state)
            {
                case EffectState.Start:
                    if (_autoPlay)
                    {
                        Play();
                        BaseExecute(_time);
                    }
                    else
                    {
                        BaseExecute(0);
                    }
                    break;

                case EffectState.Play:
                    _time += Time.deltaTime * _speed;
                    BaseExecute(_time);
                    break;

                case EffectState.Pause:
                    BaseExecute(_time);
                    break;

                case EffectState.End:
                    if (_autoStop)
                    {
                        Stop();
                    }
                    else
                    {
                        BaseExecute(float.PositiveInfinity);
                    }
                    break;

                default:
                    return;
            }
        }

        public void ExtraExecute()
        {
            switch (_state)
            {
                case EffectState.Start:
                    BaseExecute(0);
                    break;

                case EffectState.Play:
                case EffectState.Pause:
                    BaseExecute(_time);
                    break;

                case EffectState.End:
                    BaseExecute(float.PositiveInfinity);
                    break;

                default:
                    break;
            }
        }

        public InfoFlags GetInfoFlags()
        {
            InfoFlags infoFlags = InfoFlags.None;

            if (_presets != null)
            {
                for (int i = 0; i < _presets.Length; i++)
                {
                    if (_presets[i] != null)
                    {
                        infoFlags |= _presets[i].infoFlags;
                    }
                }
            }

            return infoFlags;
        }

        public HashSet<string> GetTags()
        {
            HashSet<string> tags = new HashSet<string>();

            if (_presets != null)
            {
                for (int i = 0; i < _presets.Length; i++)
                {
                    if (_presets[i] != null)
                    {
                        tags.Add(_presets[i].tag ?? string.Empty);
                    }
                }
            }

            return tags;
        }

        public void Pause()
        {
            if (_state == EffectState.Start || _state == EffectState.Play)
            {
                _state = EffectState.Pause;

                onPause?.Invoke();
            }
        }

        public void Play()
        {
            if (_state != EffectState.Play)
            {
                _state = EffectState.Play;

                onPlay?.Invoke();
            }
            else
            {
                Reset();
            }
        }

        public void Refresh()
        {
            if (_state == EffectState.Stop)
            {
                if (_autoStart)
                {
                    Start();
                }
            }
            else
            {
                switch (_refreshMode)
                {
                    case RefreshMode.Start:
                        Start();
                        break;

                    case RefreshMode.Replay:
                        Play();
                        break;

                    case RefreshMode.Pause:
                        Pause();
                        break;

                    case RefreshMode.End:
                        End();
                        break;

                    default:
                        break;
                }
            }
        }

        public void Start()
        {
            if (_state != EffectState.Start)
            {
                if (_state != EffectState.Stop)
                {
                    Reset();
                }

                _state = EffectState.Start;

                onStart?.Invoke();
            }
        }

        public void Stop()
        {
            if (_state != EffectState.Stop)
            {
                Reset();

                _state = EffectState.Stop;
                
                onStop?.Invoke();
            }
        }

        public void Update(TextInfo textInfo, BaseAnimatext animatext)
        {
            if (_executionEffect == null)
            {
                _executionEffect = new ExecutionEffect();
            }

            _executionEffect.UpdateEffectInfo(_presets, this, animatext);
            _executionEffect.UpdateExecutionData(textInfo.GenerateExecutionRanges(_executionEffect));

            if (_state == EffectState.Stop)
            {
                if (_autoStart)
                {
                    Start();
                }
            }
        }
    }
}