using AlwaysUp.Events;
using UnityEngine;

namespace AlwaysUp.Gameplay
{
    public class ScoreController : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField] private float _scoreMultiplier;
        [SerializeField] private Transform _target;
        [SerializeField] private float _minDeslocationToScore;

        [Header("Listening")]
        [SerializeField] private VoidEventChannelSO _onReset;
        [SerializeField] private VoidEventChannelSO _onStop;

        private float _startY;
        private bool _stopped;
        public static int CurrentScore { get; private set; }

        private void OnEnable()
        {
            _onReset.OnEventRaised += OnReset;
            _onStop.OnEventRaised += Stop;
        }

        private void OnDisable()
        {
            _onReset.OnEventRaised -= OnReset;
            _onStop.OnEventRaised -= Stop;
        }

        private void Awake()
        {
            _startY = _target.position.y;
        }

        private void Update()
        {
            if (_stopped)
                return;

            float distance = _target.transform.position.y - _startY;
            if (distance <= _minDeslocationToScore)
                return;

            int newScore = (int)((_target.transform.position.y - _startY - _minDeslocationToScore) * _scoreMultiplier);
            if (newScore > CurrentScore)
                CurrentScore = newScore;
        }

        private void OnReset()
        {
            CurrentScore = 0;
            _stopped = false;
        }

        public void Stop()
        {
            _stopped = true;
        }
    }
}
