using System;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;

#endif
// ╭──────────────────────────────────────╮
// │ FLOAT                              X │
// ┝━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━┥
// │ x = Distance(a, b)                   │ Useless?  Dupe: Mathf.Abs(a-b)
// │ v = Lerp(min, max, t)                │
// │ t = InvLerp(min, max, v)             │
// │ t = Remap(iMin, iMax, oMin, oMax, v) │
// ╰──────────────────────────────────────╯
// ╭────────────────────────╮
// │ VECTOR2             XY │
// ┝━━━━━━━━━━━━━━━━━━━━━━━━┥
// │ xy = Direction(a, b)   │
// │ x  = Distance(a, b)    │ Dupe: Vector2.Distance(a, b)
// │ xy = Middle(a, b)      │
// │ b  = DistHack(a, b, r) │
// ╰────────────────────────╯
// ╭──────────────────────╮
// │ VECTOR3          XYZ │
// ┝━━━━━━━━━━━━━━━━━━━━━━┥
// │ xyz = Distance(a, b) │ Dupe: Vector3.Distance(a, b)
// ╰──────────────────────╯

public static class Kief
{
    ///////////
    // FLOAT //
    ///////////

    /// <summary>
    /// Get distance between a and b
    /// </summary>
    /// <param name="a">Start</param>
    /// <param name="b">End</param>
    /// <returns>Distance</returns>
    public static float Distance(float a, float b)
    {
        float aMinusB = a - b; // Get the direction vector from a to b
        //return aMinusB < 0 ? -1 * aMinusB : aMinusB; // How Abs() is made
        return Mathf.Abs(aMinusB); // If negative, flip it, else return as is
    }

    /// <summary>
    /// [L]inearly Int[erp]olate between min and max, based on t:0-1.
    /// </summary>
    /// <param name="min">Min</param>
    /// <param name="max">Max</param>
    /// <param name="t"></param>
    /// <returns>Mix</returns>
    public static float Lerp(float min, float max, float t)
        // Example values:          2          3        0.5
    {
        float tFlipped = 1f - t; // flips t.  ie. (1 = 0), (0 = 1)
        float minMix = min * tFlipped; // Decrease (min->0) as (t) increases.  ie. (2 * 0.5 = 1)
        float maxMix = max * t; // Increase (0->max) as (t) increases.  ie. (3 * 0.5 = 1.5)
        return minMix + maxMix; // Combine adjusted min/max values. (1 + 1.5 = 2.5)
        // return (1f - t) * a + b * t; // Compact version
    }

    /// <summary>
    /// Return a normalised value that corresponds to where v sits between min and max.
    /// </summary>
    /// <param name="min">Min</param>
    /// <param name="max">Max</param>
    /// <param name="v">Value</param>
    /// <returns>Normalised value</returns>
    public static float InvLerp(float min, float max, float v)
        //                            25       75       50
    {
        float valMinusMin = v - min; // 50 - 25 = 25
        float maxMinusMin = max - min; //  75 - 25 = 50
        return valMinusMin / maxMinusMin; // 25 / 50 = 0.5
        //return (v - a) / (b - a); // Compact version
    }

    /// <summary>
    /// Find where v sits within the Input Range and rescale to fit the Output range.
    /// Returning the rescaled value of that conversion.
    /// </summary>
    /// <param name="iMin">Input min</param>
    /// <param name="iMax">Input max</param>
    /// <param name="oMin">Output min</param>
    /// <param name="oMax">Output max</param>
    /// <param name="v">Input value</param>
    /// <returns>Output value</returns>
    public static float Remap(float iMin, float iMax, float oMin, float oMax, float v)
    {
        float t = InvLerp(iMin, iMax, v);
        return Lerp(oMin, oMax, t);
    }


    //////////////
    // VECTOR 2 //
    //////////////

    /// <summary>
    /// Get normalized direction from a to b
    /// </summary>
    /// <param name="a">Base</param>
    /// <param name="b">Target</param>
    /// <returns>Unit vector</returns>
    public static Vector2 Direction(Vector2 a, Vector2 b)
    {
        return (b - a).normalized; // Get direction vector from a to b, then reduce length to 1
    }

    // Dupe: Vector2.Distance(a, b)
    /// <summary>
    /// Get distance between a and b
    /// </summary>
    /// <param name="a">Start</param>
    /// <param name="b">End</param>
    /// <returns>Result</returns>
    public static float Distance(Vector2 a, Vector2 b)
    {
        Vector2 p = b - a;
        // return Mathf.Sqrt(p.x * p.x + p.y * p.y);
        return p.magnitude;
    }

    /// <summary>
    /// Get the midpoint position between a and b.
    /// </summary>
    /// <param name="a">Start</param>
    /// <param name="b">End</param>
    /// <returns>Midpoint position</returns>
    public static Vector2 Middle(Vector2 a, Vector2 b)
    {
        // Get the vector (direction/magnitude) from a to b by subtracting a's position from b
        Vector2 dir = b - a;
        // Half it to get the midpoint
        Vector2 dirHalved = dir / 2;
        // Start with a as reference point, then add the halved midpoint vector
        return a + dirHalved;
    }

    /// <summary>
    /// A hacky way to get distance from a to b without using Sqrt().
    /// Also provides a bool which is true if b is within radius.
    /// </summary>
    /// <remarks>
    /// It isn't ideal since the dispSq isn't the real distance and can only be used as such if
    /// we get the Square Root of it first, which negates any saving.
    ///
    /// But if you only need a radius trigger, then it is fine.
    /// </remarks>
    /// <param name="a">Start</param>
    /// <param name="b">End</param>
    /// <param name="r">Radius</param>
    /// <returns>True when b is within a's radius</returns>
    [Obsolete("Not really worth using except in performance critical situations.")]
    public static bool DistHack(Vector2 a, Vector2 b, float r)
    {
        Vector2 displacement = b - a; // Get the vector from a to b

        //float displacementSquared = displacement.x * displacement.x + displacement.y * displacement.y;
        float displacementSquared = displacement.sqrMagnitude; // Same as above ^
        // Normally we would Sqrt() displacementSquared to get an accurate distance
        return displacementSquared < r * r; // Square [r]adius to avoid Sqrting displacementSquared
    }

    /// <summary>
    /// Compare 2 angles
    /// </summary>
    /// <remarks>
    /// b must be normalised to yield the correct result.<br/>
    /// 1: Identical<br/>
    /// -1: Opposite<br/>
    /// 0: Perpendicular<br/>
    /// </remarks>
    /// <param name="a">Angle A</param>
    /// <param name="b">Angle B</param>
    /// <returns>Dot Product</returns>
    public static float Dot(Vector2 a, Vector2 b)
    {
        return a.x * b.x + a.y * b.y;
    }

    public static void Line(float sX, float sY, float eX, float eY, Color color, float thickness)
    {
#if UNITY_EDITOR
        Handles.DrawBezier(
            new Vector2(sX, sY), new Vector2(eX, eY),
            new Vector2(sX, sY), new Vector2(eX, eY),
            color, null, thickness
        );
#endif
    }

    public static Vector2 LocalToWorld(Transform localOrigin, Vector2 localPoint)
    {
        Vector2 x = localOrigin.right * localPoint.x;
        Vector2 y = localOrigin.up * localPoint.y;
        return (Vector2)localOrigin.position + x + y;
    }

    //////////////
    // VECTOR 3 //
    //////////////

    // Dupe: Vector3.Distance(a, b)
    /// <summary>
    /// Get distance between a and b
    /// </summary>
    /// <param name="a">Start</param>
    /// <param name="b">End</param>
    /// <returns>Result</returns>
    public static float Distance(Vector3 a, Vector3 b)
    {
        Vector3 p = b - a;

        // return Mathf.Sqrt(p.x * p.x + p.y * p.y + p.z * p.z);
        return p.magnitude; // Same as above ^
    }
}