using UnityEngine;

namespace AlwaysUp.Gameplay
{
    public class SineRotateObstacle : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField] private Sine _sine;
        [SerializeField] private ObstacleTarget[] _targets;

        private void Update()
        {
            foreach (ObstacleTarget target in _targets)
            {
                Vector3 targetRot = Vector3.forward * _sine.Get(target.TimeOffset);
                target.Transform.eulerAngles = targetRot * (target.Inverse ? -1 : 1);
            }
        }
    }
}
