using UnityEngine;
using UnityEngine.Events;

namespace AlwaysUp.Gameplay
{
    public class InputController : MonoBehaviour
    {
        public UnityAction OnJump { get; set; }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
                OnJump?.Invoke();
        }
    }
}
