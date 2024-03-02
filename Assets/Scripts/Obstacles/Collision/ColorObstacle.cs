using System.Collections.Generic;
using AlwaysUp.Events;
using UnityEngine;

namespace AlwaysUp.Gameplay
{
    public class ColorObstacle : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField] private SpriteRenderer[] _obstacles;

        [Header("Broadcasting")]
        [SerializeField] private IntEventChannelSO _onHitColor;

        private Dictionary<SpriteRenderer, int> _obstaclesColorIndex;
        private ObstacleCollisionDetector _collisionDetector;

        private void OnEnable()
        {
            if (!_collisionDetector)
                _collisionDetector = GetComponent<ObstacleCollisionDetector>();

            _collisionDetector.OnCollision += OnCollision;

            SetObstaclesColors();
        }

        private void SetObstaclesColors()
        {
            _obstaclesColorIndex = new Dictionary<SpriteRenderer, int>();
            int j = 0;
            for (int i = 0; i < GameColors.Count; i++)
            {
                if (i == GameColorController.CurrentColorIndex)
                    continue;

                _obstacles[j].color = GameColors.GetColorByIndex(i);
                _obstaclesColorIndex.Add(_obstacles[j], i);

                j++;
            }
        }

        private void OnDisable()
        {
            _collisionDetector.OnCollision -= OnCollision;
        }

        private void OnCollision(GameObject obstacle, Ball ball)
        {
            int colorIndex = _obstaclesColorIndex[obstacle.GetComponent<SpriteRenderer>()];
            _onHitColor.RaiseEvent(colorIndex);
        }
    }
}
