using CardBuildingGame.Datas;
using CardBuildingGame.Infrastructure.Factories;
using CardBuildingGame.Services;
using CardBuildingGame.Services.DI;
using CardBuildingGame.Services.SceneLoader;

namespace CardBuildingGame.Infrastructure.StateMachine
{
    public class BootstrapState : IState
    {
        private readonly GameStateMachine _gameStateMachine;
        private readonly DiContainer _container;

        public BootstrapState(GameStateMachine gameStateMachine, DiContainer container)
        {
            _gameStateMachine = gameStateMachine;
            _container = container;
        }

        public void Enter()
        {
            RegisterSceneLoaderService();
            RegisterStaticDataServices();

            _gameStateMachine.Enter<LoadLevelState, SceneName>(SceneName.GameplayScene);
        }

        public void Exit()
        {

        }

        private void RegisterSceneLoaderService()
        {
            _container.Register<SceneLoader>
                (registrationType: DiRegistrationType.AsTransient,
                factory: c => new SceneLoader(_container.Resolve<ICoroutineRunner>()));
        }

        private void RegisterStaticDataServices()
        {
            IStaticDataService staticDataService = new StaticDataService();
            staticDataService.LoadStaticDatas();
            _container.RegisterInstance(staticDataService);
        }
    }
}