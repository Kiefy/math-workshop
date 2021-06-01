using UnityEditor;
using UnityEngine;

public class Vectors : MonoBehaviour
{
    public bool loop;
    [Range(0f, 0.02f)] public float speed;

    public Transform targetTransform;
    private float offsetP;
    private float offsetT;
    private bool jump;

    private readonly Color redColor = new Color(1f, 0.26f, 0.26f);
    private readonly Color greenColor = new Color(0.36f, 1f, 0.32f);
    private readonly Color blueColor = new Color(0.22f, 0.6f, 1f);
    private readonly Color darkRedColor = new Color(0.5f, 0.14f, 0.14f);
    private readonly Color darkGreenColor = new Color(0.18f, 0.5f, 0.16f);
    private readonly Color darkBlueColor = new Color(0.13f, 0.29f, 0.5f);
    private readonly Color darkYellowColor = new Color(0.5f, 0.45f, 0.01f);

    private void OnDrawGizmos()
    {
        // Color styles
        GUIStyle redStyle = new GUIStyle {normal = {textColor = new Color(1f, 0.26f, 0.26f)}};
        GUIStyle greenStyle = new GUIStyle {normal = {textColor = new Color(0.36f, 1f, 0.32f)}};
        GUIStyle blueStyle = new GUIStyle {normal = {textColor = new Color(0.22f, 0.6f, 1f)}};
        GUIStyle yellowStyle = new GUIStyle {normal = {textColor = Color.yellow}};
        GUIStyle cyanStyle = new GUIStyle {normal = {textColor = Color.cyan}};
        GUIStyle whiteStyle = new GUIStyle {normal = {textColor = Color.white}};
        GUIStyle grayStyle = new GUIStyle {normal = {textColor = Color.gray}};
        GUIStyle blackStyle = new GUIStyle {normal = {textColor = Color.black}};

        // Positions
        Vector2 origin = Vector2.zero;
        Vector2 player = transform.position;
        Vector2 target = targetTransform.position;

        ////////////
        // Origin //
        ////////////

        // Draw X axis line
        Vector3 left = Vector3.left * 100f;
        Vector3 right = Vector3.right * 100f;
        Handles.DrawBezier(left, right, left, right, darkRedColor, null, 2f);
        // Draw Y axis line
        Vector3 up = Vector3.up * 100f;
        Vector3 down = Vector3.down * 100f;
        Handles.DrawBezier(up, down, up, down, darkGreenColor, null, 2f);
        // Draw Z axis line
        Vector3 forward = Vector3.forward * 100f;
        Vector3 back = Vector3.back * 100f;
        Handles.DrawBezier(forward, back, forward, back, darkBlueColor, null, 2f);

        // Draw Origin unit circle
        Handles.color = darkYellowColor;
        Handles.DrawWireDisc(origin, Vector3.forward, 1f, 2f);

        // Draw axis number labels
        // for (int i = -10; i <= 10; i++)
        // {
        //     if (i == 0) continue;
        //     Handles.Label(new Vector3(i / 10f - 0.011f, 0.005f, 0f), i.ToString("F0"), red);
        //     Handles.Label(new Vector3(-0.04f, i / 10f - (-0.025f), 0), i.ToString("F0"), green);
        // }

        /////////////////////
        // Origin To Point //
        /////////////////////

        // Draw lines from origin to player. separated at unit vector
        Vector2 opUnitVector = player.normalized;
        Handles.DrawBezier(origin, opUnitVector, origin, opUnitVector, Color.white, null, 4f);
        Handles.DrawBezier(opUnitVector, player, opUnitVector, player, Color.gray, null, 2f);

        // Draw unit vector angle in degrees label
        float playerAngle = Vector2.Angle(opUnitVector, Vector2.up);
        Handles.Label(opUnitVector / 2, playerAngle.ToString("F1") + "°", whiteStyle);

        // Draw Perpendicularity lines
        Vector2 playerX = new Vector2(player.x, 0);
        Vector2 playerY = new Vector2(0, player.y);
        Handles.DrawBezier(player, playerX, player, playerX, darkRedColor, null, 2f);
        Handles.DrawBezier(player, playerY, player, playerY, darkGreenColor, null, 2f);

        // Draw Perpendicularity numbers
        const float OFFSET01 = 0.1f;
        Handles.Label(new Vector2(OFFSET01 + player.x, 0), player.x.ToString("F2"), redStyle);
        Handles.Label(new Vector2(OFFSET01, player.y), player.y.ToString("F2"), greenStyle);

        // Draw length label, midway between o and p
        Vector2 originPlayerMiddle = KUtil.Middle(origin, player);
        float originPlayerDistance = Vector2.Distance(origin, player);
        Handles.Label(originPlayerMiddle, originPlayerDistance.ToString("F2"), grayStyle);

        // Draw p coords label
        const float OFFSET025 = 0.25f;
        Vector2 playerOffsetX = new Vector2(player.x + OFFSET01, player.y - OFFSET01);
        Vector2 playerOffsetY = new Vector2(player.x + OFFSET01, player.y - OFFSET025);
        Handles.Label(playerOffsetX, "X: " + player.x.ToString("F2"), redStyle);
        Handles.Label(playerOffsetY, "Y: " + player.y.ToString("F2"), greenStyle);

        /////////////////////
        // Point to Target //
        /////////////////////

        // Draw Unit circle
        Handles.color = darkYellowColor;
        Handles.DrawWireDisc(player, Vector3.forward, 1f, 2f);

        // Draw lines from player to target. separated at unit vector
        Vector2 ptUnitVector = KUtil.Direction(player, target);
        Vector2 ptUnitVectorPos = player + ptUnitVector;
        Handles.DrawBezier(player, ptUnitVectorPos, player, ptUnitVectorPos, Color.white, null, 4f);
        Handles.DrawBezier(ptUnitVectorPos, target, ptUnitVectorPos, target, Color.gray, null, 2f);

        // Draw player to target angle label
        float ptAngle = Vector2.Angle(ptUnitVector, Vector2.up);
        Handles.Label(player + ptUnitVector / 2, ptAngle.ToString("F1") + "°", whiteStyle);

        // Draw Target length label, midway between p and t
        Vector2 playerTargetMiddlePosition = KUtil.Middle(player, target);
        float playerTargetDistance = Vector2.Distance(player, target);
        Handles.Label(playerTargetMiddlePosition, playerTargetDistance.ToString("F2"), grayStyle);

        // Draw Target coords label
        Vector2 targetOffsetX = new Vector2(target.x + OFFSET01, target.y - OFFSET01);
        Vector2 targetOffsetY = new Vector2(target.x + OFFSET01, target.y - OFFSET025);
        Handles.Label(targetOffsetX, "X: " + target.x.ToString("F2"), redStyle);
        Handles.Label(targetOffsetY, "Y: " + target.y.ToString("F2"), greenStyle);

        // Traveling spheres
        if (loop && !jump && offsetP < originPlayerDistance)
        {
            offsetP += speed;
            Handles.color = Color.Lerp(redColor, greenColor, offsetP / originPlayerDistance);
            Vector2 originPlayerOffset = opUnitVector * offsetP;
            Handles.DrawSolidDisc(originPlayerOffset, Vector3.forward, 0.1f);
        }
        else if (loop && !jump && offsetP > originPlayerDistance)
        {
            offsetP = 0f;
            jump = true;
        }

        if (loop && jump && offsetT < playerTargetDistance)
        {
            offsetT += speed;
            Handles.color = Color.Lerp(greenColor, blueColor, offsetT / playerTargetDistance);
            Vector2 playerTargetOffset = player + ptUnitVector * offsetT;
            Handles.DrawSolidDisc(playerTargetOffset, Vector3.forward, 0.1f);
        }
        else if (loop && jump && offsetT > playerTargetDistance)
        {
            offsetT = 0f;
            jump = false;
        }
    }

    private void Start()
    {
        Vector2 a = new Vector2(-2f, 3f);
        Vector2 b = new Vector2(1f, 2f);

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
        ("KUtil.Middle(a, b) = " /* (-0.5, 2.5) */ + KUtil.Middle(a, b)).Log();
    }
}