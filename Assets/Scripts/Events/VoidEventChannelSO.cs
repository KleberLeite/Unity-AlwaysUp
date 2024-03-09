using UnityEngine;
using UnityEngine.Events;

namespace AlwaysUp.Events
{
    [CreateAssetMenu(menuName = "Events/Void Event Channel")]
    public class VoidEventChannelSO : ScriptableObject
    {
        public UnityAction OnEventRaised { get; set; }

        public void RaiseEvent()
        {
            OnEventRaised?.Invoke();
        }
    }
}
