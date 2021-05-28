using UnityEngine;

public class Vectors : MonoBehaviour
{
    private void Start()
    {
        // SCALAR 1D MATH

        "╭───┬─────────────────╮".Log();
        "│ + │ Addition/Offset │".Log();
        "╰───┴─────────────────╯".Log();
        (" 2 +  1 =  " + (2 + 1)).Log(); //   3
        (" 1 + -1 =  " + (1 + -1)).Log(); //  0
        ("-1 +  2 =  " + (-1 + 2)).Log(); //  1
        ("-2 + -1 = " + (-2 + -1)).Log(); // -3

        "╭───┬────────────────╮".Log();
        "│ * │ Multiply/Scale │".Log();
        "╰───┴────────────────╯".Log();
        (" 10 *  2   =  " + (10 * 2)).Log(); //   20
        (" 10 *  0.5 =   " + (10 * 0.5)).Log(); // 5
        (" -5 *  4   = " + (-5 * 4)).Log(); //   -20
        ("-10 * -2   =  " + (-10 * -2)).Log(); // 20

        "╭─────────┬───────────────────────────╮".Log();
        "│ Sign(x) ╪ Direction. either -1 or 1 │".Log();
        "╰─────────┴───────────────────────────╯".Log();
        ("Mathf.Sign(-10) = " + Mathf.Sign(-10)).Log(); // -1
        ("Mathf.Sign(  0) =  " + Mathf.Sign(0)).Log(); //   1
        ("Mathf.Sign( 10) =  " + Mathf.Sign(10)).Log(); //  1

        "╭────────┬────────────────────────────────────────────────╮".Log();
        "│ Abs(x) ╪ Length/Magnitude. Convert negative to positive │".Log();
        "╰────────┴────────────────────────────────────────────────╯".Log();
        ("Mathf.Abs(-10) = " + Mathf.Abs(-10)).Log(); // 10
        ("Mathf.Abs(  0) =  " + Mathf.Abs(0)).Log(); //   0
        ("Mathf.Abs( 10) = " + Mathf.Abs(10)).Log(); //  10

        "╭────────────┬──────────╮".Log();
        "│ Abs(a - b) ╪ Distance │".Log();
        "╰────────────┴──────────╯".Log();
        ("Mathf.Abs( 1 -  3) = " + Mathf.Abs(1 - 3)).Log(); //   2
        ("Mathf.Abs( 3 -  1) = " + Mathf.Abs(3 - 1)).Log(); //   2
        ("Mathf.Abs(-3 - -1) = " + Mathf.Abs(-3 - -1)).Log(); // 2
        ("Mathf.Abs(-1 -  1) = " + Mathf.Abs(-1 - 1)).Log(); //  2

        "╭─────────────────────────────────╮".Log();
        "│ Abs() powered Distance function │".Log();
        "╰─────────────────────────────────╯".Log();
        "Distance(float a, float b) { return Mathf.Abs(a - b); }".Log();
        static float Distance(float a, float b) { return Mathf.Abs(a - b); }
        "╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌".Log();
        ("Distance( 1,  3) = " + Distance(1, 3)).Log(); //   2
        ("Distance( 3,  1) = " + Distance(3, 1)).Log(); //   2
        ("Distance(-3, -1) = " + Distance(-3, -1)).Log(); // 2
        ("Distance(-1,  1) = " + Distance(-1, 1)).Log(); //  2
    }
}