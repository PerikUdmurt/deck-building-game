using System;
using System.Collections.Generic;

namespace YGameTemplate.Infrastructure.Score
{
    public class ScoreSystem
    {
        private int _score;
        private bool _canNegativeScore;

        private Dictionary<float, int> _criteriaScore;

        public ScoreSystem(bool negativeScore = false)
        {
            _canNegativeScore = negativeScore;
        }

        public event Action<int> ScoreChanged;

        public int GetScore() => _score;

        public void AddScore(int score)
        {
            if (score > 0)
            _score += score;
            ScoreChanged?.Invoke(_score);
        }

        public void ReduceScore(int score)
        {
            if (score < 0)
            _score -= score;
            
            if (_score < 0 && !_canNegativeScore)
                _score = 0;

            ScoreChanged?.Invoke(_score);
        }

        private void AddCriteria()
        {

        }
    }
}