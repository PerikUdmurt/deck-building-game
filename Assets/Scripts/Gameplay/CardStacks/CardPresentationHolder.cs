using CardBuildingGame.Gameplay.Cards;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace CardBuildingGame.Gameplay.Stacks
{
    public class CardPresentationHolder
    {
        private readonly Vector3 _holderPosition;
        private readonly Vector3 _deltaCardOffset;
        private List<ICardPresentation> _cardPresentations;


        public CardPresentationHolder(Vector3 holderPosition, Vector3 deltaCardOffset)
        {
            _cardPresentations = new List<ICardPresentation>();
            _holderPosition = holderPosition;
            _deltaCardOffset = deltaCardOffset;
        }

        public void Add(ICardPresentation cardPresentation)
        { 
            _cardPresentations.Add(cardPresentation);
            cardPresentation.Dragged += Remove;
            Sort();
        }

        public void Remove(ICardPresentation cardPresentation)
        {
            if (_cardPresentations.Contains(cardPresentation))
            {
                _cardPresentations?.Remove(cardPresentation);
                cardPresentation.Dragged -= Remove;
                Sort();
            }
        }

        private void Sort()
        {
            var list = from card in _cardPresentations
                       orderby card.SortInfo.Item1, card.SortInfo.Item2
                       select card;

            for (int i = 0; i < list.Count(); i++)
            {
                Vector3 newPos = _holderPosition + (_deltaCardOffset * i);
                _cardPresentations[i].MoveTo(newPos);
            }
        }
    }
}