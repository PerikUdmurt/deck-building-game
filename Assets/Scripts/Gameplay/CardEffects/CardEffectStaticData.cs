using CardBuildingGame.StaticDatas;
using System;
using UnityEngine;

namespace CardBuildingGame.Gameplay.Cards
{
    public abstract class CardEffectStaticData: ScriptableObject
    {
        public Sprite Icon;
        public string Description = "";

        public abstract string TextForMarker { get;}
        public abstract void Play(ICardTarget target, ICardTarget source = null);
    }
}