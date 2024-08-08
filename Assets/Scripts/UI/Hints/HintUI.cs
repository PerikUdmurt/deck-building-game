using TMPro;
using UnityEngine;
using CardBuildingGame.Infrastructure;

namespace YGameTemplate.UI.Hints
{
    public class HintUI : MonoBehaviour, IPooledObject
    {
        public TextMeshProUGUI hintName;
        public TextMeshProUGUI description;
        public RectTransform rectTransform;

        public void OnCreated()
        {
            gameObject.SetActive(false);
        }

        public void OnReceipt()
        {
            gameObject.SetActive(true);
        }

        public void OnReleased()
        {
            gameObject.SetActive(false);
        }

        private void Awake()
        {
            rectTransform = GetComponent<RectTransform>();
        }
    }
}
