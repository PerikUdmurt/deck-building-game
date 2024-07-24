using System;

namespace CardBuildingGame.Gameplay.Characters
{
    public class Health: IHealth
    {
        private int _maxHealth;
        private int _health;
        private int _defense;

        public Health(int health, int maxHealth, int defense = 0)
        {
            _maxHealth = health;
            _health = health;
            _defense = defense;
        }

        public event Action<int> DefenceChanged;
        public event Action<int, int> HealthChanged;
        public event Action Died;

        public int CurrentHealth
        {
            get => _health;
            set
            {
                _health = value;
                if (_health > _maxHealth)
                {
                    _health = _maxHealth;
                }
                if (_health <= 0)
                {
                    _health = 0;
                    Died?.Invoke();
                }
                HealthChanged?.Invoke(_health, _maxHealth);
            }
        }
        public int MaxHealth { get => _maxHealth; }
        public int Defence
        {
            get => _defense;
            set
            {
                _defense = value;
                if (_defense < 0)
                {
                    _defense = 0;
                }
                DefenceChanged?.Invoke(_defense);
            }
        }

        public void GetDamage(int damage)
        {
            if (Defence < damage)
                CurrentHealth -= (damage - Defence);

            Defence -= damage;
        }

        public void GetHealth(int healPoints)
        {
            CurrentHealth += healPoints;
        }

        public void GetDefense(int defense) 
        { 
            Defence += defense;
        }
    }
}