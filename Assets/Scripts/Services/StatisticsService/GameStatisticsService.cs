using System.Collections.Generic;
using UnityEngine;
using YGameTempate.Services.SaveLoad;

namespace YGameTemplate.Services.StatisticsService
{
    public class GameStatisticsService : IDataSaver
    {
        private Statistics _generalStatistics; 
        private readonly Dictionary<string, Statistics> _intermidiateStatistics;

        public GameStatisticsService(StatisticsData generalStatistics, List<StatisticsData> intermidiateStats = null)
        {
            _generalStatistics = new(generalStatistics);

            _intermidiateStatistics = new();
            if (intermidiateStats != null) 
            {
                foreach (var statistic in intermidiateStats)
                    _intermidiateStatistics.Add(statistic.ID, new(statistic));
            }
        }

        public Statistics GeneneralStatistics { get => _generalStatistics; }

        public Statistics GetStatistics(string id)
        {
            if (!_intermidiateStatistics.ContainsKey(id))
            {
                Debug.Log($"Statistics with id '{id}' not founded");
                return null;
            }
            return _intermidiateStatistics[id];
        }

        public void CreateStatistics(string id)
        {
            if (!_intermidiateStatistics.ContainsKey(id))
            _intermidiateStatistics.Add(id, new(new(id)));
            else _intermidiateStatistics[id] = new(new(id));
        }

        public void DeleteStatistics(string id)
        {
            if (_intermidiateStatistics.ContainsKey(id))
            _intermidiateStatistics.Remove(id);
        }

        public void ClearIntermidiateStatistics()
            => _intermidiateStatistics.Clear();

        public void SaveData(ref GameData gameData)
        {
            gameData.GeneralStatistics = _generalStatistics.GetStatisticsData();
            
            List<StatisticsData> interstat = new List<StatisticsData>();
            foreach (var statistic in _intermidiateStatistics.Values)
                interstat.Add(statistic.GetStatisticsData());

            gameData.IntermidiateStatistics = interstat;
        }
    }

    public enum StandartStatisticsName
    {
        GameModeStatistics = 0
    }
}
