using UnityEngine;

namespace AlwaysUp.Utils
{
    public static class Vector3Extensions
    {
        public static Vector3 With(this Vector3 vector, float? x = null, float? y = null, float? z = null)
        {
            vector.x = x ?? vector.x;
            vector.y = y ?? vector.y;
            vector.z = z ?? vector.z;

            return vector;
        }
    }
}
