using System.Collections.Generic;
using UnityEngine;

namespace CardBuildingGame.StaticDatas
{
    [CreateAssetMenu(fileName = "NewDeckStaticData", menuName = "StaticData/Deck")]
    public class DeckStaticData: StaticData
    {
        public string ID;
        public List<CardStaticData> Cards = new List<CardStaticData>();

        public override string StaticDataID => ID;
    }
}