using CardBuildingGame.Gameplay.Cards;
using System.Collections;
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
            _cardPresentations.OrderBy(p => p.SortInfo.Item1).ThenBy(p => p.SortInfo.Item2);
            for (int i = 0; i > _cardPresentations.Count; i++)
            {
                Vector3 newPos = _holderPosition + (_deltaCardOffset * i);
                _cardPresentations[i].MoveTo(newPos);
                Debug.Log(newPos.ToString());
            }
            
        }
    }
}