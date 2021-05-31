using UnityEngine;

// ╭────────────────────╮
// │ INT                │
// ┝━━━━━━━━━━━━━━━━━━━━┥
// │ value.AtLeast(min) │
// │ value.AtMost(max)  │
// ╰────────────────────╯
// ╭────────────────────╮
// │ FLOAT            X │
// ┝━━━━━━━━━━━━━━━━━━━━┥
// │ value.AtLeast(min) │
// │ value.AtMost(max)  │
// │ value.Pow(power)   │
// │ value.Round(step)  │
// │ value.Sqrt()       │
// │ a.Length(b)        │
// ╰────────────────────╯
// ╭────────────────────╮
// │ VECTOR2         XY │
// ┝━━━━━━━━━━━━━━━━━━━━┥
// │ a.Middle(b)        │
// │ a.Direction(b)     │
// │ a.Distance(b)      │
// ╰────────────────────╯
// ╭────────────────────╮
// │ VECTOR3        XYZ │
// ┝━━━━━━━━━━━━━━━━━━━━┥
// │ v3.Round()         │
// │ v3.Round(steps)    │
// ╰────────────────────╯

public static class ExtensionMethods
{
    /////////
    // Log //
    /////////

    /// <summary>
    /// Debug log - ie. 42.Log();
    /// </summary>
    /// <param name="i">int value</param>
    public static void Log(this int i) { Debug.Log(i); }

    /// <summary>
    /// Debug log - ie. 3.14f.Log();
    /// </summary>
    /// <param name="f">float value</param>
    public static void Log(this float f) { Debug.Log(f); }

    /// <summary>
    /// Debug log - ie. "Hello World".Log();
    /// </summary>
    /// <param name="s">string value</param>
    public static void Log(this string s) { Debug.Log(s); }


    /////////
    // Int //
    /////////
    // value.AtLeast(min)
    // value.AtMost(max)
    //

    /// <summary>
    /// Clamp a number to a minimum
    /// </summary>
    /// <param name="v">Input value</param>
    /// <param name="m">Minimum limit</param>
    /// <returns>Clamped value</returns>
    public static int AtLeast(this int v, int m) { return Mathf.Max(v, m); }

    /// <summary>
    /// Clamp a number to a maximum
    /// </summary>
    /// <param name="v">Input value</param>
    /// <param name="m">Maximum limit</param>
    /// <returns>Clamped value</returns>
    public static int AtMost(this int v, int m) { return Mathf.Min(v, m); }


    ///////////
    // FLOAT //
    ///////////
    // value.AtLeast(min)
    // value.AtMost(max)
    // value.Pow(power)
    // value.Round(step)
    // value.Sqrt()
    ///////////
    // a.Length(b)
    //

    /// <summary>
    /// Clamp a number to a minimum
    /// </summary>
    /// <param name="v">Input value</param>
    /// <param name="m">Minimum limit</param>
    /// <returns>Clamped value</returns>
    public static float AtLeast(this float v, float m) { return Mathf.Max(v, m); }

    /// <summary>
    /// Clamp a number to a maximum
    /// </summary>
    /// <param name="v">Input value</param>
    /// <param name="m">Maximum limit</param>
    /// <returns>Clamped value</returns>
    public static float AtMost(this float v, float m) { return Mathf.Min(v, m); }

    /// <summary>
    /// Returns f raised to power p.
    /// </summary>
    /// <param name="f">Input value</param>
    /// <param name="p">Power</param>
    /// <returns>Result</returns>
    public static float Pow(this float f, float p) { return Mathf.Pow(f, p); }

    /// <summary>
    /// Round v to steps of s
    /// </summary>
    /// <param name="v">Input value</param>
    /// <param name="s">Step size</param>
    /// <returns>Rounded value</returns>
    public static float Round(this float v, float s) { return Mathf.Round(v / s) * s; }

    /// <summary>
    /// Returns the square root of f.
    /// </summary>
    /// <param name="f">Input value</param>
    /// <returns>Squared value</returns>
    public static float Sqrt(this float f) { return Mathf.Sqrt(f); }

    /// <summary>
    /// Length - ie. a.Distance(b)
    /// </summary>
    /// <param name="a">start point</param>
    /// <param name="b">end point</param>
    /// <returns>float</returns>
    public static float Length(this float a, float b) { return Mathf.Abs(a - b); }


    //////////////
    // VECTOR 2 //
    //////////////
    // a.Middle(b)
    // a.Direction(b)
    // a.Distance(b)
    //

    /// <summary>
    /// Get midpoint between a and b
    /// </summary>
    /// <param name="a">Start</param>
    /// <param name="b">End</param>
    /// <returns>Midpoint</returns>
    public static Vector2 Middle(this Vector2 a, Vector2 b)
    {
        return a + (b - a) / 2;
    }

    /// <summary>
    /// Get direction from a to b
    /// </summary>
    /// <param name="a">Base</param>
    /// <param name="b">Target</param>
    /// <returns>Unit vector</returns>
    public static Vector2 Direction(this Vector2 a, Vector2 b)
    {
        return (b - a).normalized;
    }

    /// <summary>
    /// Get distance between vectors a and b.
    /// </summary>
    /// <param name="a"></param>
    /// <param name="b"></param>
    /// <returns>distance</returns>
    public static float Distance(this Vector2 a, Vector2 b)
    {
        Vector2 output = new Vector2(a.x - b.x, a.y - b.y);

        float ax = (a.x * a.x + a.y * a.y).Sqrt();
        float bx = (b.x * b.x + b.y * b.y).Sqrt();

        return Vector2.Distance(a, b);
    }


    //////////////
    // VECTOR 3 //
    //////////////
    // v3.Round()
    // v3.Round(step)

    /// <summary>
    /// Round Vector3 (Remove decimals)
    /// </summary>
    /// <param name="v">Input value</param>
    /// <returns>Rounded value</returns>
    private static Vector3 Round(this Vector3 v)
    {
        return new Vector3(
            Mathf.Round(v.x),
            Mathf.Round(v.y),
            Mathf.Round(v.z)
        );
    }

    /// <summary>
    /// Round v to increments of s
    /// </summary>
    /// <param name="v">Input value</param>
    /// <param name="s">Step size</param>
    /// <returns>Rounded value</returns>
    public static Vector3 Round(this Vector3 v, float s) { return (v / s).Round() * s; }
}