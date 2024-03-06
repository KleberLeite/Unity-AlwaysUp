using AlwaysUp.Gameplay;
using AlwaysUp.Menus.Core;
using TMPro;
using UnityEngine;

namespace AlwaysUp.Menus.Gameplay
{
    public class EndGameMenu : Menu
    {
        [Header("Settings")]
        [SerializeField] private TMP_Text _scoreText;
        [SerializeField] private TMP_Text _highScoreText;

        private void OnEnable()
        {
            _scoreText.text = $"Score: {ScoreController.CurrentScore}";
            _highScoreText.text = $"HighScore: {ScoreController.HighScore}";
        }
    }
}
