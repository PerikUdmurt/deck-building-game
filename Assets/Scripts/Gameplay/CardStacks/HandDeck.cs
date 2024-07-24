using CardBuildingGame.Gameplay.Cards;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CardBuildingGame.Gameplay.Stacks
{
    public class HandDeck
    {
        private List<ICard> _cardModels;

        public HandDeck()
        {
            _cardModels = new List<ICard>();
        }

        public void Add(ICard cardModel) => _cardModels.Add(cardModel);

        public void Remove(ICard card)
        {
            if (_cardModels.Contains(card))
                _cardModels.Remove(card);
        }

        public ICard[] GetAllCardModels() => _cardModels.ToArray();
    }
}