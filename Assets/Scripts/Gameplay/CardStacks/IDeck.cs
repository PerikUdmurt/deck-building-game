using CardBuildingGame.Gameplay.Cards;
using System;
using System.Collections.Generic;

namespace CardBuildingGame.Gameplay.Stacks
{
    public interface IDeck
    {
        event Action<int> Changed;

        void AddCard(CardData card);
        void Clear();
        List<CardData> GetCards();
        CardData GetRandomCardData();
        void Remove(CardData card);
    }
}