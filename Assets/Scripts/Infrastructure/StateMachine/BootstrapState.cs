using CardBuildingGame.Datas;
using CardBuildingGame.Gameplay.Cards;
using CardBuildingGame.Gameplay.Stacks;
using CardBuildingGame.Infrastructure.Factories;
using CardBuildingGame.Services;
using CardBuildingGame.Services.DI;
using CardBuildingGame.StaticDatas;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace CardBuildingGame.Infrastructure.StateMachine
{
    public class BootstrapState : IState
    {
        private readonly GameStateMachine _gameStateMachine;
        private readonly DiContainer _container;

        private IDeck _deck;
        private ICardReset _cardReset;
        private CardPresentationHolder _cardHolder;
        private HandDeck _handDeck;

        public BootstrapState(GameStateMachine gameStateMachine, DiContainer container)
        {
            _gameStateMachine = gameStateMachine;
            _container = container;
        }

        public void Enter()
        {
            RegisterStaticDataServices();
            RegisterLevelData();
            InstantiateDecks();
            InstantiateHUD();
            RegisterCardSpawner();
            RegisterCharacterSpawner();
            InstantiateHero();

            _gameStateMachine.Enter<NewRoomState>();
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

        private void InstantiateDecks()
        {
            _cardReset = new CardReset();

            _handDeck = new HandDeck();

            _deck = InitPlayerDeck(_cardReset);

            _cardHolder = InitCardHolder();
        }

        private IDeck InitPlayerDeck(ICardReset cardReset)
        {
            IDeck deck = new Deck(new List<CardData>(), cardReset);
            var deckStaticData = _container.Resolve<DeckStaticData>("InitialDeckStaticData");

            foreach (CardStaticData cardStaticData in deckStaticData.Cards)
            {
                CardData data = cardStaticData.ToCardData();
                if (data != null)
                    deck.AddCard(data);
            }

            _container.RegisterInstance(deck, tag: "PlayerDeck");

            return deck;
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
            HUDController _hudController = new(hud, _cardReset, _deck);
            _container.RegisterInstance(_hudController);
        }

        private void RegisterCardSpawner()
        {
            ICardSpawner cardSpawner =
                new CardSpawner(6, _container.Resolve<IStaticDataService>(), _cardReset, _handDeck, _cardHolder);
            _container.RegisterInstance(cardSpawner);
        }

        private void RegisterCharacterSpawner()
            => _container.Register<ICharacterSpawner>(
                DiRegistrationType.AsTransient, 
                c => new CharacterSpawner(
                    staticDataService: _container.Resolve<IStaticDataService>(), 
                    levelData: _container.Resolve<LevelData>()));

        private void InstantiateHero()
        {
            ICharacterSpawner characterSpawner = _container.Resolve<ICharacterSpawner>();
            Vector3 playerMarkerPosition = _container.Resolve<Vector3>("HeroPosition");
            characterSpawner.SpawnCharacterFromStaticData("Hero", playerMarkerPosition);
        }
    }
}