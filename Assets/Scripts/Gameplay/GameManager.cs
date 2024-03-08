using System.Collections;
using AlwaysUp.Events;
using AlwaysUp.Utils;
using UnityEngine;
using AlwaysUp.Menus.Core;

namespace AlwaysUp.Gameplay
{
    public class GameManager : MonoBehaviour
    {
        [Header("Dependencies")]
        [SerializeField] private InputController _input;
        [SerializeField] private Menu _endGameMenu;

        [Header("Settings")]
        [SerializeField] private float _timeBetweenBallKilledAndOpenEndMenu;
        [SerializeField] private Animation _openCloseScene;
        [SerializeField] private OpenCloseSceneAnimationEndDetector _openCloseSceneEndDetector;

        [Header("Broadcasting")]
        [SerializeField] private VoidEventChannelSO _reset;
        [SerializeField] private VoidEventChannelSO _onBallKilled;

        private enum GameState
        {
            Preparing,
            Playing,
            End
        }

        public static GameManager Instance { get; private set; }

        private void OnEnable()
        {
            _onBallKilled.OnEventRaised += OnBallKilled;
            _openCloseSceneEndDetector.OnOpenEnds.AddListener(OnOpenScene);
            _openCloseSceneEndDetector.OnCloseEnds.AddListener(OnCloseScene);
        }

        private void OnDisable()
        {
            _onBallKilled.OnEventRaised -= OnBallKilled;
            _openCloseSceneEndDetector.OnOpenEnds.RemoveListener(OnOpenScene);
            _openCloseSceneEndDetector.OnCloseEnds.RemoveListener(OnCloseScene);
        }

        private void Awake()
        {
            Instance = this;
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
        }

        private void HandlePreparingState()
        {
            _reset.RaiseEvent();
            _input.enabled = false;
            _endGameMenu.Close();

            _openCloseScene.Play("OpenScene");
        }

        private void OnOpenScene()
        {
            _input.enabled = true;
        }

        private void OnBallKilled()
        {
            _input.enabled = false;

            StartCoroutine(WaitDelayAndOpenEndMenu());
        }

        private IEnumerator WaitDelayAndOpenEndMenu()
        {
            yield return new WaitForSeconds(_timeBetweenBallKilledAndOpenEndMenu);
            _endGameMenu.Open();
        }

        public void PlayAgain()
        {
            _openCloseScene.Play("CloseScene");
        }

        private void OnCloseScene()
        {
            ChangeGameState(GameState.Preparing);
        }
    }
}
