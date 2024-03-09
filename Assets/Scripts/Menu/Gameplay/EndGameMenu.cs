using System.Collections;
using AlwaysUp.Gameplay;
using AlwaysUp.Menus.Core;
using TMPro;
using UnityEngine;

namespace AlwaysUp.Menus.Gameplay
{
    public class EndGameMenu : Menu
    {
        [Header("Settings")]
        [SerializeField] private Animator _animator;
        [SerializeField] private TMP_Text _scoreText;
        [SerializeField] private TMP_Text _highScoreText;
        [SerializeField] private AnimationCurve _countCurve;

        private bool _countingScore;
        private bool _canSkip;
        private bool _playAgain;

        private void OnEnable()
        {
            _canSkip = false;
            _playAgain = false;
            _countingScore = false;
            _animator.SetBool("NewHighScore", ScoreController.NewHighScore);
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (_playAgain)
                {
                    GameManager.Instance.PlayAgain();
                    _playAgain = false;
                }

                if (_canSkip)
                {
                    if (_countingScore)
                    {
                        // Jump score counting
                        StopAllCoroutines();
                        OnEndCountUpScore();
                    }
                    else
                        _animator.CrossFade(ScoreController.NewHighScore ? "NewHighScore" : "PlayAgain", .1f);
                }
            }
        }

        private void OnShowScore()
        {
            _canSkip = true;
            StartCoroutine(CountUpScoreCoroutine());
        }

        private IEnumerator CountUpScoreCoroutine()
        {
            _countingScore = true;
            float current = 0;
            float currentTime = 0;
            while (current < ScoreController.CurrentScore)
            {
                currentTime += Time.deltaTime;
                current = ScoreController.HighScore * _countCurve.Evaluate(currentTime);
                _scoreText.text = $"Score: {(int)current}";
                yield return null;
            }

            OnEndCountUpScore();
        }

        private void OnEndCountUpScore()
        {
            _scoreText.text = $"Score: {ScoreController.CurrentScore}";
            _countingScore = false;
            _animator.SetTrigger("EndCountScore");
        }

        private void OnShowHighScore()
        {
            _countingScore = false;
            _highScoreText.text = $"HighScore: {ScoreController.HighScore}";
        }

        private void OnEnterPlayAgain()
        {
            _canSkip = false;
            _playAgain = true;
        }
    }
}
