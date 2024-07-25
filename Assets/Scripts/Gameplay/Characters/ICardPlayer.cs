using CardBuildingGame.Gameplay.Cards;
using CardBuildingGame.Gameplay.Stacks;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace CardBuildingGame.Gameplay.Characters
{
    public interface ICardPlayer
    {
        HandDeck HandDeck { get; }
        ICardReset CardReset { get; }
        IDeck Deck { get; }
        Energy Energy { get; }
        ICard PreparedCard { get; set; }

        event Action<ICard> CardPrepared;

        void PlayCard(ICard card, ICardTarget cardTarget);
        void PlayCardOnRandomTarget(ICard card, IEnumerable<ICardTarget> targets);
        void PlayPreparedCard(IEnumerable<ICardTarget> targets);
        void PrepareCard();
    }
}