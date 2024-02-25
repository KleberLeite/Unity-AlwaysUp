using UnityEngine;

namespace AlwaysUp.Gameplay
{
    public class SineObstacle : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField][Range(0, 3)] private float _range;
        [Tooltip("WARNING: when using random automatically the starting percentage is ignored")]
        [SerializeField][Range(0, 1)] private float _startPercentage = 0f;
        [Tooltip("WARNING: when using random automatically the starting percentage is ignored")]
        [SerializeField][Min(0)] private float _randomStart = 0f;
        [SerializeField][Min(0)] private float _speed = 1f;
        [SerializeField] private ObstacleTarget[] _targets;

        private float _startTimeOffset;

        private void Awake()
        {
            _startTimeOffset = Random.Range(0, _randomStart) + Mathf.PI / 2 * _startPercentage;
        }

        private void Update()
        {
            foreach (ObstacleTarget target in _targets)
            {
                Vector3 targetPos = Vector3.right * (
                    Mathf.Sin(Time.time * _speed + _startTimeOffset + target.TimeOffset) * _range
                );
                target.Transform.position = targetPos * (target.Inverse ? -1 : 1);
            }
        }
    }
}
