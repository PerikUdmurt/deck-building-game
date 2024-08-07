using CardBuildingGame.Services;
using CardBuildingGame.StaticDatas;
using System.Collections.Generic;
using System.Text;
using Unity.VisualScripting;
using UnityEngine;
using static CardBuildingGame.Infrastructure.GameScenario.RoomStaticData;

namespace CardBuildingGame.Infrastructure.GameScenario
{
    public class RoomScriptsCreator
    {
        private readonly IStaticDataService _staticDataService;

        public RoomScriptsCreator(IStaticDataService staticDataService)
        {
            _staticDataService = staticDataService;
        }

        public Dictionary<int, List<RoomStaticData>> CreateGameGraph(GameMode gameMode)
        {
            GameModeConfig gameConfig = GetGameStaticData(gameMode);

            Dictionary<int, List<RoomStaticData>> result = new Dictionary<int, List<RoomStaticData>>();
            AddStageGraphsToGameGraph(gameConfig, result);

            DebugGameGraph(result);

            return result;
        }

        private void DebugGameGraph(Dictionary<int, List<RoomStaticData>> debuggingCollection)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Создан граф уровней со следующими параметрами:");
            sb.AppendLine(new string('-', 30));

            foreach (var floor in debuggingCollection)
            {
                DebugFloor(sb, floor);
            }

            Debug.Log(sb.ToString());
        }

        private static void DebugFloor(StringBuilder sb, KeyValuePair<int, List<RoomStaticData>> floor)
        {
            sb.AppendLine($"FLOOR {floor.Key}");
            foreach (var room in floor.Value)
            {
                sb.AppendLine(room.ToString());
            }
            sb.AppendLine(new string('-', 30));
        }

        private void AddStageGraphsToGameGraph(GameModeConfig gameConfig, Dictionary<int, List<RoomStaticData>> result)
        {
            for (int stage = 0; stage < gameConfig.Stages.Count; stage++)
            {
                var stageRooms = CreateStageGraph(gameConfig.Stages[stage]);

                int additionalIndex = 0;
                for (int i = 0; i < stage; i++)
                    additionalIndex += gameConfig.Stages[i].NumbersOfFloors;

                foreach (var room in stageRooms)
                    result.Add(room.Key + additionalIndex, room.Value);
            }
        }

        private Dictionary<int, List<RoomStaticData>> CreateStageGraph(GameStageStaticData stageData)
        {
            Dictionary<int, List<RoomStaticData>> result = new Dictionary<int, List<RoomStaticData>>();
            AddBattleRooms(stageData, result);
            AddBossRooms(stageData, result);
            AddEventRooms(stageData, result);
            AddShopRooms(stageData, result);
            return result;
        }

        private void AddShopRooms(GameStageStaticData stageData, Dictionary<int, List<RoomStaticData>> result)
        {
            int NumOfFloors = stageData.NumbersOfFloors;
            int NumOfShop = stageData.NumbersOfShop;

            for (int a = 0; a < NumOfShop; a++)
            {
                var room = GetRandomRoomByCriteria(RoomType.Shop, stageData);
                int randomFloor = Random.Range(1, NumOfFloors - 1);
                result.AddRoom(randomFloor, room);
            }
        }

        private void AddEventRooms(GameStageStaticData stageData, Dictionary<int, List<RoomStaticData>> result)
        {
            int NumOfFloors = stageData.NumbersOfFloors;
            int NumOfEvent = stageData.NumbersOfDialogue;
            List<RoomStaticData> eventPool = stageData.DialogueRooms;

            for (int a = 0; a < NumOfEvent ; a++)
            {
                var room = GetRandomRoomByCriteria(RoomType.Dialogue, stageData);
                int randomFloor = Random.Range(1, NumOfFloors - 1);
                result.AddRoom(randomFloor, room);
                eventPool.Remove(room);
            }
        }

        private void AddBattleRooms(GameStageStaticData stageData, Dictionary<int, List<RoomStaticData>> result)
        {
            for (int floor = 1; floor < stageData.NumbersOfFloors; floor++)
            {
                var value = GetRandomRoomByCriteria(RoomType.Battle, stageData);
                result.AddRoom(floor, value);
            }
        }
        private void AddBossRooms(GameStageStaticData stageData, Dictionary<int, List<RoomStaticData>> result)
        {
            int floor = stageData.NumbersOfFloors;
            var value = GetRandomRoomByCriteria(RoomType.Boss, stageData);
            result.AddRoom(floor, value);
        }

        private RoomStaticData GetRandomRoomByCriteria(RoomType roomType, GameStageStaticData data)
        {
            List<RoomStaticData> targetList = GetRoomList(roomType, data);

            if (targetList.Count == 0)
                return GetRandomRoomByCriteria(RoomType.Battle, data);
             
            int randomIndex = Random.Range(0, targetList.Count - 1);
            return targetList[randomIndex];
        }

        private List<RoomStaticData> GetRoomList(RoomType roomType, GameStageStaticData data) 
        {
            switch (roomType)
            {
                case RoomType.Battle:
                    return data.BattleRooms;
                case RoomType.Dialogue:
                    return data.DialogueRooms;
                case RoomType.Shop:
                    return new() { data.ShopRoom };
                case RoomType.Boss:
                    return data.BossRooms;
            }

            throw new System.Exception("Не задан тип комнаты в необходимом диапазоне");
        }

        private GameModeConfig GetGameStaticData(GameMode gameMode)
        {
            _staticDataService.GetStaticData(gameMode.ToString(), out StaticData staticData);
            return (GameModeConfig)staticData;
        }
    }

    public static class RoomScriptExtension
    {
        public static void AddRoom(this Dictionary<int, List<RoomStaticData>> mutableCollection, int floor, RoomStaticData newRoom)
        {
            if (!mutableCollection.ContainsKey(floor))
                mutableCollection.Add(floor, new List<RoomStaticData>());

            mutableCollection[floor].Add(newRoom);
        }
    }
}