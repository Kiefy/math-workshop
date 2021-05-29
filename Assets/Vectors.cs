using UnityEditor;
using UnityEngine;

public class Vectors : MonoBehaviour
{
    private void Start()
    {
        // 1D, Scalar, Float, x Axis
        "╭────────┰─────────────────────────────────────╮".Log();
        "│  <color=cyan>┐ ┬╮</color><color=yellow>x</color> ┃  <color=cyan>╭╮ ╭╮ ╭╮ ╷  ╭╮ ┌╮   ╭╴ ╷  ╭╮ ╭╮╶┬╴</color> │"
            .Log();
        "│  <color=cyan>│ ││</color>  ┃  <color=cyan>╰╮ │  ├┤ │  ├┤ ├┤   ├╴ │  ││ ├┤ │ </color> │".Log();
        "│  <color=cyan>┴ ┴╯</color>  ┃  <color=cyan>╰╯ ╰╯ ╵╵ ╰─ ╵╵ ╵╰   ╵  ╰─ ╰╯ ╵╵ ╵ </color> │".Log();
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
        "│ <color=cyan>Abs(x)</color> ┃ <color=cyan>Length/Magnitude. Convert negative to positive</color> │".Log();
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
        "│ <color=cyan>Round(f)</color> ┃ <color=cyan>Convert decimal to nearest whole number</color> │".Log();
        "╰──────────┸─────────────────────────────────────────╯".Log();
        ("Mathf.Round( 1.499) =  " /* 1 */ + Mathf.Round(1.499f)).Log();
        ("Mathf.Round( 1.5  ) =  " /* 2 */ + Mathf.Round(1.5f)).Log();
        ("Mathf.Round(-1.499) = " /* -1 */ + Mathf.Round(-1.499f)).Log();
        ("Mathf.Round(-1.5  ) = " /* -2 */ + Mathf.Round(-1.5f)).Log();

        "╭────────────────────┰───────────────────────────────╮".Log();
        "│ <color=cyan>Clamp(v, min, max)</color> ┃ <color=cyan>Limit v between a min and max</color> │".Log();
        "╰────────────────────┸───────────────────────────────╯".Log();
        ("Mathf.Clamp(-0.5, -1, 1) = " /* -0.5 */ + Mathf.Clamp(-0.5f, -1, 1)).Log();
        ("Mathf.Clamp(-0.5,  0, 1) =  " /* 0 */ + Mathf.Clamp(-0.5f, 0, 1)).Log();
        ("Mathf.Clamp( 0,    0, 1) =  " /* 0 */ + Mathf.Clamp(0f, 0, 1)).Log();
        ("Mathf.Clamp( 0.5,  0, 1) =  " /* 0.5 */ + Mathf.Clamp(0.5f, 0, 1)).Log();
        ("Mathf.Clamp( 1,    0, 1) =  " /* 1 */ + Mathf.Clamp(1f, 0, 1)).Log();
        ("Mathf.Clamp( 1.5,  0, 1) =  " /* 1 */ + Mathf.Clamp(1.5f, 0, 1)).Log();
        ("Mathf.Clamp( 1.5,  0, 2) =  " /* 1.5 */ + Mathf.Clamp(1.5f, 0, 2)).Log();

        // 2D, Vector2, xy Axis
        "╭─────────┰───────────────────────╮".Log();
        "│  <color=cyan>╭╮ ┬╮</color><color=yellow>x</color> ┃  <color=cyan>╷╷ ╭╴ ╭╮╶┬╴╭╮ ┌╮ ╭╮</color>  │"
            .Log();
        "│  <color=cyan>╭╯ ││</color><color=yellow>y</color> ┃  <color=cyan>││ ├╴ │  │ ││ ├┤ ╭╯</color>  │"
            .Log();
        "│  <color=cyan>└╴ ┴╯</color>  ┃  <color=cyan>╰┘ ╰─ ╰╯ ╵ ╰╯ ╵╰ └╴</color>  │".Log();
        "╰─────────┸───────────────────────╯".Log();
    }

    private void OnDrawGizmos()
    {
        Vector2 point = transform.position;
        Vector2 dirToPoint = point.normalized;

        GUIStyle cyan = new GUIStyle {normal = {textColor = Color.cyan}};
        GUIStyle white = new GUIStyle {normal = {textColor = Color.white}};
        GUIStyle red = new GUIStyle {normal = {textColor = new Color(1f, 0.26f, 0.26f)}};
        GUIStyle green = new GUIStyle {normal = {textColor = new Color(0.36f, 1f, 0.32f)}};

        Gizmos.color = new Color(0.5f, 0.5f, 0.5f, 0.5f);
        Gizmos.DrawWireSphere(Vector2.zero, 1f);
        Gizmos.DrawWireSphere(Vector2.zero, 100f);

        Gizmos.color = new Color(0.36f, 1f, 0.32f);
        Gizmos.DrawLine(Vector2.zero, dirToPoint);

        Gizmos.color = new Color(1f, 0.26f, 0.26f);
        Gizmos.DrawLine(dirToPoint, point);

        Gizmos.color = Color.cyan;
        Gizmos.DrawLine(point, new Vector3(point.x, 0, 0));

        Handles.Label(new Vector3(point.x + 0.015f, 0.055f, 0), point.x.ToString("F2"), cyan);

        Gizmos.DrawLine(point, new Vector3(0, point.y, 0));

        Handles.Label(new Vector3(0.01f, point.y, 0), point.y.ToString("F2"), cyan);

        //Handles.color = new Color(1f, 0.26f, 0.26f);
        for (int i = -10; i <= 10; i++)
        {
            if (i != 0)
            {
                Handles.Label(new Vector3(i / 10f - 0.011f, 0.005f, 0f), i.ToString("F0"), red);
            }
        }

        for (int i = -10; i <= 10; i++)
        {
            if (i != 0)
            {
                Handles.Label(new Vector3(-0.04f, i / 10f - (-0.025f), 0), i.ToString("F0"), green);
            }
        }
    }
}