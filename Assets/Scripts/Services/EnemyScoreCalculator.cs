using System.Collections.Generic;
using System.Linq;
using YGameTemplate.Services.StatisticsService;
using static CardBuildingGame.Gameplay.Characters.Character;

namespace YGameTemplate.Infrastructure.Score
{
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