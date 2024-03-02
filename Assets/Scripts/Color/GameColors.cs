using UnityEngine;

namespace AlwaysUp.Gameplay
{
    public static class GameColors
    {
        public static int Count { get; private set; }

        private readonly static Color[] _colors;

        static GameColors()
        {
            Color green = new Color(85f / 255, 171f / 255f, 60f / 255, 1);
            Color red = new Color(255f / 255, 0, 0, 1f);
            Color orange = new Color(226f / 255, 101f / 255, 33f / 255, 1f);
            Color pink = new Color(255f / 255, 1f / 255, 197f / 255, 1f);
            Color blue = new Color(0, 180f / 255, 180f / 255, 1f);

            _colors = new Color[] { green, red, orange, pink, blue };
            Count = _colors.Length;
        }

        public static Color GetColorByIndex(int index) => _colors[index];
    }
}
