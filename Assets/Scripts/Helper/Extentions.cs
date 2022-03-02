using UnityEngine;

public static class Extentions
{
    public static Vector3 Change(this Vector3 vector, float? x = null, float? y = null, float? z = null)
    {
        return new Vector3(x == null ? vector.x : (float)x,
                           y == null ? vector.y : (float)y,
                           z == null ? vector.z : (float)z);
    }

    public static Vector2 Change(this Vector2 vector, float? x = null, float? y = null)
    {
        return new Vector2(x == null ? vector.x : (float)x,
                           y == null ? vector.y : (float)y);
    }
}