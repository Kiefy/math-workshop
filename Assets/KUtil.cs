using UnityEngine;

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
    /// <returns>Result</returns>
    public static float Distance(float a, float b) { return Mathf.Abs(a - b); }

    //////////////
    // VECTOR 2 //
    //////////////

    /// <summary>
    /// Get distance between a and b
    /// </summary>
    /// <param name="a">Start</param>
    /// <param name="b">End</param>
    /// <returns>Result</returns>
    public static float Distance(Vector2 a, Vector2 b)
    {
        Vector2 p = b - a;
        return Mathf.Sqrt(p.x * p.x + p.y * p.y);
        // return p.magnitude;
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

    //////////////
    // VECTOR 3 //
    //////////////

    /// <summary>
    /// Get distance between a and b
    /// </summary>
    /// <param name="a">Start</param>
    /// <param name="b">End</param>
    /// <returns>Result</returns>
    public static float Distance(Vector3 a, Vector3 b)
    {
        Vector3 p = b - a;
        return Mathf.Sqrt(p.x * p.x + p.y * p.y + p.z * p.z);
        // return p.magnitude;
    }
}