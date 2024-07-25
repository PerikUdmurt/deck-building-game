using CardBuildingGame.Gameplay.Characters;
using System;
using UnityEngine;

namespace CardBuildingGame.Gameplay.Cards
{
    public interface ICardTarget
    {
        IHealth Health { get; }
        TargetLayer TargetLayer { get; }
        ICardPlayer CardPlayer { get; }

        event Action<Character> Died;
    }
}