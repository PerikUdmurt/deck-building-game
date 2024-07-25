﻿using CardBuildingGame.Gameplay.Stacks;
using System;
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

            if (cardHolder != null)
            _cardPresentation.Dropped += FindNearestTarget;
        }

        public event Action<ICard> Played;
        public event Action<ICard, ICardTarget> TargetFinded;

        public CardData CardData { get => _cardData;}
        public CardPresentation CardPresentation { get => _cardPresentation;}

        public void MoveCardPresentationToHolder() => _cardHolder.Add(_cardPresentation);

        public void PlayCard(ICardTarget target)
        {
            PlayCardEffects(target);
            Played?.Invoke(this);
        }

        private void PlayCardEffects(ICardTarget target)
        {
            foreach (CardEffect effect in _cardData.Effects)
                effect.Play(target);
        }

        public void CleanUp()
        {
            if (_cardHolder != null)
            _cardPresentation.Dropped -= FindNearestTarget;
        }

        private void FindNearestTarget(Collider2D collider)
        {
            TargetFinder targetFinder = new();
            if (targetFinder.FindNearestCardTarget(collider,_cardData.TargetLayer, out ICardTarget cardTarget))
                TargetFinded?.Invoke(this, cardTarget);
            else MoveCardPresentationToHolder();
        }
    }
}