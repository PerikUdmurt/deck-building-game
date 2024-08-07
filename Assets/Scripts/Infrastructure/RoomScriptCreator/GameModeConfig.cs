using CardBuildingGame.StaticDatas;
using System.Collections.Generic;
using UnityEngine;

namespace CardBuildingGame.Infrastructure.GameScenario
{
    [CreateAssetMenu(fileName = "New Game Mode Config", menuName = "StaticData/GameModeConfig")]
    public class GameModeConfig : StaticData
    {
        public override string StaticDataID => GameMode.ToString();
        public GameMode GameMode;

        public List<GameStageStaticData> Stages;
    }
}