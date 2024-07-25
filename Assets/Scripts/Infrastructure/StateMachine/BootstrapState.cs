using CardBuildingGame.Datas;
using CardBuildingGame.Gameplay.Cards;
using CardBuildingGame.Gameplay.Characters;
using CardBuildingGame.Gameplay.Stacks;
using CardBuildingGame.Infrastructure.Factories;
using CardBuildingGame.Services;
using CardBuildingGame.Services.DI;
using System.Collections.Generic;
using UnityEngine;

namespace CardBuildingGame.Infrastructure.StateMachine
{
    public class BootstrapState : IState
    {
        private readonly GameStateMachine _gameStateMachine;
        private readonly DiContainer _container;

        private CardPresentationHolder _cardHolder;
        private Character _character;

        public BootstrapState(GameStateMachine gameStateMachine, DiContainer container)
        {
            _gameStateMachine = gameStateMachine;
            _container = container;
        }

        public void Enter()
        {
            RegisterStaticDataServices();
            RegisterLevelData();
            RegisterCharacterSpawner();
            _character = InstantiateHero();
            _cardHolder = InitCardHolder();
            RegisterCardSpawner();
            InstantiateHUD();

            _gameStateMachine.Enter<GameLoopState>();
        }

        public void Exit()
        {

        }

        private void RegisterStaticDataServices()
        {
            IStaticDataService staticDataService = new StaticDataService();
            staticDataService.LoadStaticDatas();
            _container.RegisterInstance(staticDataService);
        }

        private void RegisterLevelData()
        {
            LevelData levelData = new(0, new List<ICardTarget>());
            _container.RegisterInstance(levelData);
        }

        private CardPresentationHolder InitCardHolder()
        {
            CardPresentationHolder cardHolder = new(
                holderPosition: _container.Resolve<Vector3>(tag: "CardHolderPosition"),
                deltaCardOffset: _container.Resolve<Vector3>(tag: "DeltaCardOffset"));

            return cardHolder;
        }

        private void InstantiateHUD()
        {
            IHUDSpawner hudSpawner = new HUDSpawner();
            HUD hud = hudSpawner.SpawnHUD();
            HUDController _hudController = new(hud, _character.CardPlayer);
            _container.RegisterInstance(_hudController);
        }

        private void RegisterCharacterSpawner()
            => _container.Register<ICharacterSpawner>(
                DiRegistrationType.AsTransient, 
                c => new CharacterSpawner(
                    staticDataService: _container.Resolve<IStaticDataService>(), 
                    levelData: _container.Resolve<LevelData>()));

        private Character InstantiateHero()
        {
            ICharacterSpawner characterSpawner = _container.Resolve<ICharacterSpawner>();
            Vector3 playerMarkerPosition = _container.Resolve<Vector3>("HeroPosition");
            Character hero = characterSpawner.SpawnCharacterFromStaticData("Hero", "HeroDeck", playerMarkerPosition);
            _container.RegisterInstance(hero, "Hero");
            return hero;
        }

        private void RegisterCardSpawner()
        {
            ICardSpawner cardSpawner =
                new CardSpawner(6, _container.Resolve<IStaticDataService>(), _character.CardPlayer, _cardHolder);
            _container.RegisterInstance(cardSpawner);
        }
    }
}