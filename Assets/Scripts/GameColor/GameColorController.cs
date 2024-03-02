using AlwaysUp.Events;
using UnityEngine;

namespace AlwaysUp.Gameplay
{
    public class GameColorController : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField] private SpriteRenderer _backgrounFillerSr;
        [SerializeField] private Animation _backgroundFillAnimation;

        [Header("Listening")]
        [SerializeField] private IntEventChannelSO _onHitColor;

        public static int CurrentColorIndex { get; private set; }

        private void OnEnable()
        {
            _onHitColor.OnEventRaised += OnHitColor;
        }

        private void OnDisable()
        {
            _onHitColor.OnEventRaised -= OnHitColor;
        }

        private void Awake()
        {
            CurrentColorIndex = -1;
        }

        private void OnHitColor(int colorIndex)
        {
            CurrentColorIndex = colorIndex;

            _backgrounFillerSr.color = GameColors.GetColorByIndex(colorIndex);
            _backgroundFillAnimation.Play();
        }
    }
}
