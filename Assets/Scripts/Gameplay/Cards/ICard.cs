using System;

namespace CardBuildingGame.Gameplay.Cards
{
    public interface ICard
    {
        CardData CardData { get; }
        CardPresentation CardPresentation { get; }

        event Action<ICard> Played;

        void PlayCard(ICardTarget target);
    }
}