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
        [SerializeField] private VoidEventChannelSO _onReset;

        public static int CurrentColorIndex { get; private set; }

        private void OnEnable()
        {
            _onHitColor.OnEventRaised += OnHitColor;
            _onReset.OnEventRaised += OnReset;
        }

        private void OnDisable()
        {
            _onHitColor.OnEventRaised -= OnHitColor;
            _onReset.OnEventRaised -= OnReset;
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

        private void OnReset()
        {
            CurrentColorIndex = -1;
        }
    }
}
