using CardBuildingGame.Gameplay.Characters;
using System;
using UnityEngine;

namespace CardBuildingGame.Gameplay.Cards
{
    public interface ICardTarget
    {
        IHealth Health { get; }
        LayerMask TargetLayer { get; }

        event Action<Character> Died;
    }
}