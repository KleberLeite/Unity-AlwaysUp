using UnityEngine;
using UnityEngine.Events;

namespace AlwaysUp.Utils
{
    public class AnimationEventHandler : MonoBehaviour
    {
        [SerializeField] public UnityEvent OnEventRaised;

        private void OnEvent()
        {
            OnEventRaised?.Invoke();
        }
    }
}
