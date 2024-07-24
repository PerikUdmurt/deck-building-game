using CardBuildingGame.Gameplay.Cards;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace CardBuildingGame.Gameplay.Stacks
{
    public class Deck : IDeck
    { 
        private List<CardData> _cards;
        private readonly ICardReset _cardReset;

        public Deck(List<CardData> cards, ICardReset cardReset) 
        { 
            _cards = cards;
            _cardReset = cardReset;
        }

        public event Action<int> Changed;

        public List<CardData> GetCards() => _cards;
        
        public CardData GetRandomCardData()
        {
            if (_cards.Count == 0)
            {
                TakeCardsFromCardReset();
            }

            int index = UnityEngine.Random.Range(0, _cards.Count);
            return _cards[index];
        }

        public void AddCard(CardData card)
        {
            _cards.Add(card);
            Changed?.Invoke(_cards.Count);
        }

        public void Remove(CardData card) 
        { 
            _cards.Remove(card);
            Changed?.Invoke(_cards.Count);
        }

        public void Clear() 
        { 
            _cards.Clear();
            Changed?.Invoke(_cards.Count);
        }

        private void TakeCardsFromCardReset()
        {
            _cards.AddRange(_cardReset.GetCards());
            _cardReset.Clear();
        }
    }
}