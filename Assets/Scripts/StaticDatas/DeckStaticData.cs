﻿using System.Collections.Generic;
using UnityEngine;

namespace CardBuildingGame.StaticDatas
{
    [CreateAssetMenu(fileName = "NewDeckStaticData", menuName = "StaticData/Deck")]
    public class DeckStaticData: ScriptableObject
    {
        public List<CardStaticData> Cards = new List<CardStaticData>();
    }
}