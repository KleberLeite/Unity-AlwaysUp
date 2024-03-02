using UnityEngine;
using UnityEngine.Events;

namespace AlwaysUp.Gameplay
{
    public class ObstacleCollisionDetector : MonoBehaviour
    {
        public UnityEvent<Ball> OnCollision { get; set; }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
            {
                Ball ball = other.gameObject.GetComponent<Ball>();
                OnCollision?.Invoke(ball);
            }
        }
    }
}
