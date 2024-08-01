using CardBuildingGame.Gameplay.Cards;
using CardBuildingGame.Gameplay.Stacks;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace CardBuildingGame.Infrastructure.Factories
{
    public interface ICardSpawner
    {
        void DespawnAllCards();
        void DespawnCard(ICard cardModel);
        UniTask<ICard> SpawnCard(CardData cardData, Vector3 at);
        UniTask<ICard> SpawnCardByStaticData(string CardID , Vector3 at);
        UniTask<ICard> SpawnCardFromDeck(IDeck deck, Vector3 at);
    }
}