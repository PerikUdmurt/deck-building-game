using CardBuildingGame.Datas;
using CardBuildingGame.Gameplay.Cards;
using CardBuildingGame.Gameplay.Characters;
using CardBuildingGame.Gameplay.Stacks;
using CardBuildingGame.Infrastructure.Factories;
using CardBuildingGame.Infrastructure.GameScenario;
using CardBuildingGame.Services;
using CardBuildingGame.Services.DI;
using CardBuildingGame.Services.SceneLoader;
using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using YGameTemplate.Infrastructure.AssetProviders;
using YGameTemplate.Infrastructure.Score;
using YGameTemplate.Services.StatisticsService;

namespace CardBuildingGame.Infrastructure.StateMachine
{
    public class LoadLevelState : IPayloadedState<LevelData>
    {
        private readonly GameStateMachine _gameStateMachine;
        private DiContainer _sessionContainer;
        private DiContainer _sceneContainer;
        private SceneLoader _sceneLoader;
        private Character _character;
        private CardPresentationHolder _cardHolder;

        public LoadLevelState(GameStateMachine gameStateMachine)
        {
            _gameStateMachine = gameStateMachine;
        }

        public void Enter(LevelData levelData)
        {
            _sessionContainer = levelData.SessionDiContainer;
            _sceneLoader = _sessionContainer.Resolve<SceneLoader>();
            _sceneLoader.Load(levelData.RoomData.SceneName, OnLoaded);
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
            return sceneInstaller.GetSceneInstaller(_sessionContainer);
        }

        private async UniTask<Character> InstantiateHero()
        {
            ICharacterSpawner characterSpawner = _sceneContainer.Resolve<ICharacterSpawner>();
            Vector3 playerMarkerPosition = _sceneContainer.Resolve<Vector3>("HeroPosition");

            Character hero = await characterSpawner.SpawnCharacterFromStaticData(Character.CharacterType.Player1, 1, playerMarkerPosition);
            _sceneContainer.RegisterInstance(hero, "Hero");

            return hero;
        }

        private void RegisterCardSpawner()
        {
            IAssetProvider assetProvider = _sceneContainer.Resolve<IAssetProvider>();
            GameStatisticsService statServise = _sceneContainer.Resolve<GameStatisticsService>();

            ICardSpawner cardSpawner =
                new CardSpawner
                (_sceneContainer.Resolve<IStaticDataService>(), 
                _character.CardPlayer, 
                _cardHolder, 
                assetProvider, 
                statServise);
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
            HUDController hudController = new(
                hud, 
                _character, 
                _gameStateMachine, 
                _sceneContainer.Resolve<ScoreSystem>(),
                _sceneContainer.Resolve<GameStatisticsService>()
                );
            _sceneContainer.RegisterInstance(hudController);
        }

        private void RegisterLevelData()
        {
            int maxRoom = _sceneContainer.Resolve<int>("MaxRoom");

            LevelInfo levelData = new(0, maxRoom, new List<ICardTarget>());
            _sceneContainer.RegisterInstance(levelData);
        }

        private void RegisterCharacterSpawner()
            => _sceneContainer.Register<ICharacterSpawner>(
                DiRegistrationType.AsTransient,
                c => new CharacterSpawner(
                    staticDataService: _sceneContainer.Resolve<IStaticDataService>(),
                    levelInfo: _sceneContainer.Resolve<LevelInfo>(),
                    assetProvider: _sceneContainer.Resolve<IAssetProvider>(),
                    statServise: _sceneContainer.Resolve<GameStatisticsService>())
                );
    }

    public class LevelData
    {
        public RoomStaticData RoomData;
        public DiContainer SessionDiContainer;
        public int CurrentFloor;

        public LevelData(RoomStaticData roomData, DiContainer sessionDiContainer, int currentFloor)
        {
            RoomData = roomData;
            SessionDiContainer = sessionDiContainer;
            CurrentFloor = currentFloor;
        }
    }
}