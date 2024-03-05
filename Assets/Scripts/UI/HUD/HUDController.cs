using TMPro;
using UnityEngine;

namespace AlwaysUp.Gameplay.UI
{
    public class HUDController : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField] private TMP_Text _scoreText;

        private void Update()
        {
            _scoreText.text = $"Score: {ScoreController.CurrentScore}";
        }
    }
}
