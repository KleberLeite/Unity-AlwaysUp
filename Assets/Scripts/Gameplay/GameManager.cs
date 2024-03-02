using AlwaysUp.Events;
using UnityEngine;

namespace AlwaysUp.Gameplay
{
    public class GameManager : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField] private GameObject _ball;
        [SerializeField] private GameObject _ballExplosionParticlesPrefab;

        [Header("Listening")]
        [SerializeField] private VoidEventChannelSO _onBallKilled;

        private void OnEnable()
        {
            _onBallKilled.OnEventRaised += OnBallKilled;
        }

        private void OnDisable()
        {
            _onBallKilled.OnEventRaised -= OnBallKilled;
        }

        private void OnBallKilled()
        {
            _ball.SetActive(false);
            Instantiate(_ballExplosionParticlesPrefab, _ball.transform.position, Quaternion.identity);
        }
    }
}
