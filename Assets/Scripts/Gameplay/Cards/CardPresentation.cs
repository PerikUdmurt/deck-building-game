using CardBuildingGame.Infrastructure;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace CardBuildingGame.Gameplay.Cards
{
    [RequireComponent(typeof(Collider2D))]
    public class CardPresentation : MonoBehaviour, IPooledObject, ICardPresentation, IDropHandler, IDragHandler, IBeginDragHandler
    {   
        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private Collider2D _collider;
        [SerializeField] private TextMeshPro _energyCostText;
        [SerializeField] private float _moveSpeed;

        public (int, string) SortInfo { get; private set; }
        public Transform Transform { get => transform; }

        public event Action<ICardPresentation> Dragged;
        public event Action<Collider2D> Dropped;

        public void Init(CardData cardData)
        {
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
        public void OnBeginDrag(PointerEventData eventData) => Dragged?.Invoke(this);

        public void OnDrop(PointerEventData eventData) { Dropped?.Invoke(_collider); Debug.Log("Dropped"); }

        public void OnCreated() => gameObject.SetActive(false);

        public void OnReceipt() => gameObject.SetActive(true);

        public void OnReleased() => gameObject.SetActive(false);

        public void MoveTo(Vector3 newVector3)
        {
            transform.position = newVector3;
        }
    }
}