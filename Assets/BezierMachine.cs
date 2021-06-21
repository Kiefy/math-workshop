using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;

#endif
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

    [Range(0f, 0.1f)] public float jointRadius = 0.05f;
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

        switch (loop)
        {
            case true when offset < 1f:
                offset += speed / 10;
                break;
            case true when offset > 1f:
                offset = 0f;
                lineRenderer.positionCount = 0;
                break;
        }

        Vector2[][] bTrees = new Vector2[bezierPoints.Length][];

        if (bTrees[0] == null)
        {
            bTrees[0] = new Vector2[bezierPoints.Length];
            for (int i = 0; i < bezierPoints.Length; i++) bTrees[0][i] = bezierPoints[i].position;
        }

        // Main Loop
        for (int i = 0; i < bTrees.Length; i++)
        {
            if (i == 0)
            {
                for (int x = 0; x < bTrees[i].Length; x++)
                {
                    if (x >= bTrees[i].Length - 1) continue;
                    bTrees[i + 1] ??= new Vector2[bTrees[i].Length - 1];
                    bTrees[i + 1][x] = Vector2.Lerp(bTrees[i][x], bTrees[i][x + 1], offset);
#if UNITY_EDITOR
                    Handles.color = bezierColors[i % 6] * constructJointOpacity;
                    Handles.DrawBezier(bTrees[i][x], bTrees[i][x + 1], bTrees[i][x], bTrees[i][x + 1],
                        controlLineColor * controlLineOpacity, null, controlLineThickness);
                    Handles.DrawSolidDisc(bTrees[i + 1][x], Vector3.forward, jointRadius);
#endif
                }
            }
            else if (i < bTrees.Length - 1)
            {
                for (int x = 0; x < bTrees[i].Length; x++)
                {
                    if (x >= bTrees[i].Length - 1) continue;
                    bTrees[i + 1] ??= new Vector2[bTrees[i].Length - 1];
                    bTrees[i + 1][x] = Vector2.Lerp(bTrees[i][x], bTrees[i][x + 1], offset);
#if UNITY_EDITOR
                    Handles.color = bezierColors[i % 6] * constructJointOpacity;
                    Handles.DrawBezier(bTrees[i][x], bTrees[i][x + 1], bTrees[i][x], bTrees[i][x + 1],
                        bezierColors[(i - 1) % 6] * constructLineOpacity, null, constructLineThickness);

                    if (i != bTrees.Length - 2)
                    {
                        Handles.DrawSolidDisc(bTrees[i + 1][x], Vector3.forward, jointRadius);
                    }
                    else
                    {
                        Handles.color = finalJointColor * finalJointOpacity;
                        Handles.DrawSolidDisc(bTrees[i + 1][x], Vector3.forward, finalJointRadius);
                    }
#endif
                }
            }
        }

        if (bTrees[bTrees.Length - 1] != null)
        {
            lineRenderer = GetComponent<LineRenderer>();
            lineRenderer.startWidth = 0.08f;
            lineRenderer.endWidth = 0.02f;
            if (offset == 0) lineRenderer.positionCount = 0;
            float time = Time.time;
            if (time > lastTime + 0.025f)
            {
                int positionCount = lineRenderer.positionCount;
                positionCount++;
                lineRenderer.positionCount = positionCount;
                lastTime = time;
                lineRenderer.SetPosition(positionCount - 1, bTrees[bTrees.Length - 1][0]);
            }
        }

        // Control Handle
#if UNITY_EDITOR
        foreach (Transform point in bezierPoints)
        {
            Handles.color = controlHandleColor * controlHandleOpacity;
            Handles.DrawSolidDisc(point.position, Vector3.forward, controlHandleRadius);
        }
#endif
    }
}