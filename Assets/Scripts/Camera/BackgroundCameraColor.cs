using UnityEngine;

namespace AlwaysUp.Gameplay
{
    public class BackgroundCameraColor : MonoBehaviour
    {
        private Camera _camera;
        private Color _defaultColor;

        private void Awake()
        {
            _camera = Camera.main;
            _defaultColor = _camera.backgroundColor;
        }

        private void OnBackgroundFill()
        {
            _camera.backgroundColor = GameColors.GetColorByIndex(GameColorController.CurrentColorIndex);
        }

        public void ResetBackgroundColor()
        {
            _camera.backgroundColor = _defaultColor;
        }
    }
}
