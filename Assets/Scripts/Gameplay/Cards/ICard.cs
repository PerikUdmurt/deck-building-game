using System;

namespace CardBuildingGame.Gameplay.Cards
{
    public interface ICard
    {
        CardData CardData { get; }
        CardPresentation CardPresentation { get; }

        event Action<ICard> Played;
        event Action<ICard, ICardTarget> TargetFinded;

        void CleanUp();
        void MoveCardPresentationToHolder();
        void PlayCard(ICardTarget target, ICardTarget source = null);
    }
}