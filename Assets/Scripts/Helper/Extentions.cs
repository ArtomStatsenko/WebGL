﻿using UnityEngine;

public static class Extentions
{
    public static Vector3 Change(this Vector3 vector, float? x = null, float? y = null, float? z = null)
    {
        return new Vector3(x == null ? vector.x : (float)x,
                           y == null ? vector.y : (float)y,
                           z == null ? vector.z : (float)z);
    }
}