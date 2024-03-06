using TMPro;
using UnityEngine;

namespace AlwaysUp.Gameplay.UI
{
    public class HUDController : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField] private TMP_Text _scoreText;
        [SerializeField] private TMP_Text _highScoreText;

        private void Update()
        {
            _scoreText.text = $"Score: {ScoreController.CurrentScore}";
            _highScoreText.text = $"HighScore: {ScoreController.HighScore}";
        }
    }
}
