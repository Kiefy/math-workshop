using UnityEditor;
using UnityEngine;

public class Vectors : MonoBehaviour
{
    public Transform target;

    private void OnDrawGizmos()
    {
        // Color styles
        GUIStyle red = new GUIStyle {normal = {textColor = new Color(1f, 0.26f, 0.26f)}};
        GUIStyle green = new GUIStyle {normal = {textColor = new Color(0.36f, 1f, 0.32f)}};
        GUIStyle blue = new GUIStyle {normal = {textColor = new Color(0.22f, 0.6f, 1f)}};
        GUIStyle cyan = new GUIStyle {normal = {textColor = Color.cyan}};
        GUIStyle yellow = new GUIStyle {normal = {textColor = Color.yellow}};
        GUIStyle white = new GUIStyle {normal = {textColor = Color.white}};
        GUIStyle gray = new GUIStyle {normal = {textColor = Color.gray}};
        GUIStyle black = new GUIStyle {normal = {textColor = Color.black}};

        Color darkYellow = new Color(0.5f, 0.45f, 0.01f);

        Vector2 o /* Origin */ = Vector2.zero;
        Vector2 p /* Player */ = transform.position;
        Vector2 t /* Target */ = target.transform.position;


        ////////////
        // Origin //
        ////////////

        // Draw huge cross
        Gizmos.color = Color.cyan;
        //Gizmos.DrawWireSphere(o, 100f);
        Gizmos.DrawLine(Vector3.left*100f, Vector3.right*100f);
        Gizmos.DrawLine(Vector3.up*100f, Vector3.down*100f);
        Gizmos.DrawLine(Vector3.forward*100f, Vector3.back*100f);

        // Draw Origin unit circle
        Handles.color = Color.gray;
        Handles.DrawWireDisc(o, Vector3.forward, 1f, 2f);

        // Draw axis number labels
        for (int i = -10; i <= 10; i++)
        {
            if (i == 0) continue;
            Handles.Label(new Vector3(i / 10f - 0.011f, 0.005f, 0f), i.ToString("F0"), red);
            Handles.Label(new Vector3(-0.04f, i / 10f - (-0.025f), 0), i.ToString("F0"), green);
        }

        /////////////////////
        // Origin To Point //
        /////////////////////

        // Draw lines from o to p. separated at unit vector
        Vector2 pDir = p.normalized;
        Handles.DrawBezier(o, pDir, o, pDir, Color.yellow, null, 3f);
        Handles.DrawBezier(pDir, p, pDir, p, darkYellow, null, 1f);

        // Draw Perpendicularity lines
        Gizmos.color = Color.cyan;
        Gizmos.DrawLine(p, new Vector3(p.x, 0, 0));
        Gizmos.DrawLine(p, new Vector3(0, p.y, 0));

        // Draw Perpendicularity numbers
        Handles.Label(new Vector3(p.x + 0.1f, 0f, 0f), p.x.ToString("F2"), cyan);
        Handles.Label(new Vector3(0.1f, p.y, 0f), p.y.ToString("F2"), cyan);

        // Draw length label, midway between o and p
        float pLen = Vector2.Distance(o, p);
        Vector2 pMid = KUtil.Middle(o,p);
        Handles.Label(new Vector3(pMid.x, pMid.y, 0f), pLen.ToString("F2"), black);

        // Draw p coords label
        Vector3 pPos = new Vector3(p.x + 0.1f, p.y - 0.1f, 0f);
        Vector3 pPos2 = new Vector3(p.x + 0.1f, p.y - 0.25f, 0f);
        Handles.Label(pPos, "X: " + p.x.ToString("F2"));
        Handles.Label(pPos2, "Y: " + p.y.ToString("F2"));

        /////////////////////
        // Point to Target //
        /////////////////////

        Handles.color = new Color(0f, 0f, 0f, 0.22f);
        Handles.DrawWireDisc(p, Vector3.forward, 1f, 2f);

        // Draw lines from p to t. separated at unit vector
        Vector2 tDir = (t - p).normalized;
        Handles.DrawBezier(p, p + tDir, p, p + tDir, Color.yellow, null, 3f);
        Handles.DrawBezier(t, p + tDir, t, p + tDir, darkYellow, null, 1f);

        // Draw length label, midway between p and t
        float tLen = Vector2.Distance(p, t);
        Vector2 tMid = p + (t - p) / 2;
        Handles.Label(new Vector3(tMid.x, tMid.y, 0f), tLen.ToString("F2"), black);

        // Draw t coords label
        Vector3 tPos = new Vector3(t.x + 0.1f, t.y - 0.1f, 0f);
        Vector3 tPos2 = new Vector3(t.x + 0.1f, t.y - 0.25f, 0f);
        Handles.Label(tPos, "X: " + t.x.ToString("F2"));
        Handles.Label(tPos2, "Y: " + t.y.ToString("F2"));
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
        ("     4 + 9                  = " /* 13 */ + (a.x * a.x + a.y * a.y)).Log();
        ("Sqrt(13)                    = " /* 3.6.. */ + (a.x * a.x + a.y * a.y).Sqrt()).Log();
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
        ("(a + (b - a) / 2)  = " /* (-0.5, 2.5) */ + (a + (b - a) / 2)).Log();
        ("KUtil.Middle(a, b) = " /* (-0.5, 2.5) */ + KUtil.Middle(a, b)).Log();
    }
}