using UnityEngine;

namespace AlwaysUp.Gameplay
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class PingEffect : MonoBehaviour
    {
        [Header("Dependencies")]
        [SerializeField] private InputController _input;
        [SerializeField] private float _pingForce;

        private float _startY;
        private Rigidbody2D _rig;

        private void OnEnable()
        {
            _input.OnJump += Disable;
        }

        private void OnDisable()
        {
            _input.OnJump -= Disable;
        }

        private void Awake()
        {
            _startY = transform.position.y;
            _rig = GetComponent<Rigidbody2D>();
        }

        private void Update()
        {
            if (transform.position.y <= _startY)
            {
                _rig.velocity = Vector2.zero;
                _rig.AddForce(new Vector2(0, _pingForce), ForceMode2D.Impulse);
            }
        }

        private void Disable()
        {
            enabled = false;
        }
    }
}
