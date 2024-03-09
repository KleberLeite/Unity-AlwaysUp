using AlwaysUp.Events;
using TMPro;
using UnityEngine;

namespace AlwaysUp.Gameplay.UI
{
    public class HUDController : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField] private TMP_Text _scoreText;
        [SerializeField] private TMP_Text _highScoreText;
        [SerializeField] private InputController _input;
        [SerializeField] private Animator _animator;

        [Header("Listening")]
        [SerializeField] private VoidEventChannelSO _onBallKilled;

        private bool _enabled;

        private void OnEnable()
        {
            _input.OnJump += Enable;
            _onBallKilled.OnEventRaised += Disable;
        }

        private void OnDisable()
        {
            _input.OnJump -= Enable;
            _onBallKilled.OnEventRaised -= Disable;
        }

        private void Update()
        {
            _scoreText.text = $"Score: {ScoreController.CurrentScore}";
            _highScoreText.text = $"HighScore: {ScoreController.HighScore}";
        }

        private void Enable()
        {
            if (_enabled)
                return;

            _animator.SetBool("HUDIsOpen", true);
            _enabled = true;
        }

        private void Disable()
        {
            _animator.SetBool("HUDIsOpen", false);
            _enabled = false;
        }
    }
}
