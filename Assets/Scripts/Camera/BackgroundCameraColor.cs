using UnityEngine;

namespace AlwaysUp.Gameplay
{
    public class BackgroundCameraColor : MonoBehaviour
    {
        private Camera _camera;

        private void Awake()
        {
            _camera = Camera.main;
        }

        private void OnBackgroundFill()
        {
            _camera.backgroundColor = GameColors.GetColorByIndex(GameColorController.CurrentColorIndex);
        }
    }
}
