using TMPro;
using UnityEngine;

namespace CardBuildingGame.UI
{
    public class MarkerUI : MonoBehaviour, IMarkerUI
    {
        [SerializeField] private TextMeshPro _textMeshPro;
        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private Sprite _startSprite;

        public void Awake()
        {
            if (_startSprite != null)
            _spriteRenderer.sprite = _startSprite;
        }

        public void SetActive(bool value)
        {
            gameObject.SetActive(value);
        }

        public void SetText(string text)
        {
            _textMeshPro.text = text;
        }

        public void SetSprite(Sprite sprite) 
        { 
            _spriteRenderer.sprite = sprite;
        }
    }
}