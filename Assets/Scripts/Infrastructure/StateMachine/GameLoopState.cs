using CardBuildingGame.Gameplay.Characters;
using CardBuildingGame.Infrastructure.Factories;
using CardBuildingGame.Services.DI;
using System;
using YGameTemplate.Infrastructure.AssetProviders;
using YGameTemplate.Infrastructure.Score;

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
            CleanUp();
        }

        private void CleanUp()
        {
            _sceneContainer.Resolve<IAssetProvider>().CleanUp();
            _sceneContainer.Resolve<ICardSpawner>().CleanUp();
            _sceneContainer.Resolve<ScoreSystem>().CleanUp();
            _sceneContainer.Resolve<HUDController>().CleanUp();
        }
    }
}