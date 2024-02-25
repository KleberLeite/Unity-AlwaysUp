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

        private Rigidbody2D _rig;

        private void Awake()
        {
            _rig = GetComponent<Rigidbody2D>();
        }

        private void OnEnable()
        {
            _input.OnJump += OnJump;
        }

        private void OnDisable()
        {
            _input.OnJump -= OnJump;
        }

        private void OnJump()
        {
            if (_rig.velocity.y < 0)
                _rig.velocity = Vector2.zero;

            _rig.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
        }
    }
}
