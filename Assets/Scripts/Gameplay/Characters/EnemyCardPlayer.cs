using CardBuildingGame.Gameplay.Cards;
using CardBuildingGame.Gameplay.Stacks;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace CardBuildingGame.Gameplay.Characters
{
    public class CardPlayer: ICardPlayer
    {
        private IDeck _deck;
        private ICardReset _cardReset;
        private HandDeck _handDeck;
        private Energy _energy;
        private ICard _preparedCard;
        private ICardTarget _source;

        public CardPlayer(List<CardData> cardDatas, int energy, int maxEnergy, ICardTarget source = null)
        {
            _cardReset = new CardReset();
            _deck = new Deck(cardDatas, _cardReset);
            _handDeck = new HandDeck();
            _energy = new Energy(energy, maxEnergy);
            _source = source;
        }

        public ICardTarget Source { get => _source; }
        public IDeck Deck { get => _deck; }
        public ICardReset CardReset { get => _cardReset; }
        public HandDeck HandDeck { get => _handDeck; }
        public Energy Energy { get => _energy; }
        public ICard PreparedCard
        {
            get => _preparedCard;
            set
            {
                _preparedCard = value;
                CardPrepared?.Invoke(_preparedCard);
            }
        }

        public event Action<ICard> CardPrepared;

        public void PrepareCard()
        {
            CardData cardData = _deck.GetRandomCardData();
            PreparedCard = new Card(cardData);
        }

        public void PlayCard(ICard card, ICardTarget cardTarget)
        {
            if (_energy.TrySpendEnergy(card.CardData.EnergyCost))
            {
                card.PlayCard(cardTarget, _source);
            }
            else card.MoveCardPresentationToHolder();
        }

        public void PlayCardOnRandomTarget(ICard card, IEnumerable<ICardTarget> targets)
        {
            TargetFinder finder = new TargetFinder();
            ICardTarget target = finder.FindRandomTarget(targets, card.CardData.TargetLayer);
            PlayCard(card, target);
        }

        public void PlayPreparedCard(IEnumerable<ICardTarget> targets)
        {
            PlayCardOnRandomTarget(PreparedCard, targets);
            PreparedCard = null;
        }
    }
}