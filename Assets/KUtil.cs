using UnityEngine;

public static class KUtil
{
    public static float Distance(float a, float b) { return Mathf.Abs(a - b); }
    public static Vector2 Middle(Vector2 a, Vector2 b) { return (a + (b - a) / 2); }
}