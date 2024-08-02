using System;
using System.Collections.Generic;
using YGameTemplate.Services.StatisticsService;

namespace YGameTemplate.Infrastructure.Score
{
    public class ScoreSystem
    {
        private int _totalScore;
        private readonly Statistics _statistics;
        private readonly EnemyScoreCalculator _enemyScoreCalculator;

        public ScoreSystem(GameStatisticsService gameStatisticsService)
        {
            _statistics = gameStatisticsService.GetStatistics(StandartStatisticsName.LevelStatistics.ToString());
            _statistics.Modified += RecalculateScore;
            _enemyScoreCalculator = new(_statistics);
        }

        public Dictionary<string, int> EnemyScore { get => _enemyScoreCalculator.EnemyScores; }

        public event Action<int> ScoreChanged;

        public int GetScore() => _totalScore;

        public void CleanUp() => _statistics.Modified -= RecalculateScore;
        
        private void RecalculateScore()
        {
            int totalScore = 0;
            totalScore += _enemyScoreCalculator.Calculate();
            SetScore(totalScore);
        }

        private void SetScore(int score)
        {
            _totalScore = score;
            ScoreChanged?.Invoke(score);
        }
    }
}