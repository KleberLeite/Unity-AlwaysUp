using AlwaysUp.Events;
using UnityEngine;

namespace AlwaysUp.Gameplay
{
    public class CameraController : MonoBehaviour
    {
        [Header("Dependencies")]
        [SerializeField] private FollowUpCamera _followUpCam;
        [SerializeField] private InputController _input;

        [Header("Listening")]
        [SerializeField] private VoidEventChannelSO _onReset;

        private void OnEnable()
        {
            _onReset.OnEventRaised += OnReset;

            _input.OnJump += OnClickJump;
        }

        private void OnDisable()
        {
            _onReset.OnEventRaised -= OnReset;

            _input.OnJump -= OnClickJump;
        }

        private void OnClickJump()
        {
            if (_followUpCam.IsFollowing)
                return;

            _followUpCam.SetEnable(true);
        }

        private void OnReset()
        {
            _followUpCam.ResetCamera();
        }
    }
}
