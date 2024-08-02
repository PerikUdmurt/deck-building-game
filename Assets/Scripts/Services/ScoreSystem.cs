using System;
using System.Collections.Generic;
using System.Linq;
using YGameTemplate.Services.StatisticsService;
using static CardBuildingGame.Gameplay.Characters.Character;

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

    public class EnemyScoreCalculator
    {
        private readonly Statistics _statistics;
        private readonly Dictionary<string, int> _scores;

        public EnemyScoreCalculator(Statistics statistics)
        {
            _statistics = statistics;
            _scores = new();
        }

        public Dictionary<string, int> EnemyScores { get => _scores; }

        public int Calculate()
        {
            UpdateCriteria();
            return _scores.Values.Sum();
        }

        private void UpdateCriteria()
        {
            AddCriteria(CharacterType.Enemy1, 5);
            AddCriteria(CharacterType.Enemy2, 6);
        }

        private void AddCriteria(CharacterType characterType, int multiplier)
        {
            int value = _statistics.GetStatisticValue($"kill_{characterType}");
            value *= multiplier;

            if (!_scores.ContainsKey($"kill_{characterType}"))
                _scores.Add($"kill_{characterType}", value);
            else
                _scores[$"kill_{characterType}"] = value;
        }
    }
}