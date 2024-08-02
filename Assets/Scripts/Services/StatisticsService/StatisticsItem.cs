using System;
using System.Collections.Generic;

namespace YGameTemplate.Services.StatisticsService
{
    [Serializable]
    public struct StatisticsItem
    {
        public string statisticsCritetia;
        public int Value;
    }

    [Serializable]
    public class StatisticsData
    {
        public string ID;
        public string DateOfCreate;
        public string DateOfModify;
        public List<StatisticsItem> Statistics;

        public StatisticsData(string id) 
        {
            ID = id;
            DateOfCreate = DateTime.Now.ToString();
            DateOfModify = DateTime.Now.ToString();
            Statistics = new List<StatisticsItem>();
        }
    }
}
