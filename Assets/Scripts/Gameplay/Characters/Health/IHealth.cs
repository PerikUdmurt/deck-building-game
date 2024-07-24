using System;

namespace CardBuildingGame.Gameplay.Characters
{
    public interface IHealth
    {
        int CurrentHealth { get; set; }
        int MaxHealth { get; }
        int Defence { get; set; }

        event Action<int> DefenceChanged;
        event Action<int, int> HealthChanged;
        event Action Died;

        void GetDamage(int damage);
        void GetDefense(int defense);
        void GetHealth(int healPoints);
    }
}