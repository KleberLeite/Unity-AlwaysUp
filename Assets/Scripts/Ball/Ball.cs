using AlwaysUp.Events;
using AlwaysUp.Utils;
using UnityEngine;

namespace AlwaysUp.Gameplay
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Ball : MonoBehaviour
    {
        [Header("Dependencies")]
        [SerializeField] private InputController _input;

        [Header("Settings")]
        [SerializeField] private float _jumpForce;
        [SerializeField][Min(0)] private float _maxVerticalVelocity;
        [SerializeField] private GameObject _ballGO;
        [SerializeField] private ParticleSystem _ballExplosionParticles;

        [Header("Listening")]
        [SerializeField] private VoidEventChannelSO _onReset;
        [SerializeField] private VoidEventChannelSO _onKilled;

        private Rigidbody2D _rig;

        private Vector3 _startPos;

        private void Awake()
        {
            _rig = GetComponent<Rigidbody2D>();
            _startPos = transform.position;
        }

        private void OnEnable()
        {
            _input.OnJump += OnJump;

            _onKilled.OnEventRaised += OnKilled;
            _onReset.OnEventRaised += OnReset;
        }

        private void OnDisable()
        {
            _input.OnJump -= OnJump;

            _onKilled.OnEventRaised -= OnKilled;
            _onReset.OnEventRaised -= OnReset;
        }

        private void OnJump()
        {
            if (_rig.velocity.y < 0)
                _rig.velocity = Vector2.zero;

            _rig.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
        }

        private void FixedUpdate()
        {
            if (_rig.velocity.y >= _maxVerticalVelocity)
                _rig.velocity = _rig.velocity.With(y: _maxVerticalVelocity);
        }

        private void OnKilled()
        {
            _ballGO.SetActive(false);
            _ballExplosionParticles.gameObject.SetActive(true);
            _ballExplosionParticles.Play();

            _input.OnJump -= OnJump;
        }

        private void OnReset()
        {
            _input.OnJump += OnJump;
            _ballGO.SetActive(true);
            _ballExplosionParticles.gameObject.SetActive(false);
            _rig.velocity = Vector2.zero;
            transform.position = _startPos;
        }
    }
}
