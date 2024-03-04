using AlwaysUp.Utils;
using UnityEngine;

namespace AlwaysUp.Gameplay
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Ball : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField] private float _jumpForce;
        [SerializeField][Min(0)] private float _maxVerticalVelocity;
        [SerializeField] private ParticleSystem _explosionEffect;
        [SerializeField] private GameObject _ballGO;

        private Rigidbody2D _rig;

        private void Awake()
        {
            _rig = GetComponent<Rigidbody2D>();
        }

        public void Jump()
        {
            if (_rig.velocity.y < 0)
                _rig.velocity = Vector2.zero;

            _rig.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
        }

        public void Kill()
        {
            // Disable ball sprite
            _ballGO.SetActive(false);

            // Enable explosion effect
            _explosionEffect.gameObject.SetActive(true);
            _explosionEffect.Play();
        }

        public void Revive()
        {
            // Enable ball sprite
            _ballGO.SetActive(true);

            // Stop explosion effect (if it's running)
            _explosionEffect.gameObject.SetActive(false);
            _explosionEffect.Stop();
        }

        private void FixedUpdate()
        {
            if (_rig.velocity.y >= _maxVerticalVelocity)
                _rig.velocity = _rig.velocity.With(y: _maxVerticalVelocity);
        }
    }
}
