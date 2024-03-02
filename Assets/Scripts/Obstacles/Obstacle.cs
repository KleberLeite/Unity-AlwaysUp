using UnityEngine;

namespace AlwaysUp.Gameplay
{
    public abstract class Obstacle : MonoBehaviour
    {
        protected virtual void Awake()
        {
            foreach (var collisionDetector in GetComponentsInChildren<ObstacleCollisionDetector>())
                collisionDetector.OnCollision += OnCollision;
        }

        protected abstract void OnCollision(GameObject go);
    }
}
