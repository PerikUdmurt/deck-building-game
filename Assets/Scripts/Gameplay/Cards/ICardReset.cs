using System;
using System.Collections.Generic;

namespace CardBuildingGame.Gameplay.Cards
{
    public interface ICardReset
    {
        event Action<int> Changed;

        void Add(CardData cardData);
        void Clear();
        List<CardData> GetCards();
        void Remove(CardData cardData);
    }
}