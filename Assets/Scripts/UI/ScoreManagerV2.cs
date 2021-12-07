using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class ScoreManagerV2 : MonoBehaviour
    {
        public int maxScoreForLevel;
        private int _currentScore;
        private int _currentScoreOnSlider;

        private Slider _scoreSlider;

        private void Start()
        {
            _scoreSlider = GameObject.FindWithTag("ScoreSlider").GetComponent<Slider>();
            _scoreSlider.maxValue = maxScoreForLevel;
        }


        private void Update()
        {
            if (_currentScore <= _currentScoreOnSlider) return;
            if ((_currentScore - _currentScoreOnSlider) / 100 > 1)
            {
                _currentScoreOnSlider += (_currentScore - _currentScoreOnSlider) / 100;
            }
            else
            {
                _currentScoreOnSlider++;
            }

            _scoreSlider.value = _currentScoreOnSlider;
        }

        public void SetMaxScoreForLevel(int score)
        {
            maxScoreForLevel = score;
            _scoreSlider.maxValue = maxScoreForLevel;
        }

        public void AddScore(int score)
        {
            if (_currentScore + score >= maxScoreForLevel)
            {
                _currentScore = maxScoreForLevel;
            }
            else
            {
                _currentScore += score;
            }
        }

        public int GetCurrentScore()
        {
            return _currentScore;
        }

        public float GetCurrentScorePercent()
        {
            return _currentScore / (float)maxScoreForLevel * 100;
        }
    }
}