using CardBuildingGame.Services;
using CardBuildingGame.Services.DI;
using CardBuildingGame.Services.SceneLoader;
using System.Collections;
using UnityEngine;
using YG;
using YGameTempate.Services.SaveLoad;
using YGameTemplate.Infrastructure.AssetProviders;
using YGameTemplate.Services.Rewards;

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
            RegisterAssetProvider();
            RegisterDataPersistentService();
            RegisterRewardService();

            _gameStateMachine.Enter<MainMenuState>();
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

        private void RegisterAssetProvider()
        {
            IAssetProvider assetProvider = new AssetProvider();
            assetProvider.Initialize();
            _container.RegisterInstance(assetProvider);
        }

        private void RegisterRewardService()
        {
            IRewardService rewardService = new RewardService();
            _container.RegisterInstance(rewardService);
        }

        private void RegisterDataPersistentService()
        {
            IDataPersistentService dataPersistentService = new DataPersistenceService();
            ICoroutineRunner coroutineRunner = _container.Resolve<ICoroutineRunner>();
            coroutineRunner.StartCoroutine(LoadGame(dataPersistentService));
            _container.RegisterInstance(dataPersistentService);
        }

        private IEnumerator LoadGame(IDataPersistentService service)
        {
            yield return new WaitUntil(() => YandexGame.SDKEnabled == false);
            service.LoadGame();
            Debug.Log("SavesLoaded");
        }
    }
}