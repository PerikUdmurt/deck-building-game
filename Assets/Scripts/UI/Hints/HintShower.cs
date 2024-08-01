using UnityEngine;
using UnityEngine.EventSystems;

namespace YGameTemplate.UI.Hints
{
    public class HintShower : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        private IHintService _hintManager;

        [SerializeField] private string _name;
        [SerializeField] private string _hintText;
        [SerializeField] private Color _hintColor;

        public void Construct(IHintService hintManager)
        {
            _hintManager = hintManager;
        }

        private void ShowHint()
        {
            _hintManager.CreateHint(_name, _hintText, Color.blue);
        }

        private void HideHint()
        {
            _hintManager.DeleteHint();
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            ShowHint();
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            HideHint();
        }
    }
}
