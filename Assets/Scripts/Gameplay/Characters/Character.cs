using CardBuildingGame.Gameplay.Cards;
using CardBuildingGame.UI;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace CardBuildingGame.Gameplay.Characters
{
    public class Character : MonoBehaviour, ICardTarget
    {
        [SerializeField] private MarkerUI _healthMarker;
        [SerializeField] private MarkerUI _defenseMarker;
        [SerializeField] private MarkerUI _attackMarker;
        [SerializeField] private TargetLayer _targetLayer;

        private IHealth _health;
        private ICardPlayer _player;

        public IHealth Health { get => _health; }
        public ICardPlayer CardPlayer { get => _player; }
        public TargetLayer TargetLayer { get => _targetLayer; }

        public event Action<Character> Died;

        public void Construct(int health, int maxHealth, int energy, int maxEnergy, List<CardData> cardDatas, int defense = 0)
        {
            _player = new CardPlayer(cardDatas, energy, maxEnergy);
            _health = new Health(health, maxHealth, defense);
            _health.HealthChanged += UpdateHealthMarker;
            _health.DefenceChanged += UpdateDefenceMarker;
            _player.CardPrepared += UpdateAttackMarker;
            UpdateAttackMarker(null);
            UpdateDefenceMarker(_health.Defence);
            UpdateHealthMarker(_health.CurrentHealth, _health.MaxHealth);
            _health.Died += OnDied;
        }


        private void OnDied()
        {
            Died?.Invoke(this);
        }

        private void UpdateHealthMarker(int health, int maxHealth)
        {
            string text = new($"{health}/{maxHealth}");
            _healthMarker.SetText(text);
        }

        private void UpdateDefenceMarker(int defense)
        {
            if (defense <= 0)
                _defenseMarker.SetActive(false);
            else 
                _defenseMarker.SetActive(true);

            string text = new($"{defense}");
            _defenseMarker.SetText(text);
        }

        private void UpdateAttackMarker(ICard card)
        {
            if (card == null)
                _attackMarker.SetActive(false);
            else
            {
                _attackMarker.SetSprite(card.CardData.Effects[0]?.Icon);
                _attackMarker.SetText(card.CardData.Effects[0]?.TextForMarker);
                _attackMarker.SetActive(true);
            }
        }
    }
}