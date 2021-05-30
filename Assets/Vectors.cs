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
        Gizmos.DrawWireSphere(o, 100f);

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
        Vector2 pMid = o + (p - o) / 2;
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
        //const float X = -2, Y = 3;

        "╭────────┰─────────────────────────────────────╮".Log();
        "│  <color=#FF4242>┐ ┬╮</color>  ┃  <color=#FF4242>╭╮ ╭╮ ╭╮ ╷  ╭╮ ┌╮   ╭╴ ╷  ╭╮ ╭╮╶┬╴</color> │"
            .Log();
        "│  <color=#FF4242>│ ││</color>  ┃  <color=#FF4242>╰╮ │  ├┤ │  ├┤ ├┤   ├╴ │  ││ ├┤ │ </color> │"
            .Log();
        "│  <color=#FF4242>┴ ┴╯</color>  ┃  <color=#FF4242>╰╯ ╰╯ ╵╵ ╰─ ╵╵ ╵╰   ╵  ╰─ ╰╯ ╵╵ ╵ </color> │"
            .Log();
        "╰────────┸─────────────────────────────────────╯".Log();

        "╭─────┰──────────────────────────────╮".Log();
        "│ <color=cyan>+ -</color> ┃ <color=cyan>Offset. Addition/Subtraction</color> │".Log();
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
        "<color=yellow>a - b</color>  ==  <color=yellow>a + (-b)</color>".Log();

        "╭─────┰────────────────────────╮".Log();
        "│ <color=cyan>* /</color> ┃ <color=cyan>Scale. Multiply/Divide</color> │".Log();
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
        "<color=yellow>a / b</color>  ==  <color=yellow>a * (1 / b)</color>".Log();

        "╭─────────┰───────────────────────────╮".Log();
        "│ <color=cyan>Sign(x)</color> ┃ <color=cyan>Direction. either -1 or 1</color> │".Log();
        "╰─────────┸───────────────────────────╯".Log();
        ("Mathf.Sign(-10) = " /* -1 */ + Mathf.Sign(-10)).Log();
        ("Mathf.Sign(  0) =  " /* 1 */ + Mathf.Sign(0)).Log();
        ("Mathf.Sign( 10) =  " /* 1 */ + Mathf.Sign(10)).Log();

        "╭────────┰────────────────────────────────────────────────╮".Log();
        "│ <color=cyan>Abs(x)</color> ┃ <color=cyan>Length/Magnitude. Convert negative to positive</color> │"
            .Log();
        "╰────────┸────────────────────────────────────────────────╯".Log();
        ("Mathf.Abs(-10) = " /* 10 */ + Mathf.Abs(-10)).Log();
        ("Mathf.Abs(  0) =  " /* 0 */ + Mathf.Abs(0)).Log();
        ("Mathf.Abs( 10) = " /* 10 */ + Mathf.Abs(10)).Log();

        "╭────────────┰──────────╮".Log();
        "│ <color=cyan>Abs(a - b)</color> ┃ <color=cyan>Distance</color> │".Log();
        "╰────────────┸──────────╯".Log();
        ("Mathf.Abs( 1 -  3) = " /* 2 */ + Mathf.Abs(1 - 3)).Log();
        ("Mathf.Abs( 3 -  1) = " /* 2 */ + Mathf.Abs(3 - 1)).Log();
        ("Mathf.Abs(-3 - -1) = " /* 2 */ + Mathf.Abs(-3 - -1)).Log();
        ("Mathf.Abs(-1 -  1) = " /* 2 */ + Mathf.Abs(-1 - 1)).Log();

        "╭──────────┰─────────────────────────────────────────╮".Log();
        "│ <color=cyan>Round(f)</color> ┃ <color=cyan>Convert decimal to nearest whole number</color> │"
            .Log();
        "╰──────────┸─────────────────────────────────────────╯".Log();
        ("Mathf.Round( 1.499) =  " /* 1 */ + Mathf.Round(1.499f)).Log();
        ("Mathf.Round( 1.5  ) =  " /* 2 */ + Mathf.Round(1.5f)).Log();
        ("Mathf.Round(-1.499) = " /* -1 */ + Mathf.Round(-1.499f)).Log();
        ("Mathf.Round(-1.5  ) = " /* -2 */ + Mathf.Round(-1.5f)).Log();

        "╭────────────────────┰───────────────────────────────╮".Log();
        "│ <color=cyan>Clamp(v, min, max)</color> ┃ <color=cyan>Limit v between a min and max</color> │"
            .Log();
        "╰────────────────────┸───────────────────────────────╯".Log();
        ("Mathf.Clamp(-0.5, -1, 1) = " /* -0.5 */ + Mathf.Clamp(-0.5f, -1, 1)).Log();
        ("Mathf.Clamp(-0.5,  0, 1) =  " /* 0 */ + Mathf.Clamp(-0.5f, 0, 1)).Log();
        ("Mathf.Clamp( 0,    0, 1) =  " /* 0 */ + Mathf.Clamp(0f, 0, 1)).Log();
        ("Mathf.Clamp( 0.5,  0, 1) =  " /* 0.5 */ + Mathf.Clamp(0.5f, 0, 1)).Log();
        ("Mathf.Clamp( 1,    0, 1) =  " /* 1 */ + Mathf.Clamp(1f, 0, 1)).Log();
        ("Mathf.Clamp( 1.5,  0, 1) =  " /* 1 */ + Mathf.Clamp(1.5f, 0, 1)).Log();
        ("Mathf.Clamp( 1.5,  0, 2) =  " /* 1.5 */ + Mathf.Clamp(1.5f, 0, 2)).Log();

        "╭─────────┰───────────────────────╮".Log();
        "│  <color=#5BFF51>╭╮ ┬╮</color>  ┃  <color=#5BFF51>╷╷ ╭╴ ╭╮╶┬╴╭╮ ┌╮ ╭╮</color>  │".Log();
        "│  <color=#5BFF51>╭╯ ││</color>  ┃  <color=#5BFF51>││ ├╴ │  │ ││ ├┤ ╭╯</color>  │".Log();
        "│  <color=#5BFF51>└╴ ┴╯</color>  ┃  <color=#5BFF51>╰┘ ╰─ ╰╯ ╵ ╰╯ ╵╰ └╴</color>  │".Log();
        "╰─────────┸───────────────────────╯".Log();

        "╭─────────────┰───────────────────────────────────╮".Log();
        "│ <color=cyan>a.magnitude</color> ┃ <color=cyan>Get the length of a (from origin)</color> │".Log();
        "╰─────────────┸───────────────────────────────────╯".Log();
        "<color=yellow>a: -2, 3</color>".Log();
        "╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌".Log();
        ("     <color=yellow>a.x</color> * <color=yellow>a.x</color>              = " /* 4 */ + a.x * a.x)
            .Log();
        ("     <color=yellow>a.y</color> * <color=yellow>a.y</color>              = " /* 9 */ + a.y * a.y)
            .Log();
        ("     4 + 9                  = " /* 13 */ + (a.x * a.x + a.y * a.y)).Log();
        ("Sqrt(13)                    = " /* 3.6... */ + (a.x * a.x + a.y * a.y).Sqrt()).Log();
        ("Sqrt(<color=yellow>a.x</color> * <color=yellow>a.x</color> + <color=yellow>a.y</color> * <color=yellow>a.y</color>) = " /* 3.6... */ +
         (a.x * a.x + a.y * a.y).Sqrt()).Log();
        ("<color=yellow>a</color>.magnitude                 = " + (a.magnitude)).Log();

        "╭────────────────────────┰──────────────────────────╮".Log();
        "│ <color=cyan>Vector2.Distance(a, b)</color> ┃ <color=cyan>Get distance from a to b</color> │".Log();
        "╰────────────────────────┸──────────────────────────╯".Log();
        "<color=yellow>a: -2, 3</color>".Log();
        "<color=yellow>b:  1, 2</color>".Log();
        "╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌".Log();
        ("(<color=yellow>b</color> - <color=yellow>a</color>).magnitude      = " /*  */ + (a - b).magnitude)
            .Log();
        ("Vector2.Distance(<color=yellow>a</color>, <color=yellow>b</color>) = " /*  */ +
         Vector2.Distance(a, b)).Log();
        "╭────────────────────┰──────────────────────────────╮".Log();
        "│ <color=cyan>KUtil.Middle(a, b)</color> ┃ <color=cyan>Get midpoint between a and b</color> │".Log();
        "╰────────────────────┸──────────────────────────────╯".Log();
        "<color=yellow>a: -2, 3</color>".Log();
        "<color=yellow>b:  1, 2</color>".Log();
        "╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌".Log();
        ("(<color=yellow>a</color> + (<color=yellow>b</color> - <color=yellow>a</color>) / 2)  = " /*  */ +
         (a + (b - a) / 2)).Log();
        ("KUtil.Middle(<color=yellow>a</color>, <color=yellow>b</color>) = " /*  */ + KUtil.Middle(a, b))
            .Log();
    }
}