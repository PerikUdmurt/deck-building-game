using CardBuildingGame.Gameplay.Cards;
using CardBuildingGame.UI;
using System;
using UnityEngine;

namespace CardBuildingGame.Gameplay.Characters
{
    public class Character : MonoBehaviour, ICardTarget
    {
        [SerializeField] private MarkerUI HealthMarker;
        [SerializeField] private MarkerUI DefenseMarker;

        private IHealth _health;

        public LayerMask TargetLayer => gameObject.layer;

        public event Action<Character> Died;

        public void Construct(int health, int maxHealth, int defense = 0)
        {
            _health = new Health(health, maxHealth, defense);
            _health.HealthChanged += UpdateHealthMarker;
            _health.DefenceChanged += UpdateDefenceMarker;
            UpdateDefenceMarker(_health.Defence);
            UpdateHealthMarker(_health.CurrentHealth, _health.MaxHealth);
            _health.Died += OnDied;
        }

        public IHealth Health => _health;

        private void OnDied()
        {
            Died?.Invoke(this);
        }

        private void UpdateHealthMarker(int health, int maxHealth)
        {
            string text = new($"{health}/{maxHealth}");
            HealthMarker.SetText(text);
        }

        private void UpdateDefenceMarker(int defense)
        {
            if (defense <= 0)
                DefenseMarker.SetActive(false);
            else 
                DefenseMarker.SetActive(true);

            string text = new($"{defense}");
            DefenseMarker.SetText(text);
        }
    }
}