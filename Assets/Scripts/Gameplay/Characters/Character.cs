using CardBuildingGame.Gameplay.Cards;
using CardBuildingGame.Gameplay.Statuses;
using CardBuildingGame.UI;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace CardBuildingGame.Gameplay.Characters
{
    public class Character : MonoBehaviour, ICardTarget
    {
        [SerializeField] protected private MarkerUI _healthMarker;
        [SerializeField] protected private MarkerUI _defenseMarker;
        [SerializeField] protected private MarkerUI _attackMarker;
        [SerializeField] protected private TargetLayer _targetLayer;
        [SerializeField] protected private Animator _animator;
        [SerializeField] protected private StatusPresentationHolder _statusPresentationHolder;

        protected private IHealth _health;
        protected private ICardPlayer _player;
        protected private IStatusHolder _statusHolder;

        public IStatusHolder statusHolder { get => _statusHolder; }
        public IHealth Health { get => _health; }
        public ICardPlayer CardPlayer { get => _player; }
        public TargetLayer TargetLayer { get => _targetLayer; }
        public Animator Animator { get => _animator; }

        public event Action<Character> Died;

        public void Construct(CharacterData characterData, List<CardData> cardDatas)
        {
            _player = new CardPlayer(cardDatas, characterData.Energy, characterData.MaxEnergy, this);
            _health = new Health(characterData.Health, characterData.MaxHealth, characterData.Defense);
            _statusHolder = new StatusHolder();

            _health.HealthChanged += UpdateHealthMarker;
            _health.DefenceChanged += UpdateDefenceMarker;
            _player.CardPrepared += UpdateAttackMarker;
            _statusHolder.Changed += UpdateStatusPresentation;
            _health.Died += OnDied;

            UpdateStatusPresentation();
            UpdateAttackMarker(null);
            UpdateDefenceMarker(_health.Defence);
            UpdateHealthMarker(_health.CurrentHealth, _health.MaxHealth);
        }

        private void OnDied()
        {
            Died?.Invoke(this);
        }

        private void UpdateStatusPresentation()
        {

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

    public class Player: Character
    {
        public enum PlayerType
        {
            Player1 = 0, 
            Player2 = 1, 
            Player3 = 2, 
            Player4 = 3,
        }
    }

    public class Enemy: Character
    {
        public enum EnemyType
        {
            Enemy1 = 0,
            Enemy2 = 1,
            Enemy3 = 2,
            Enemy4 = 3,
        }
    }
}