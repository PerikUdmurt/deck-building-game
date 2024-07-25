using CardBuildingGame.Gameplay.Characters;
using CardBuildingGame.Services.DI;
using System;

namespace CardBuildingGame.Infrastructure.StateMachine
{
    public class GameLoopState : IPayloadedState<DiContainer>
    {
        private readonly GameStateMachine _gameStateMachine;
        private RoundStateMachine _roundStateMachine;
        private DiContainer _sceneContainer;

        public GameLoopState(GameStateMachine gameStateMachine)
        {
            _gameStateMachine = gameStateMachine;
        }

        public void Enter(DiContainer sceneContainer)
        {
            _sceneContainer = sceneContainer;
            _roundStateMachine = new RoundStateMachine(_gameStateMachine, sceneContainer);
            _roundStateMachine.Enter<NewRoomState>();
        }

        public void Exit()
        {
            Character hero = _sceneContainer.Resolve<Character>("Hero");
            HUDController controller = _sceneContainer.Resolve<HUDController>();

            hero.Died -= controller.OnGameOver;
        }
    }
}