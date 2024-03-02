using UnityEngine;
using UnityEngine.Events;

namespace AlwaysUp.Gameplay
{
    public class ObstacleCollisionDetector : MonoBehaviour
    {
        public UnityAction<GameObject, Ball> OnCollision { get; set; }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
            {
                Ball ball = other.gameObject.GetComponent<Ball>();
                OnCollision?.Invoke(gameObject, ball);
            }
        }
    }
}
