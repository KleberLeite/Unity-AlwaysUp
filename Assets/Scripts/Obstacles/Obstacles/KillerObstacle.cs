using AlwaysUp.Events;
using UnityEngine;

namespace AlwaysUp.Gameplay
{
    public class KillerObstacle : Obstacle
    {
        [Header("Broadcasting")]
        [SerializeField] private VoidEventChannelSO _onCollision;

        public override void Init(int colorIndex)
        {
            base.Init(colorIndex);

            Color color = GameColors.GetColorByIndex(colorIndex);
            foreach (var sr in GetComponentsInChildren<SpriteRenderer>())
                sr.color = color;
        }

        override protected void OnCollision(GameObject gameObject)
        {
            if (GameColorController.CurrentColorIndex != _colorIndex)
                _onCollision.RaiseEvent();
        }
    }
}
