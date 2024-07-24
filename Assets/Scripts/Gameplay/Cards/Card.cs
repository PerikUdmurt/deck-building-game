using CardBuildingGame.Gameplay.Stacks;
using System;
using System.Text;
using UnityEngine;

namespace CardBuildingGame.Gameplay.Cards
{
    public class Card: ICard
    {
        private CardData _cardData;
        private CardPresentation _cardPresentation;
        private readonly CardPresentationHolder _cardHolder;

        public Card(CardData cardData, CardPresentation cardPresentation = null, CardPresentationHolder cardHolder = null)
        {
            _cardData = cardData;
            _cardPresentation = cardPresentation;
            _cardHolder = cardHolder;
            _cardPresentation.Dropped += FindNearestTarget;
        }

        public event Action<ICard> Played;

        public CardData CardData { get => _cardData;}
        public CardPresentation CardPresentation { get => _cardPresentation;}

        private void MoveCardPresentationToHolder() => _cardHolder.Add(_cardPresentation);

        public void PlayCard(ICardTarget target)
        {
            PlayCardEffects(target);
            Played?.Invoke(this);
            CleanUp();
        }

        private void PlayCardEffects(ICardTarget target)
        {
            foreach (CardEffect effect in _cardData.Effects)
            {
                effect.Play(target);
            }

            StringBuilder sb = new StringBuilder();
            foreach (CardEffect effect in _cardData.Effects) { sb.AppendLine(effect.ToString()); }
        }

        private void FindNearestTarget(Collider2D collider)
        {
            TargetFinder targetFinder = new(collider);
            if (targetFinder.FindNearestCardTarget(_cardData.TargetLayer, out ICardTarget cardTarget))
                PlayCard(cardTarget);
            else MoveCardPresentationToHolder();
        }

        private void CleanUp()
        {
            _cardPresentation.Dropped -= FindNearestTarget;
            _cardPresentation = null;
            _cardData = null;
            Debug.Log("Cleaned");
        }
    }
}