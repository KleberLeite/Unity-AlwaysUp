using UnityEngine;

namespace AlwaysUp.Gameplay
{
    public class SineRotateEffect : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField] private Sine _sine;
        [SerializeField] private SineEffectTarget[] _targets;

        private void Update()
        {
            foreach (SineEffectTarget target in _targets)
            {
                Vector3 targetRot = Vector3.forward * _sine.Get(target.TimeOffset);
                target.Transform.eulerAngles = targetRot * (target.Inverse ? -1 : 1);
            }
        }
    }
}
