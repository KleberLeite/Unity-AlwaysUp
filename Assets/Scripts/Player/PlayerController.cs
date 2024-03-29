using AlwaysUp.Events;
using UnityEngine;

namespace AlwaysUp.Gameplay
{
    public class PlayerController : MonoBehaviour
    {
        [Header("Dependencies")]
        [SerializeField] private InputController _input;
        [SerializeField] private Ball _ball;
        [SerializeField] private PingEffect _pingEffect;

        [Header("Listening")]
        [SerializeField] private VoidEventChannelSO _onReset;
        [SerializeField] private VoidEventChannelSO _onBallKilled;

        private Vector3 _ballStartPos;

        private bool _readingInput;

        private void OnEnable()
        {
            _onReset.OnEventRaised += OnReset;
            _onBallKilled.OnEventRaised += OnBallKilled;

            _input.OnJump += OnClickJump;
        }

        private void OnDisable()
        {
            _onReset.OnEventRaised -= OnReset;
            _onBallKilled.OnEventRaised -= OnBallKilled;

            _input.OnJump -= OnClickJump;
        }

        private void Awake()
        {
            _ballStartPos = _ball.transform.position;
        }

        private void OnClickJump()
        {
            if (!_readingInput)
                return;

            if (_pingEffect.enabled)
                _pingEffect.enabled = false;

            _ball.Jump();
        }

        private void OnBallKilled()
        {
            _ball.Kill();
            _readingInput = false;
        }

        private void OnReset()
        {
            ResetBall();
            _readingInput = true;
        }

        private void ResetBall()
        {
            _ball.Revive();
            _ball.transform.position = _ballStartPos;
            _pingEffect.enabled = true;
        }
    }
}
