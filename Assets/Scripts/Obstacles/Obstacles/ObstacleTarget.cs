using UnityEngine;

namespace AlwaysUp.Gameplay
{
    [System.Serializable]
    public class ObstacleTarget
    {
        [SerializeField] private Transform _transform;
        [SerializeField] private bool _inverse;
        [SerializeField][Range(0, 2 * Mathf.PI)] private float _timeOffset;

        public Transform Transform => _transform;
        public bool Inverse => _inverse;
        public float TimeOffset => _timeOffset;
    }
}
