using UnityEngine;

namespace AlwaysUp.Utils
{
    public static class Vector2Extensions
    {
        public static Vector2 With(this Vector2 vector, float? x = null, float? y = null)
        {
            vector.x = x ?? vector.x;
            vector.y = y ?? vector.y;

            return vector;
        }
    }
}
