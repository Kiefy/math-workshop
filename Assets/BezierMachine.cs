using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;

public class BezierMachine : MonoBehaviour
{
    public bool loop;
    [Range(0f, 0.02f)] public float speed;

    public Color controlHandleColor = Color.cyan;
    [Range(0f, 0.1f)] public float controlHandleRadius = 0.05f;
    [Range(0f, 1f)] public float controlHandleOpacity = 0.7f;

    public Color controlLineColor = new Color(0f, 0.5f, 0.5f);
    public float controlLineThickness = 0.7f;
    [Range(0f, 1f)] public float controlLineOpacity = 0.7f;

    [Range(0f, 0.1f)] public float constructJointRadius = 0.05f;
    [Range(0f, 1f)] public float constructJointOpacity = 0.7f;
    public float constructLineThickness = 4f;
    [Range(0f, 1f)] public float constructLineOpacity = 0.7f;

    public Color finalJointColor = Color.white;
    [Range(0f, 0.2f)] public float finalJointRadius = 0.1f;
    [Range(0f, 1f)] public float finalJointOpacity = 0.7f;

    public Transform[] bezierPoints;

    private float offset;
    private float lastTime;

    private readonly Color[] bezierColors = new Color[6];
    private LineRenderer lineRenderer;

    private readonly Color redColor = new Color(1f, 0.26f, 0.26f);
    private readonly Color greenColor = new Color(0.36f, 1f, 0.32f);
    private readonly Color blueColor = new Color(0.22f, 0.6f, 1f);

    private void OnDrawGizmos()
    {
        bezierColors[0] = redColor;
        bezierColors[1] = greenColor;
        bezierColors[2] = blueColor;
        bezierColors[3] = Color.yellow;
        bezierColors[4] = Color.cyan;
        bezierColors[5] = Color.magenta;

        if (loop && offset < 1f)
        {
            offset += speed / 10;
        }
        else if (loop && offset > 1f)
        {
            offset = 0f;
            lineRenderer.positionCount = 0;
        }

        Vector2[][] bezierTrees = new Vector2[bezierPoints.Length][];

        if (bezierTrees[0] == null)
        {
            bezierTrees[0] = new Vector2[bezierPoints.Length];

            for (int i = 0; i < bezierPoints.Length; i++)
            {
                bezierTrees[0][i] = bezierPoints[i].position;
            }
        }

        // Main Loop
        for (int i = 0; i < bezierTrees.Length; i++)
        {
            if (i == 0)
            {
                for (int x = 0; x < bezierTrees[i].Length; x++)
                {
                    if (x < bezierTrees[i].Length - 1)
                    {
                        if (bezierTrees[i + 1] == null)
                        {
                            bezierTrees[i + 1] = new Vector2[bezierTrees[i].Length - 1];
                        }

                        bezierTrees[i + 1][x] =
                            Vector2.Lerp(bezierTrees[i][x], bezierTrees[i][x + 1], offset);
                        Handles.color = bezierColors[i % 6] * constructJointOpacity;

                        Handles.DrawBezier(
                            bezierTrees[i][x],
                            bezierTrees[i][x + 1],
                            bezierTrees[i][x],
                            bezierTrees[i][x + 1],
                            controlLineColor * controlLineOpacity, null, controlLineThickness
                        );
                        Handles.DrawSolidDisc(bezierTrees[i + 1][x], Vector3.forward, constructJointRadius);
                    }
                }
            }
            else if (i < bezierTrees.Length - 1)
            {
                for (int x = 0; x < bezierTrees[i].Length; x++)
                {
                    if (x < bezierTrees[i].Length - 1)
                    {
                        if (bezierTrees[i + 1] == null)
                        {
                            bezierTrees[i + 1] = new Vector2[bezierTrees[i].Length - 1];
                        }

                        bezierTrees[i + 1][x] =
                            Vector2.Lerp(bezierTrees[i][x], bezierTrees[i][x + 1], offset);
                        Handles.color = bezierColors[i % 6] * constructJointOpacity;

                        Handles.DrawBezier(
                            bezierTrees[i][x],
                            bezierTrees[i][x + 1],
                            bezierTrees[i][x],
                            bezierTrees[i][x + 1],
                            bezierColors[(i - 1) % 6] * constructLineOpacity, null, constructLineThickness
                        );

                        if (i != bezierTrees.Length - 2)
                        {
                            Handles.DrawSolidDisc(bezierTrees[i + 1][x], Vector3.forward,
                                constructJointRadius);
                        }
                        else
                        {
                            Handles.color = finalJointColor * finalJointOpacity;

                            Handles.DrawSolidDisc(bezierTrees[i + 1][x], Vector3.forward, finalJointRadius);
                        }
                    }
                }
            }
        }

        if (bezierTrees[bezierTrees.Length - 1] != null)
        {
            lineRenderer = GetComponent<LineRenderer>();
            lineRenderer.startWidth = 0.08f;
            lineRenderer.endWidth = 0.02f;
            if (offset == 0)
            {
                lineRenderer.positionCount = 0;
            }

            float time = Time.time;
            if (time > lastTime + 0.025f)
            {
                int positionCount = lineRenderer.positionCount;
                positionCount++;
                lineRenderer.positionCount = positionCount;
                lastTime = time;
                lineRenderer.SetPosition(positionCount - 1, bezierTrees[bezierTrees.Length - 1][0]);
            }
        }

        // Control Handle
        foreach (Transform point in bezierPoints)
        {
            Handles.color = controlHandleColor * controlHandleOpacity;
            Handles.DrawSolidDisc(point.position, Vector3.forward, controlHandleRadius);
        }
    }
}