using System.Collections.Generic;
using UnityEngine;
using static CardBuildingGame.Gameplay.Characters.Character;

namespace CardBuildingGame.StaticDatas
{
    [CreateAssetMenu(fileName = "NewDeckStaticData", menuName = "StaticData/Deck")]
    public class DeckStaticData: StaticData
    {
        public CharacterType CharacterType;
        public int ID;
        public List<CardStaticData> Cards = new List<CardStaticData>();

        public override string StaticDataID => CharacterType.ToString() + ID.ToString();
    }
}