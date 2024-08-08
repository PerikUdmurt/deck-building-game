using CardBuildingGame.Infrastructure.GameScenario;
using CardBuildingGame.Services;
using CardBuildingGame.Services.DI;
using System.Linq;
using YGameTemplate.Infrastructure.Score;
using YGameTemplate.Services.StatisticsService;

namespace CardBuildingGame.Infrastructure.StateMachine
{
    public class GameModeRegistrateState : IPayloadedState<GameMode>
    {
        private readonly GameStateMachine _gameStateMachine;
        private readonly DiContainer _projectContainer;
        private DiContainer _sessionContainer;

        public GameModeRegistrateState(GameStateMachine gameStateMachine, DiContainer projectContainer) 
        {
            _gameStateMachine = gameStateMachine;
            _projectContainer = projectContainer;
        }

        public void Enter(GameMode payload)
        {
            switch (payload)
            {
                case GameMode.Test:
                    RegistrateTestMode();
                    break;
                case GameMode.Infinite:
                    RegistrateInfiniteMode();
                    break;
            }
        }

        public void Exit()
        {

        }

        private void RegistrateTestMode()
        {
            RegistrateGameSessionContainer();
            RegistrateScenarioService(GameMode.Infinite);
            RegisterScoreSystem();
            LoadFirstLevel();
        }

        private void RegistrateInfiniteMode()
        {
            
        }

        private void LoadFirstLevel()
        {
            RoomStaticData roomData = _sessionContainer.Resolve<ScenarioService>().GetRoomScripts(1).First();
            LevelData levelData = new LevelData
                (
                roomData: roomData,
                sessionDiContainer: _sessionContainer,
                currentFloor: 1
                );

            _gameStateMachine.Enter<LoadLevelState, LevelData>(levelData);
        }

        private void RegistrateScenarioService(GameMode gameMode)
        {
            ScenarioService scenarioService = new ScenarioService
                            (gameMode: gameMode,
                            staticDataService: _projectContainer.Resolve<IStaticDataService>());

            _sessionContainer.RegisterInstance(scenarioService);
        }

        private void RegistrateGameSessionContainer() 
            => _sessionContainer = new (_projectContainer);

        private void RegisterScoreSystem()
        {
            RegisterIntermidiateStatistics();

            ScoreSystem scoreSystem = new ScoreSystem
                (_sessionContainer.Resolve<GameStatisticsService>());
            _sessionContainer.RegisterInstance(scoreSystem);
        }

        private void RegisterIntermidiateStatistics()
        {
            GameStatisticsService statisticService = _sessionContainer.Resolve<GameStatisticsService>();
            statisticService.CreateStatistics(StandartStatisticsName.GameModeStatistics.ToString());
        }
    }
}