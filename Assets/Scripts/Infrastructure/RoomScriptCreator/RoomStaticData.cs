using CardBuildingGame.Infrastructure.StateMachine;
using CardBuildingGame.StaticDatas;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace CardBuildingGame.Infrastructure.GameScenario
{
    [CreateAssetMenu(fileName = "New Room Script", menuName = "StaticData/RoomScripts")]
    public class RoomStaticData : ScriptableObject
    {
        public Sprite RoomIcon;
        public SceneName SceneName;
        public RoomType CurrentRoomType;
        public int Difficulty;

        public List<CharacterStaticData> Enemies;

        public enum RoomType
        {
            Battle = 0,
            Dialogue = 1,
            Shop = 2,
            Boss = 3
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"FileName: {name}");
            sb.AppendLine($"RoomType: {CurrentRoomType}");
            sb.AppendLine($"SceneName: {SceneName}");
            sb.AppendLine($"Difficulty: {Difficulty}");
            sb.AppendLine($"Enemies:");

            foreach ( var character in Enemies )
                sb.AppendLine($"    - {character.CharacterType}");
            
            return sb.ToString();
        }
    }
}