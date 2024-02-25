using System;
using UnityEngine;

namespace AlwaysUp.Gameplay
{
    [System.Serializable]
    public class Sine
    {
        [Header("Settings")]
        [SerializeField] private float _range;
        [Tooltip("WARNING: when using random automatically the starting percentage is ignored")]
        [SerializeField][Range(-1, 1)] private float _startPercentage = 0f;
        [Tooltip("WARNING: when using random automatically the starting percentage is ignored")]
        [SerializeField][Min(0)] private float _randomStart = 0f;
        [SerializeField] private float _speed = 1f;

        public float Speed { get => _speed; set => _speed = value; }

        private float _startTimeOffset;
        private bool _initialized;

        public void Init()
        {
            _startTimeOffset = UnityEngine.Random.Range(0, _randomStart) + (float)Math.Asin(_startPercentage);
            _initialized = true;
        }

        public float Get(float? timeOffset = null)
        {
            if (!_initialized)
                Init();

            return Mathf.Sin(Time.time * _speed + _startTimeOffset + (timeOffset ?? 0)) * _range;
        }
    }
}
