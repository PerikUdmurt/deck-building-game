using System;
using System.Collections.Generic;

namespace CardBuildingGame.Gameplay.Cards
{
    public class CardReset: ICardReset
    {
        private readonly List<CardData> _cards = new List<CardData>();
        
        public event Action<int> Changed;

        public void Add(CardData cardData)
        {
            _cards.Add(cardData);
            Changed?.Invoke(_cards.Count);
        }

        public void Remove(CardData cardData) 
        { 
            _cards.Remove(cardData);
            Changed?.Invoke(_cards.Count);
        }

        public void Clear()
        {
            _cards.Clear();
            Changed?.Invoke(_cards.Count);
        }

        public List<CardData> GetCards() => _cards;
    }
}