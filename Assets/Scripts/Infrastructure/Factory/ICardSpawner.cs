using CardBuildingGame.Gameplay.Cards;
using CardBuildingGame.Gameplay.Stacks;
using UnityEngine;

namespace CardBuildingGame.Infrastructure.Factories
{
    public interface ICardSpawner
    {
        void DespawnAllCards();
        void DespawnCard(ICard cardModel);
        ICard SpawnCard(CardData cardData, Vector3 at);
        ICard SpawnCardByStaticData(string CardID , Vector3 at);
        ICard SpawnCardFromDeck(IDeck deck, Vector3 at);
    }
}