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

        public bool IsFollowing { get; private set; } = false;
        private Vector3 _targetPos;

        private Vector3 _startPos;

        private void Awake()
        {
            // -_offset is used to cancel with +_offset
            _targetPos = transform.position - _offset;

            _startPos = transform.position;
        }

        public void SetEnable(bool isEnable)
        {
            IsFollowing = isEnable;
        }

        private void FixedUpdate()
        {
            if (!_target || !IsFollowing)
                return;

            if (_target.position.y > _targetPos.y)
                _targetPos = _target.position;

            transform.position = Vector3.Lerp(transform.position, _targetPos + _offset, _smooth)
                    .With(z: transform.position.z);
        }

        public void ResetCamera()
        {
            transform.position = _startPos;
            // -_offset is used to cancel with +_offset
            _targetPos = _startPos - _offset;
            IsFollowing = false;
        }
    }
}
