using UnityEngine;
using UnityEngine.Events;

namespace AlwaysUp.Utils
{
    public class OpenCloseSceneAnimationEndDetector : MonoBehaviour
    {
        [SerializeField] public UnityEvent OnOpenEnds;
        [SerializeField] public UnityEvent OnCloseEnds;

        private void OnOpenEnd()
        {
            OnOpenEnds?.Invoke();
        }

        private void OnCloseEnd()
        {
            OnCloseEnds?.Invoke();
        }
    }
}
