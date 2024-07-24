using CardBuildingGame.Gameplay.Cards;
using CardBuildingGame.Gameplay.Stacks;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace CardBuildingGame.Gameplay.Characters
{
    public class CardPlayer: ICardPlayer
    {
        private ICardReset _cardReset;
        private IDeck _deck;
        private List<ICardTarget> _cardTargets;

        public CardPlayer(List<CardData> cardDatas)
        {
            _cardReset = new CardReset();
        }

        public Card PreparedCard { get; private set; }

        public void PrepareCard()
        {
            CardData cardData = _deck.GetRandomCardData();
            PreparedCard = new Card(cardData);
        }

        public void PlayCard()
        {
            
        }

        public ICardTarget FindTarget(IEnumerable<ICardTarget> cardTargets, LayerMask targetLayer)
        {
            var availableTargets = from target in cardTargets
                                   where target.TargetLayer == targetLayer
                                   select target;

            return availableTargets.FirstOrDefault();
        }
    }
}