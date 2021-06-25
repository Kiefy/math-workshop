using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;

#endif
public class Vectors : MonoBehaviour
{
    [Range(0f, 0.02f)] public float speed;
    private const int MARKER_COUNT = 20;

    public Transform targetTransform;
    private float offsetP;
    private float offsetT;
    private bool jump;

    private struct TravelData
    {
        public float[] lengths;
        public float fullLength;
        public float routePos;
    }

    private TravelData vectorRoute;
    private TravelData laserRoute;

    private readonly Color red = new Color(1f, 0.26f, 0.26f);
    private readonly Color green = new Color(0.36f, 1f, 0.32f);
    private readonly Color blue = new Color(0.22f, 0.6f, 1f);
    private readonly Color darkRed = new Color(0.5f, 0.14f, 0.14f);
    private readonly Color darkGreen = new Color(0.18f, 0.5f, 0.16f);
    private readonly Color darkBlue = new Color(0.13f, 0.29f, 0.5f);
    private readonly Color darkYellow = new Color(0.5f, 0.45f, 0.01f);
    private readonly Color darkCyan = new Color(0f, 0.5f, 0.5f);

    private void Start()
    {
        FloatExamples();
        Vector2Functions();
    }

    private void OnDrawGizmos()
    {
        Vector2 origin = Vector2.zero;
        Vector2 player = transform.position;
        Vector2 target = targetTransform.position;

        OriginGui(origin);
        OriginToPlayerGui(origin, player);
        PlayerToTargetGui(player, target);

        List<Vector3> vectorPoints = new List<Vector3> {origin, player, target};
        vectorRoute = TravelingSphere(vectorPoints, vectorRoute, red, green);

        if (CrossProduct.points.Count > 1)
        {
            List<Vector3> laserPoints = new List<Vector3>();
            foreach (Vector3 t in CrossProduct.points) laserPoints.Add(t);
            laserRoute = TravelingSphere(laserPoints, laserRoute, Color.cyan, Color.yellow);
        }
    }

    // ╭───────╮
    // │ Float │
    // ╰───────╯
    private static void FloatExamples()
    {
        "╭────────┰─────────────────────────────────────╮".Log();
        "│  ┐ ┬╮  ┃  ╭╮ ╭╮ ╭╮ ╷  ╭╮ ┌╮   ╭╴ ╷  ╭╮ ╭╮╶┬╴ │".Log();
        "│  │ ││  ┃  ╰╮ │  ├┤ │  ├┤ ├┤   ├╴ │  ││ ├┤ │  │".Log();
        "│  ┴ ┴╯  ┃  ╰╯ ╰╯ ╵╵ ╰─ ╵╵ ╵╰   ╵  ╰─ ╰╯ ╵╵ ╵  │".Log();
        "╰────────┸─────────────────────────────────────╯".Log();

        "╭─────┰──────────────────────────────╮".Log();
        "│ + - ┃ Offset. Addition/Subtraction │".Log();
        "╰─────┸──────────────────────────────╯".Log();
        (" 2 +  1 =  " /* 3 */ + (2 + 1)).Log();
        (" 1 + -1 =  " /* 0 */ + (1 + -1)).Log();
        ("-1 +  2 =  " /* 1 */ + (-1 + 2)).Log();
        ("-2 + -1 = " /* -3 */ + (-2 + -1)).Log();
        "╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌".Log();
        (" 2 -  1 =  " /* 1 */ + (2 - 1)).Log();
        (" 1 - -1 =  " /* 2 */ + (1 - -1)).Log();
        ("-1 -  2 = " /* -3 */ + (-1 - 2)).Log();
        ("-2 - -1 = " /* -1 */ + (-2 - -1)).Log();
        "╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌".Log();
        "a - b  ==  a + (-b)".Log();

        "╭─────┰────────────────────────╮".Log();
        "│ * / ┃ Scale. Multiply/Divide │".Log();
        "╰─────┸────────────────────────╯".Log();
        (" 10 *  2   =  " /* 20 */ + 10 * 2).Log();
        (" 10 *  0.5 =   " /* 5 */ + 10 * 0.5f).Log();
        (" -5 *  4   = " /* -20 */ + -5 * 4).Log();
        ("-10 * -2   =  " /* 20 */ + -10 * -2).Log();
        "╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌".Log();
        (" 10 /  2   =   " /* 5 */ + 10 / 2).Log();
        (" 10 /  0.5 =  " /* 20 */ + 10 / 0.5f).Log();
        (" -5 /  4   =  " /* -1.25 */ + -5f / 4).Log();
        ("-10 / -2   =   " /* 5 */ + -10 / -2).Log();
        "╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌".Log();
        "a / b  ==  a * (1 / b)".Log();

        "╭─────────┰───────────────────────────╮".Log();
        "│ Sign(x) ┃ Direction. either -1 or 1 │".Log();
        "╰─────────┸───────────────────────────╯".Log();
        ("Mathf.Sign(-10) = " /* -1 */ + Mathf.Sign(-10)).Log();
        ("Mathf.Sign(  0) =  " /* 1 */ + Mathf.Sign(0)).Log();
        ("Mathf.Sign( 10) =  " /* 1 */ + Mathf.Sign(10)).Log();

        "╭────────┰────────────────────────────────────────────────╮".Log();
        "│ Abs(x) ┃ Length/Magnitude. Convert negative to positive │".Log();
        "╰────────┸────────────────────────────────────────────────╯".Log();
        ("Mathf.Abs(-10) = " /* 10 */ + Mathf.Abs(-10)).Log();
        ("Mathf.Abs(  0) =  " /* 0 */ + Mathf.Abs(0)).Log();
        ("Mathf.Abs( 10) = " /* 10 */ + Mathf.Abs(10)).Log();

        "╭────────────┰──────────╮".Log();
        "│ Abs(a - b) ┃ Distance │".Log();
        "╰────────────┸──────────╯".Log();
        ("Mathf.Abs( 1 -  3) = " /* 2 */ + Mathf.Abs(1 - 3)).Log();
        ("Mathf.Abs( 3 -  1) = " /* 2 */ + Mathf.Abs(3 - 1)).Log();
        ("Mathf.Abs(-3 - -1) = " /* 2 */ + Mathf.Abs(-3 - -1)).Log();
        ("Mathf.Abs(-1 -  1) = " /* 2 */ + Mathf.Abs(-1 - 1)).Log();

        "╭──────────┰─────────────────────────────────────────╮".Log();
        "│ Round(f) ┃ Convert decimal to nearest whole number │".Log();
        "╰──────────┸─────────────────────────────────────────╯".Log();
        ("Mathf.Round( 1.499) =  " /* 1 */ + Mathf.Round(1.499f)).Log();
        ("Mathf.Round( 1.5  ) =  " /* 2 */ + Mathf.Round(1.5f)).Log();
        ("Mathf.Round(-1.499) = " /* -1 */ + Mathf.Round(-1.499f)).Log();
        ("Mathf.Round(-1.5  ) = " /* -2 */ + Mathf.Round(-1.5f)).Log();

        "╭────────────────────┰───────────────────────────────╮".Log();
        "│ Clamp(v, min, max) ┃ Limit v between a min and max │".Log();
        "╰────────────────────┸───────────────────────────────╯".Log();
        ("Mathf.Clamp(-0.5, -1, 1) = " /* -0.5 */ + Mathf.Clamp(-0.5f, -1, 1)).Log();
        ("Mathf.Clamp(-0.5,  0, 1) =  " /* 0 */ + Mathf.Clamp(-0.5f, 0, 1)).Log();
        ("Mathf.Clamp( 0,    0, 1) =  " /* 0 */ + Mathf.Clamp(0f, 0, 1)).Log();
        ("Mathf.Clamp( 0.5,  0, 1) =  " /* 0.5 */ + Mathf.Clamp(0.5f, 0, 1)).Log();
        ("Mathf.Clamp( 1,    0, 1) =  " /* 1 */ + Mathf.Clamp(1f, 0, 1)).Log();
        ("Mathf.Clamp( 1.5,  0, 1) =  " /* 1 */ + Mathf.Clamp(1.5f, 0, 1)).Log();
        ("Mathf.Clamp( 1.5,  0, 2) =  " /* 1.5 */ + Mathf.Clamp(1.5f, 0, 2)).Log();
    }

    // ╭─────────╮
    // │ Vector2 │
    // ╰─────────╯
    private static void Vector2Functions()
    {
        Vector2 a = new Vector2(-2f, 3f);
        Vector2 b = new Vector2(1f, 2f);

        "╭─────────┰───────────────────────╮".Log();
        "│  ╭╮ ┬╮  ┃  ╷╷ ╭╴ ╭╮╶┬╴╭╮ ┌╮ ╭╮  │".Log();
        "│  ╭╯ ││  ┃  ││ ├╴ │  │ ││ ├┤ ╭╯  │".Log();
        "│  └╴ ┴╯  ┃  ╰┘ ╰─ ╰╯ ╵ ╰╯ ╵╰ └╴  │".Log();
        "╰─────────┸───────────────────────╯".Log();

        "╭─────────────┰───────────────────────────────────╮".Log();
        "│ a.magnitude ┃ Get the length of a (from origin) │".Log();
        "╰─────────────┸───────────────────────────────────╯".Log();
        "a: -2, 3".Log();
        "╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌".Log();
        ("     a.x * a.x              = " /* 4 */ + a.x * a.x).Log();
        ("     a.y * a.y              = " /* 9 */ + a.y * a.y).Log();
        ("     4 + 9                  = " /* 13 */ + (4 + 9)).Log();
        ("Sqrt(13)                    = " /* 3.6.. */ + 13f.Sqrt()).Log();
        ("Sqrt(a.x * a.x + a.y * a.y) = " /* 3.6.. */ + (a.x * a.x + a.y * a.y).Sqrt()).Log();
        ("a.magnitude                 = " + (a.magnitude)).Log();

        "╭────────────────────────┰──────────────────────────╮".Log();
        "│ Vector2.Distance(a, b) ┃ Get distance from a to b │".Log();
        "╰────────────────────────┸──────────────────────────╯".Log();
        "a: -2, 3".Log();
        "b:  1, 2".Log();
        "╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌".Log();
        ("(b - a).magnitude      = " /* 3.1.. */ + (a - b).magnitude).Log();
        ("Vector2.Distance(a, b) = " /* 3.1.. */ + Vector2.Distance(a, b)).Log();
        "╭────────────────────┰──────────────────────────────╮".Log();
        "│ KUtil.Middle(a, b) ┃ Get midpoint between a and b │".Log();
        "╰────────────────────┸──────────────────────────────╯".Log();
        "a: -2, 3".Log();
        "b:  1, 2".Log();
        "╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌".Log();
        "b Position (1,2) - a Position (-2,3) = b Direction Vector (3,-1)".Log();
        "╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌".Log();
        "a Position (-2,3) + b Direction Vector (3,-1) = b Position (1,2)".Log();
        "╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌".Log();
        (b - a).Log();
        (b - a + a).Log();
        (a + b).Log();
        (a + b / 2).Log();
        ("a + (b - a) / 2    = " /* (-0.5, 2.5) */ + (a + (b - a) / 2)).Log();
        ("KUtil.Middle(a, b) = " /* (-0.5, 2.5) */ + Kief.Middle(a, b)).Log();
        "╭───────────────┰──────────────────────────────╮".Log();
        "│ Lerp(a, b, t) ┃ Blend from a to b based on t │".Log();
        "╰───────────────┸──────────────────────────────╯".Log();
    }

    // ╭────────╮
    // │ Origin │
    // ╰────────╯
    private void OriginGui(Vector2 origin)
    {
#if UNITY_EDITOR
        // Draw axis lines
        Vector3 left = Vector3.left * 100f;
        Vector3 right = Vector3.right * 100f;
        Handles.DrawBezier(left, right, left, right, darkRed, null, 2f);
        Vector3 up = Vector3.up * 100f;
        Vector3 down = Vector3.down * 100f;
        Handles.DrawBezier(up, down, up, down, darkGreen, null, 2f);
        Vector3 forward = Vector3.forward * 100f;
        Vector3 back = Vector3.back * 100f;
        Handles.DrawBezier(forward, back, forward, back, darkBlue, null, 2f);

        // Draw Origin unit circle
        Handles.color = new Color(1f, 1f, 1f, 0.2f);
        Handles.DrawWireDisc(origin, Vector3.forward, 1f, 2f);

        // Draw 2D Axis Line markers
        for (int i = 1; i < MARKER_COUNT + 1; i++)
        {
            if (i % 10 == 0)
            {
                Kief.Line(i * 0.1f, 0f, i * 0.1f, 0.1f, red, 4f);
                Kief.Line(-i * 0.1f, 0f, -i * 0.1f, -0.1f, red, 4f);
                Kief.Line(0f, i * 0.1f, -0.1f, i * 0.1f, green, 4f);
                Kief.Line(0f, i * -0.1f, 0.1f, i * -0.1f, green, 4f);
            }
            else
            {
                Kief.Line(i * 0.1f, 0f, i * 0.1f, 0.05f, red, 2f);
                Kief.Line(-i * 0.1f, 0f, -i * 0.1f, -0.05f, red, 2f);
                Kief.Line(0f, i * 0.1f, -0.05f, i * 0.1f, green, 2f);
                Kief.Line(0f, i * -0.1f, 0.05f, i * -0.1f, green, 2f);
            }
        }

        // Draw 2D Axis Line numbers
        GUIStyle styleX = new GUIStyle {normal = {textColor = red}};
        for (int i = 1; i <= MARKER_COUNT / (MARKER_COUNT / 2); i++)
            Handles.Label(new Vector3(i, 0.3f, 0f), i.ToString("F0"), styleX);
        for (int i = -(MARKER_COUNT / (MARKER_COUNT / 2)); i < 0; i++)
            Handles.Label(new Vector3(i, -0.1f, 0f), i.ToString("F0"), styleX);
        GUIStyle styleY = new GUIStyle {normal = {textColor = green}};
        for (int i = 1; i <= MARKER_COUNT / (MARKER_COUNT / 2); i++)
            Handles.Label(new Vector3(-0.2f, i + 0.05f, 0f), i.ToString("F0"), styleY);
        for (int i = -(MARKER_COUNT / (MARKER_COUNT / 2)); i < 0; i++)
            Handles.Label(new Vector3(0.2f, i + 0.05f, 0f), i.ToString("F0"), styleY);
#endif
    }

    // ╭──────────────────╮
    // │ Origin to Player │
    // ╰──────────────────╯
    private void OriginToPlayerGui(Vector2 origin, Vector2 player)
    {
#if UNITY_EDITOR
        GUIStyle styleX = new GUIStyle {normal = {textColor = new Color(1f, 0.26f, 0.26f)}};
        GUIStyle styleY = new GUIStyle {normal = {textColor = new Color(0.36f, 1f, 0.32f)}};
        Vector2 opUnitVector = player.normalized;

        // Draw lines from origin to player. meeting at unit boundary
        Handles.DrawBezier(origin, opUnitVector, origin, opUnitVector, Color.white, null, 4f);
        Handles.DrawBezier(opUnitVector, player, opUnitVector, player, Color.gray, null, 2f);

        // Draw angle in degrees label at unit boundary
        float playerAngle = Vector2.Angle(opUnitVector, Vector2.up);
        GUIStyle angleLabel = new GUIStyle {normal = {textColor = Color.white}};
        Handles.Label(opUnitVector / 2, playerAngle.ToString("F1") + "°", angleLabel);

        // Draw Perpendicular lines from zero xy to player
        Vector2 playerX = new Vector2(player.x, 0);
        Handles.DrawBezier(player, playerX, player, playerX, darkRed, null, 2f);
        Vector2 playerY = new Vector2(0, player.y);
        Handles.DrawBezier(player, playerY, player, playerY, darkGreen, null, 2f);

        // Draw Perpendicularity numbers
        Handles.Label(new Vector2(0.1f + player.x, 0), player.x.ToString("F2"), styleX);
        Handles.Label(new Vector2(0.1f, player.y), player.y.ToString("F2"), styleY);

        // Draw length label, midway between o and p
        Vector2 originPlayerMiddle = Kief.Middle(origin, player);
        float originPlayerDistance = Vector2.Distance(origin, player);
        GUIStyle lengthLabel = new GUIStyle {normal = {textColor = Color.gray}};
        Handles.Label(originPlayerMiddle, originPlayerDistance.ToString("F2"), lengthLabel);

        // Draw p coords label
        Vector2 playerOffsetX = new Vector2(player.x + 0.1f, player.y - 0.1f);
        Handles.Label(playerOffsetX, "X: " + player.x.ToString("F2"), styleX);
        Vector2 playerOffsetY = new Vector2(player.x + 0.1f, player.y - 0.25f);
        Handles.Label(playerOffsetY, "Y: " + player.y.ToString("F2"), styleY);
#endif
    }

    // ╭──────────────────╮
    // │ Player to Target │
    // ╰──────────────────╯
    private void PlayerToTargetGui(Vector2 player, Vector2 target)
    {
#if UNITY_EDITOR
        Vector2 ptUnitVector = Kief.Direction(player, target);

        // Draw Unit circle
        Handles.color = darkYellow;
        Handles.DrawWireDisc(player, Vector3.forward, 1f, 2f);

        // Draw lines from player to target. separated at unit vector
        Vector2 ptUnitVectorPos = player + ptUnitVector;
        Handles.DrawBezier(player, ptUnitVectorPos, player, ptUnitVectorPos, Color.white, null, 4f);
        Handles.DrawBezier(ptUnitVectorPos, target, ptUnitVectorPos, target, Color.gray, null, 2f);

        // Draw player to target angle label
        Vector2 place = player + ptUnitVector / 2;
        float ptAngle = Vector2.SignedAngle(ptUnitVector, player.normalized);
        GUIStyle angleLabel = new GUIStyle {normal = {textColor = Color.white}};
        Handles.Label(place, "Ang: " + ptAngle.ToString("F1") + "°", angleLabel);
        Vector2 start = Kief.Direction(Vector2.zero, player);
        Vector2 ptDir = Kief.Direction(player, target);
        GUIStyle dotLabel = new GUIStyle {normal = {textColor = Color.yellow}};
        Handles.Label(place + Vector2.down / 7, "Dot: " + Kief.Dot(start, ptDir).ToString("F2"), dotLabel);


        // Draw Target length label, midway between p and t
        Vector2 playerTargetMiddlePosition = Kief.Middle(player, target);
        float playerTargetDistance = Vector2.Distance(player, target);
        GUIStyle lengthLabel = new GUIStyle {normal = {textColor = Color.gray}};
        Handles.Label(playerTargetMiddlePosition, playerTargetDistance.ToString("F2"), lengthLabel);

        // Draw Target coords label
        Vector2 targetOffsetX = new Vector2(target.x + 0.1f, target.y - 0.1f);
        GUIStyle styleX = new GUIStyle {normal = {textColor = new Color(1f, 0.26f, 0.26f)}};
        Handles.Label(targetOffsetX, "X: " + target.x.ToString("F2"), styleX);
        Vector2 targetOffsetY = new Vector2(target.x + 0.1f, target.y - 0.25f);
        GUIStyle styleY = new GUIStyle {normal = {textColor = new Color(0.36f, 1f, 0.32f)}};
        Handles.Label(targetOffsetY, "Y: " + target.y.ToString("F2"), styleY);
#endif
    }

    // ╭──────────────────╮
    // │ Traveling sphere │
    // ╰──────────────────╯
    private TravelData TravelingSphere(IReadOnlyList<Vector3> points,
        TravelData tData,
        Color startColor,
        Color endColor)
    {
#if UNITY_EDITOR
        void RebuildRoutes()
        {
            tData.fullLength = 0;
            tData.lengths = new float[points.Count];
            for (int i = 0; i < points.Count - 1; i++)
            {
                tData.lengths[i] = Kief.Distance(points[i], points[i + 1]);
                tData.fullLength += tData.lengths[i];
            }
        }

        if (tData.lengths == null || tData.lengths.Length == 0) RebuildRoutes();
        else
        {
            for (int i = 0; i < points.Count - 1; i++)
            {
                float stepDistance = Kief.Distance(points[i], points[i + 1]);
                //("points.Count: " + points.Count + " i: " + i + " tData.lengths.Length: " +
                // tData.lengths.Length).Log();
                if (i >= tData.lengths.Length) continue;
                if (tData.lengths[i] == stepDistance) continue;
                //tData.lengths[i].Log();
                tData.fullLength += stepDistance - tData.lengths[i];
                tData.lengths[i] = stepDistance;
            }
        }

        float length = 0;
        for (int i = 0; i < tData.lengths.Length - 1; i++)
        {
            if (tData.routePos > tData.fullLength) tData.routePos = 0;
            length += tData.lengths[i];
            if (!(length > tData.routePos)) continue;
            Handles.color = Color.Lerp(startColor, endColor, tData.routePos / tData.fullLength);
            if (i >= points.Count - 1) continue;
            Vector3 dir = Kief.Direction(points[i], points[i + 1]);
            float remap = Kief.Remap(
                length - tData.lengths[i],
                length,
                0f,
                tData.lengths[i],
                tData.routePos
            );
            Handles.DrawSolidDisc(points[i] + dir * remap, Vector3.forward, 0.1f);
            tData.routePos += speed;
            break;
        }
#endif
        return tData;
    }
}