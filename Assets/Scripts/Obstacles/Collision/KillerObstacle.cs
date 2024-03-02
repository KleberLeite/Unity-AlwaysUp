using AlwaysUp.Events;
using UnityEngine;

namespace AlwaysUp.Gameplay
{
    public class KillerObstacle : Obstacle
    {
        [Header("Broadcasting")]
        [SerializeField] private VoidEventChannelSO _onCollision;

        override protected void OnCollision(GameObject gameObject)
        {
            _onCollision.RaiseEvent();
        }
    }
}
