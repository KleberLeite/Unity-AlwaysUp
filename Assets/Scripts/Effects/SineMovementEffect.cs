using AlwaysUp.Utils;
using UnityEngine;

namespace AlwaysUp.Gameplay
{
    public class SineMovementEffect : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField] private Sine _sine;
        [SerializeField] private SineEffectTarget[] _targets;

        private void Update()
        {
            foreach (SineEffectTarget target in _targets)
            {
                Vector3 targetPos = Vector3.right * _sine.Get(target.TimeOffset);
                target.Transform.position = (targetPos * (target.Inverse ? -1 : 1)).With(y: target.Transform.position.y);
            }
        }
    }
}
