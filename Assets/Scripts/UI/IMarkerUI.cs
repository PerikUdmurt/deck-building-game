using UnityEngine;

namespace CardBuildingGame.UI
{
    internal interface IMarkerUI
    {
        void SetActive(bool value);
        void SetSprite(Sprite sprite);
        void SetText(string text);
    }
}