using CardBuildingGame.StaticDatas;
using UnityEngine;

namespace CardBuildingGame.Gameplay.Cards
{
    public abstract class CardEffect: ScriptableObject
    {
        public Sprite Icon;
        public string TextForMarker;
        public abstract void Play(ICardTarget target, ICardTarget source = null);
    }
}