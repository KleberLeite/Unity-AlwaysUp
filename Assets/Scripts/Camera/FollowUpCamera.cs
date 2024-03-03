using AlwaysUp.Events;
using AlwaysUp.Utils;
using UnityEngine;

namespace AlwaysUp.Gameplay
{
    [RequireComponent(typeof(Camera))]
    public class FollowUpCamera : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField] private float _smooth;
        [SerializeField] private Vector3 _offset;
        [SerializeField] private Transform _target;

        [Header("Dependencies")]
        [SerializeField] private InputController _input;

        [Header("Listening")]
        [SerializeField] private VoidEventChannelSO _onReset;

        private bool _follow = false;
        private Vector3 _targetPos;

        private Vector3 _startPos;

        private void OnEnable()
        {
            _input.OnJump += Enable;

            _onReset.OnEventRaised += OnReset;
        }

        private void OnEDisable()
        {
            _input.OnJump -= Enable;

            _onReset.OnEventRaised -= OnReset;
        }

        private void Awake()
        {
            // -_offset is used to cancel with +_offset
            _targetPos = transform.position - _offset;

            _startPos = transform.position;
        }

        private void Enable()
        {
            _follow = true;
        }

        private void FixedUpdate()
        {
            if (!_target || !_follow)
                return;

            if (_target.position.y > _targetPos.y)
                _targetPos = _target.position;

            transform.position = Vector3.Lerp(transform.position, _targetPos + _offset, _smooth)
                    .With(z: transform.position.z);
        }

        private void OnReset()
        {
            transform.position = _startPos;
            _follow = false;
        }
    }
}
