using System;
using UnityEngine;

// ╭────────────────────╮
// │ INT                │
// ┝━━━━━━━━━━━━━━━━━━━━┥
// │ None yet.          │
// ╰────────────────────╯
// ╭────────────────────╮
// │ FLOAT            X │
// ┝━━━━━━━━━━━━━━━━━━━━┥
// │ x = Distance(a, b) │ Useless?
// ╰────────────────────╯
// ╭──────────────────────╮
// │ VECTOR2           XY │
// ┝━━━━━━━━━━━━━━━━━━━━━━┥
// │ xy = Direction(a, b) │
// │ x  = Distance(a, b)  │ Dupe: Vector2.Distance(a, b)
// │ xy = Middle(a, b)    │
// ╰──────────────────────╯
// ╭──────────────────────╮
// │ VECTOR3          XYZ │
// ┝━━━━━━━━━━━━━━━━━━━━━━┥
// │ xyz = Distance(a, b) │ Dupe: Vector3.Distance(a, b)
// ╰──────────────────────╯

public static class KUtil
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
        return Mathf.Abs(a - b);
    }

    //////////////
    // VECTOR 2 //
    //////////////

    /// <summary>
    /// Get direction from a to b
    /// </summary>
    /// <param name="a">Base</param>
    /// <param name="b">Target</param>
    /// <returns>Unit vector</returns>
    public static Vector2 Direction(Vector2 a, Vector2 b)
    {
        return (b - a).normalized;
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
    /// Get the midpoint between a and b
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
    /// It isn't ideal since we would also need to square everything
    /// that is compared with the distance output.
    /// </remarks>
    /// <param name="a">Start</param>
    /// <param name="b">End</param>
    /// <param name="r">Radius</param>
    /// <param name="isInside"></param>
    /// <returns>Distance</returns>
    [Obsolete("Not really worth using but interesting nonetheless.")]
    public static float DistHack(Vector2 a, Vector2 b, float r, out bool isInside)
    {
        Vector2 disp = b - a;
        float distSq = disp.x * disp.x + disp.y * disp.y; // No Sqrt() here
        float radiusSq = r * r; // Square radius to avoid Sqrting distance
        isInside = distSq < radiusSq;
        return distSq;
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
        return p.magnitude;
    }
}