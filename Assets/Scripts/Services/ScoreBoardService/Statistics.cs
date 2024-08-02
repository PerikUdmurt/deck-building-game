using System;
using System.Collections.Generic;
using UnityEngine;

namespace YGameTemplate.Services.StatisticsService
{
    public class Statistics
    {
        private readonly Dictionary<string, int> _statistics;
        private StatisticsData _statisticsData;
        public Statistics(StatisticsData statisticsData)
        {
            _statisticsData = statisticsData;

            _statistics = new()
            {
                {StatisticsCritetia.KillMonser.ToString(), 0},
                {StatisticsCritetia.CardPlayed.ToString(), 0},
            };

            foreach (var item in _statisticsData.Statistics)
            {
                _statistics.Add(item.statisticsCritetia, item.Value);
            }
        }

        public StatisticsData GetStatisticsData()
        {
            List<StatisticsItem> statisticsItems = new();
            foreach (var kvp in _statistics)
            {
                StatisticsItem newStat = new StatisticsItem()
                {
                    statisticsCritetia = kvp.Key,
                    Value = kvp.Value
                };
                statisticsItems.Add(newStat);
            }

            _statisticsData.Statistics = statisticsItems;
            return _statisticsData;
        }

        public int GetStatisticValue(string statistics)
        {
            if (_statistics.ContainsKey(statistics))
                return _statistics[statistics];
            else
            {
                Debug.Log($"Statistics with type {statistics} not founded");
                return 0;
            }
        }

        public void ModifyStatistics(string targetStat, ModifyType modifyType, int value)
        {
            if (!_statistics.ContainsKey(targetStat))
            {
                _statistics.Add(targetStat, 0);
                Debug.Log($"Statistics with name {targetStat} not founded. Created new statistics");
            }

            switch (modifyType)
            {
                case ModifyType.Plus:
                    _statistics[targetStat] += value;
                    break;

                case ModifyType.Minus:
                    _statistics[targetStat] -= value;
                    break;

                case ModifyType.Set:
                    _statistics[targetStat] = value;
                    break;
            }

            _statisticsData.DateOfModify = DateTime.Now.ToString();
        }

        public enum ModifyType
        {
            Minus = 0,
            Set = 1,
            Plus = 2,
        }
    }
}
