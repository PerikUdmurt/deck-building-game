using CardBuildingGame.Gameplay.Cards;
using CardBuildingGame.Gameplay.Characters;
using CardBuildingGame.Gameplay.Stacks;
using CardBuildingGame.Services;
using CardBuildingGame.StaticDatas;
using Cysharp.Threading.Tasks;
using System;
using UnityEngine;
using YGameTemplate.Infrastructure.AssetProviders;
using YGameTemplate.Infrastructure.ObjectPool;

namespace CardBuildingGame.Infrastructure.Factories
{
    public class CardSpawner: ICardSpawner
    {
        private ObjectPool<CardPresentation> _cardPool;
        private IStaticDataService _staticDataService;
        private readonly ICardPlayer _cardPlayer;
        private readonly CardPresentationHolder _cardHolder;

        public CardSpawner(IStaticDataService staticDataService, ICardPlayer cardPlayer, CardPresentationHolder cardHolder, IAssetProvider assetProvider)
        {
            _cardPool = new(assetProvider, BundlePath.Card);
            _staticDataService = staticDataService;
            _cardPlayer = cardPlayer;
            _cardHolder = cardHolder;
        }

        public async UniTask<ICard> SpawnCardByStaticData(string CardID, Vector3 at)
        {
            _staticDataService.GetStaticData(CardID, out StaticData staticData);
            CardStaticData cardStaticData;

            if (cardStaticData = staticData as CardStaticData)
            {
                CardData cardData = cardStaticData.ToCardData();
                return await SpawnCard(cardData, at);
            }
            else throw new Exception($"It is not possible to convert the static data with ID:{CardID} to {this.GetType()}");
        }
        
        public async UniTask<ICard> SpawnCardFromDeck(IDeck deck, Vector3 at)
        {
            CardData cardData = deck.GetRandomCardData();
            deck.Remove(cardData);
            return await SpawnCard(cardData, at);
        }

        public async UniTask<ICard> SpawnCard(CardData cardData, Vector3 at)
        {
            CardPresentation cardPresentation = await InstantiateCardPresentation(cardData, at);
            return CreateCardModel(cardData, cardPresentation);
        }


        public void DespawnCard(ICard card) 
        {
            card.Played -= DespawnCard;
            card.TargetFinded -= _cardPlayer.PlayCard;
            card.CleanUp();
            _cardHolder.Remove(card.CardPresentation);
            _cardPool.Release(card.CardPresentation);
            _cardPlayer.CardReset.Add(card.CardData);
            _cardPlayer.HandDeck.Remove(card);
        }

        public void DespawnAllCards()
        {
            ICard[] cardModels = _cardPlayer.HandDeck.GetAllCardModels();

            foreach (ICard cardModel in cardModels)
                DespawnCard(cardModel);
        }

        private ICard CreateCardModel(CardData cardData, CardPresentation cardPresentation)
        {
            ICard cardModel = new Card(cardData, cardPresentation, _cardHolder);
            _cardPlayer.HandDeck.Add(cardModel);
            cardModel.TargetFinded += _cardPlayer.PlayCard;
            cardModel.Played += DespawnCard;
            return cardModel;
        }

        private async UniTask<CardPresentation> InstantiateCardPresentation(CardData cardData, Vector3 at)
        {
            CardPresentation cardPresentation = await _cardPool.Get();
            cardPresentation.gameObject.transform.position = at;
            cardPresentation.Init(cardData);
            _cardHolder.Add(cardPresentation);
            return cardPresentation;
        }
    }
}