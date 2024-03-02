using UnityEngine;

namespace AlwaysUp.Gameplay
{
    public abstract class Obstacle : MonoBehaviour
    {
        protected int _colorIndex;

        protected virtual void Awake()
        {
            foreach (var collisionDetector in GetComponentsInChildren<ObstacleCollisionDetector>())
                collisionDetector.OnCollision += OnCollision;
        }

        virtual public void Init(int colorIndex)
        {
            _colorIndex = colorIndex;
        }

        abstract protected void OnCollision(GameObject go);
    }
}
