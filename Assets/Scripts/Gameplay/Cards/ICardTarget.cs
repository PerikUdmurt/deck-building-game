using CardBuildingGame.Gameplay.Characters;
using CardBuildingGame.Gameplay.Statuses;
using System;
using UnityEngine;

namespace CardBuildingGame.Gameplay.Cards
{
    public interface ICardTarget
    {
        IHealth Health { get; }
        TargetLayer TargetLayer { get; }
        ICardPlayer CardPlayer { get; }
        Animator Animator { get; }
        IStatusHolder StatusHolder { get; }

        event Action<Character> Died;
    }
}