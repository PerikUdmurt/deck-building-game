using CardBuildingGame.Gameplay.Cards;
using System.Collections.Generic;
using UnityEngine;

namespace CardBuildingGame.StaticDatas
{
    [CreateAssetMenu(fileName = "NewCardStaticData", menuName = "StaticData/Card")]
    public class CardStaticData : StaticData
    {
        public string Name = "EmptyCard";
        public int EnergyCost = 0;
        public Sprite Sprite;
        public LayerMask TargetLayer;
        public List<CardEffect> Effects = new List<CardEffect>();

        public override string StaticDataID => Name;
    }
}