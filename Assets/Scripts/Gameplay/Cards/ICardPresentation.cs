using System;
using UnityEngine;

namespace CardBuildingGame.Gameplay.Cards
{
    public interface ICardPresentation
    {
        (int, string) SortInfo { get; }
        Transform Transform { get; }

        event Action<ICardPresentation> Dragged;
        event Action<Collider2D> Dropped;

        void Init(CardData cardData);
        void SetEnergyCostText(int cost);
        void SetSprite(Sprite sprite);
        void MoveTo(Vector3 vector3);
    }
}