using System.Collections.Generic;
using UnityEngine;

namespace CardBuildingGame.Infrastructure.GameScenario
{
    [CreateAssetMenu(fileName = "Stage", menuName = "StaticData/Stage")]
    public class GameStageStaticData : ScriptableObject
    {
        public int NumbersOfFloors;
        public int NumbersOfShop;
        public int NumbersOfDialogue;
        public List<RoomStaticData> BattleRooms;
        public List<RoomStaticData> DialogueRooms;
        public List<RoomStaticData> BossRooms;
        public RoomStaticData ShopRoom;
    }
}