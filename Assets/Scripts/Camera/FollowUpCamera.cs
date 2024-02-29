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

        private Vector3 _targetPos;

        private void Awake()
        {
            _targetPos = transform.position;
        }

        private void FixedUpdate()
        {
            if (!_target)
                return;

            if (_target.position.y + _offset.y > transform.position.y)
                _targetPos = _target.position + _offset;

            transform.position = Vector3.Lerp(transform.position, _targetPos, _smooth)
                    .With(z: transform.position.z);
        }
    }
}
