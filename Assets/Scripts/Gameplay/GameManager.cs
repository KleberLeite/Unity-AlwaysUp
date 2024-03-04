using System.Collections;
using AlwaysUp.Events;
using AlwaysUp.Utils;
using UnityEngine;

namespace AlwaysUp.Gameplay
{
    public class GameManager : MonoBehaviour
    {
        [Header("Dependencies")]
        [SerializeField] private InputController _input;

        [Header("Settings")]
        [SerializeField] private float _timeBetweenBallKilledAndReset;
        [SerializeField] private Animation _openCloseScene;
        [SerializeField] private AnimationEventHandler _onOpenScene;

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
            _onOpenScene.OnEventRaised.AddListener(OnOpenScene);
        }

        private void OnDisable()
        {
            _onBallKilled.OnEventRaised -= OnBallKilled;
            _onOpenScene.OnEventRaised.RemoveListener(OnOpenScene);
        }

        private void Start()
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
            _reset.RaiseEvent();
            _input.enabled = false;

            _openCloseScene.Play("OpenScene");
        }

        private void OnOpenScene()
        {
            _input.enabled = true;
        }

        private void OnBallKilled()
        {
            _input.enabled = false;

            StartCoroutine(WaitDelayAndReset());
        }

        private IEnumerator WaitDelayAndReset()
        {
            yield return new WaitForSeconds(_timeBetweenBallKilledAndReset);
            ChangeGameState(GameState.Preparing);
        }
    }
}
