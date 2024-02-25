using UnityEngine;

namespace AlwaysUp.Gameplay
{
    public class SineMovementObstacle : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField] private Sine _sine;
        [SerializeField] private ObstacleTarget[] _targets;

        private void Update()
        {
            foreach (ObstacleTarget target in _targets)
            {
                Vector3 targetPos = Vector3.right * _sine.Get(target.TimeOffset);
                target.Transform.position = targetPos * (target.Inverse ? -1 : 1);
            }
        }
    }
}
