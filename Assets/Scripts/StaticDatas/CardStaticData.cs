using CardBuildingGame.Gameplay.Cards;
using CardBuildingGame.Gameplay.Characters;
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
        public TargetLayer TargetLayer;
        public List<CardEffect> Effects = new List<CardEffect>();

        public override string StaticDataID => Name;

    }
}