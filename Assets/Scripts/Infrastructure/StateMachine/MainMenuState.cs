using CardBuildingGame.Services.DI;
using CardBuildingGame.Services.SceneLoader;

namespace CardBuildingGame.Infrastructure.StateMachine
{
    public class MainMenuState : IState
    {
        private readonly DiContainer _container;
        private readonly GameStateMachine _gameStateMachine;

        public MainMenuState(GameStateMachine gameStateMachine, DiContainer container) 
        {
            _container = container;
            _gameStateMachine = gameStateMachine;
        }
        public void Enter()
        {
            SceneLoader sceneLoader = _container.Resolve<SceneLoader>();
            sceneLoader.Load(SceneName.GamemenuScene, OnLoaded);

        }

        public void Exit()
        {
            
        }

        private void OnLoaded()
        {
            _gameStateMachine.Enter<LoadLevelState, SceneName>(SceneName.GameplayScene);
        }
    }
}
