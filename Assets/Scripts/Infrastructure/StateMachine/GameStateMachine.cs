using CardBuildingGame.Services.DI;
using System;
using System.Collections.Generic;
using UnityEditor;

namespace CardBuildingGame.Infrastructure.StateMachine
{
    public class GameStateMachine
    {
        private Dictionary<Type, IExitableState> _states;
        private IExitableState _activeState = null;

        public GameStateMachine(DiContainer projectDiContainer)
        {
            _states = new()
            {
                [typeof(BootstrapState)] = new BootstrapState(this, projectDiContainer),
                [typeof(EnemyRoundState)] = new EnemyRoundState(this, projectDiContainer),
                [typeof(PlayerRoundState)] = new PlayerRoundState(this, projectDiContainer),
                [typeof(NewRoomState)] = new NewRoomState(this, projectDiContainer)
            };
        }

        public void Enter<TState>() where TState : class, IState
        {
            _activeState?.Exit();
            IState state = GetState<TState>();
            _activeState = state;
            state.Enter();
        }

        public void Enter<TState, TPayload>(TPayload payload) where TState : class, IPayloadedState<TPayload>
        {
            _activeState?.Exit();
            IPayloadedState<TPayload> state = GetState<TState>();
            _activeState = state;
            state.Enter(payload);
        }

        private TState GetState<TState>() where TState : class, IExitableState
        {
            return _states[typeof(TState)] as TState;
        }
    }
}