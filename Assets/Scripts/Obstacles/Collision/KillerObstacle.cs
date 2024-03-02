using AlwaysUp.Events;
using UnityEngine;

namespace AlwaysUp.Gameplay
{
    [RequireComponent(typeof(ObstacleCollisionDetector))]
    public class KillerObstacle : MonoBehaviour
    {
        [Header("Broadcasting")]
        [SerializeField] private VoidEventChannelSO _onCollision;

        private void Awake()
        {
            ObstacleCollisionDetector collisionDetector = GetComponent<ObstacleCollisionDetector>();
            collisionDetector.OnCollision += OnCollision;
        }

        private void OnCollision(GameObject gameObject, Ball ball)
        {
            _onCollision.RaiseEvent();
        }
    }
}
