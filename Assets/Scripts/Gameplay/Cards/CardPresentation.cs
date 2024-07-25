using CardBuildingGame.Infrastructure;
using DG.Tweening;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace CardBuildingGame.Gameplay.Cards
{
    [RequireComponent(typeof(Collider2D))]
    public class CardPresentation : MonoBehaviour, IPooledObject, ICardPresentation, IDropHandler, IDragHandler, IBeginDragHandler, IPointerEnterHandler, IPointerExitHandler
    {
        [Header("General")]
        [SerializeField] private Transform _visualTransform;
        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private Collider2D _collider;
        [SerializeField] private TextMeshPro _energyCostText;

        [Header("Animations")]
        [SerializeField] private Vector3 _onSelectedDelta;
        [SerializeField] private Vector3 _onSelectedScale;
        [SerializeField] private float _onSelectedAnimationSpeed;
        [SerializeField] private float _moveSpeed;

        private bool _isDragging;

        public (int, string) SortInfo { get; private set; }
        public Transform Transform { get => transform; }

        public event Action<ICardPresentation> Dragged;
        public event Action<Collider2D> Dropped;

        public void Init(CardData cardData)
        {
            _isDragging = false;
            SetEnergyCostText(cardData.EnergyCost);
            SetSprite(cardData.Sprite);
            SortInfo = (cardData.EnergyCost, cardData.CardName);
        }

        public void SetEnergyCostText(int cost) => _energyCostText.text = cost.ToString();

        public void SetSprite(Sprite sprite) => _spriteRenderer.sprite = sprite;

        public void OnDrag(PointerEventData eventData)
        {

            Vector2 worldPosition = Camera.main.ScreenToWorldPoint(eventData.position);
            transform.position = Vector2.Lerp(transform.position, worldPosition, 0.5f);
        }
        public void OnBeginDrag(PointerEventData eventData)
        {
            _isDragging = true;
            Dragged?.Invoke(this);
        }

        public void OnDrop(PointerEventData eventData)
        {
            _isDragging = false;
            Dropped?.Invoke(_collider);
        }

        public void OnCreated() => gameObject.SetActive(false);

        public void OnReceipt() => gameObject.SetActive(true);

        public void OnReleased()
        {
            OnSelectedAnimation(new(), new(1, 1, 1), _onSelectedAnimationSpeed);
            gameObject.SetActive(false);
        }

        public void MoveTo(Vector3 newVector3)
        {
            transform.DOMove(newVector3, _moveSpeed).SetEase(Ease.InCirc);
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (_isDragging)
                return;

            OnSelectedAnimation(_visualTransform.localPosition + _onSelectedDelta,
                _visualTransform.localScale + _onSelectedScale,
                _onSelectedAnimationSpeed);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (_isDragging)
                return;

            OnSelectedAnimation(new(), new(1, 1, 1), _onSelectedAnimationSpeed);
        }

        private void OnSelectedAnimation(Vector3 posDelta, Vector3 scaleDelta, float speed)
        {
            Sequence sequence = DOTween.Sequence();
            sequence.Append(_visualTransform.DOLocalMove(posDelta, speed)).
                Join(_visualTransform.DOScale(scaleDelta, speed)).
                SetEase(Ease.InCirc);

            sequence.Play();
        }
    }
}