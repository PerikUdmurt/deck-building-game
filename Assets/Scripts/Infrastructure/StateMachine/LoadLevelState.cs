using CardBuildingGame.Datas;
using CardBuildingGame.Gameplay.Cards;
using CardBuildingGame.Gameplay.Characters;
using CardBuildingGame.Gameplay.Stacks;
using CardBuildingGame.Infrastructure.Factories;
using CardBuildingGame.Services;
using CardBuildingGame.Services.DI;
using CardBuildingGame.Services.SceneLoader;
using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using YGameTemplate.Infrastructure.AssetProviders;

namespace CardBuildingGame.Infrastructure.StateMachine
{
    public class LoadLevelState : IPayloadedState<SceneName>
    {
        private readonly GameStateMachine _gameStateMachine;
        private readonly DiContainer _projectContainer;
        private DiContainer _sceneContainer;
        private SceneLoader _sceneLoader;
        private Character _character;
        private CardPresentationHolder _cardHolder;

        public LoadLevelState(GameStateMachine gameStateMachine, DiContainer projectContainer)
        {
            _gameStateMachine = gameStateMachine;
            _projectContainer = projectContainer;
        }

        public void Enter(SceneName sceneName)
        {
            _sceneLoader = _projectContainer.Resolve<SceneLoader>();
            _sceneLoader.Load(sceneName, OnLoaded);
        }

        private async void OnLoaded()
        {
            _sceneContainer = GetSceneContainer();
            RegisterLevelData();
            RegisterCharacterSpawner();
            _character = await InstantiateHero();
            _cardHolder = InitCardHolder();
            RegisterCardSpawner();
            await InstantiateHUD();

            _gameStateMachine.Enter<GameLoopState, DiContainer>(_sceneContainer);
        }

        public void Exit()
        {

        }

        private DiContainer GetSceneContainer()
        {
            GameObject gameObject = GameObject.FindGameObjectsWithTag("SceneInstaller").First();
            gameObject.TryGetComponent(out SceneInstaller sceneInstaller);
            return sceneInstaller.GetSceneInstaller(_projectContainer);
        }

        private async UniTask<Character> InstantiateHero()
        {
            ICharacterSpawner characterSpawner = _sceneContainer.Resolve<ICharacterSpawner>();
            Vector3 playerMarkerPosition = _sceneContainer.Resolve<Vector3>("HeroPosition");

            Character hero = await characterSpawner.SpawnCharacterFromStaticData("Hero", "HeroDeck", playerMarkerPosition);
            _sceneContainer.RegisterInstance(hero, "Hero");

            return hero;
        }

        private void RegisterCardSpawner()
        {
            IAssetProvider assetProvider = _sceneContainer.Resolve<IAssetProvider>();

            ICardSpawner cardSpawner =
                new CardSpawner(_sceneContainer.Resolve<IStaticDataService>(), _character.CardPlayer, _cardHolder, assetProvider);
            _sceneContainer.RegisterInstance(cardSpawner);
        }

        private CardPresentationHolder InitCardHolder()
        {
            CardPresentationHolder cardHolder = new(
                holderPosition: _sceneContainer.Resolve<Vector3>(tag: "CardHolderPosition"),
                deltaCardOffset: _sceneContainer.Resolve<Vector3>(tag: "DeltaCardOffset"));

            return cardHolder;
        }

        private async UniTask InstantiateHUD()
        {
            IAssetProvider assetProvider = _sceneContainer.Resolve<IAssetProvider>();
            IHUDSpawner hudSpawner = new HUDSpawner(assetProvider);
            HUD hud = await hudSpawner.SpawnHUD();
            HUDController hudController = new(hud, _character.CardPlayer, _gameStateMachine);
            _character.Died += hudController.OnGameOver;
            _sceneContainer.RegisterInstance(hudController);
        }

        private void RegisterLevelData()
        {
            int maxRoom = _sceneContainer.Resolve<int>("MaxRoom");

            LevelData levelData = new(0, maxRoom, new List<ICardTarget>());
            _sceneContainer.RegisterInstance(levelData);
        }

        private void RegisterCharacterSpawner()
            => _sceneContainer.Register<ICharacterSpawner>(
                DiRegistrationType.AsTransient,
                c => new CharacterSpawner(
                    staticDataService: _sceneContainer.Resolve<IStaticDataService>(),
                    levelData: _sceneContainer.Resolve<LevelData>(),
                    assetProvider: _sceneContainer.Resolve<IAssetProvider>())
                );
    }
}