using System;
using System.Collections.Generic;
using YGameTemplate.Services.StatisticsService;

namespace YGameTempate.Services.SaveLoad
{
    [Serializable]
    public class GameData
    {
        public StatisticsData GeneralStatistics;
        public List<StatisticsData> IntermidiateStatistics;

        public GameData()
        {
            GeneralStatistics = new StatisticsData("GeneralStatistics");
            IntermidiateStatistics = new List<StatisticsData>();
        }
    }
}