using UnityEngine;

namespace AlwaysUp.Gameplay
{
    public class RotateObstacle : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField][Range(-360, 360)] private float _startAngle = 0f;
        [Tooltip("Add to \"StartAngle\" a random angle between \"-RandomStartAngle\" and \"RandomStartAngle\"")]
        [SerializeField][Min(0)] private float _randomStartAngle = 0f;
        [SerializeField] private float _speed = 0f;
        [SerializeField] private ObstacleTarget[] _targets;

        private Vector3 _startAngleOffset;

        private void Awake()
        {
            _startAngleOffset = Vector3.forward * (Random.Range(-_randomStartAngle, _randomStartAngle) + _startAngle);
        }

        private void Update()
        {
            foreach (ObstacleTarget target in _targets)
            {
                Vector3 targetRot = _startAngleOffset + Vector3.forward * (Time.time + target.TimeOffset) * _speed;
                target.Transform.eulerAngles = targetRot * (target.Inverse ? -1 : 1);
            }
        }
    }
}
