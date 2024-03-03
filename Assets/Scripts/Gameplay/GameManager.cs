using System.Collections;
using AlwaysUp.Events;
using UnityEngine;

namespace AlwaysUp.Gameplay
{
    public class GameManager : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField] private float _timeBetweenBallKilledAndReset;
        [SerializeField] private Animation _openCloseScene;

        [Header("Broadcasting")]
        [SerializeField] private VoidEventChannelSO _reset;
        [SerializeField] private VoidEventChannelSO _onBallKilled;

        private enum GameState
        {
            Preparing,
            Playing,
            End
        }

        private GameState _currentState;

        private void OnEnable()
        {
            _onBallKilled.OnEventRaised += OnBallKilled;
        }

        private void OnDisable()
        {
            _onBallKilled.OnEventRaised -= OnBallKilled;
        }

        private void Awake()
        {
            ChangeGameState(GameState.Preparing);
        }

        private void ChangeGameState(GameState newState)
        {
            switch (newState)
            {
                case GameState.Preparing:
                    HandlePreparingState();
                    break;
            }

            _currentState = newState;
        }

        private void HandlePreparingState()
        {
            Debug.Log("GameManager: HandlePreparingState");
            _reset.RaiseEvent();
            _openCloseScene.Play("OpenScene");
        }

        private void OnBallKilled()
        {
            StartCoroutine(WaitDelayAndReset());
        }

        private IEnumerator WaitDelayAndReset()
        {
            yield return new WaitForSeconds(_timeBetweenBallKilledAndReset);
            ChangeGameState(GameState.Preparing);
        }
    }
}
