using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class CrossProduct : MonoBehaviour
{
  [SerializeField] [Range(0.05f, 1.0f)] private float length = 0.05f;
  [SerializeField] [Range(1f, 10f)] private float width = 4f;
  [SerializeField] [Range(1, 100)] private int maxBounces = 100;

  private static List<Vector3> points;

  private void OnValidate() => points = new List<Vector3>();

  private void OnDrawGizmos()
  {
    Transform trans = transform;
    RaycastHit[] hits = new RaycastHit[maxBounces];
    Vector3 prevPos = trans.position;
    Vector3 prevDir = trans.forward;

    static void DrawRay(Vector3 pos, Vector3 dir, float w) => Handles.DrawAAPolyLine(w, pos, pos + dir);

    points = new List<Vector3>();
    for (int i = 0; i < maxBounces; i++)
    {
      if (i != maxBounces - 1 && Physics.Raycast(prevPos, prevDir, out hits[i]))
      {
        Vector3 pos = hits[i].point;
        Vector3 up = hits[i].normal;
        Vector3 right = Vector3.Cross(up, prevDir).normalized;
        Vector3 forward = Vector3.Cross(right, up);

        Handles.color = new Color(1f, 0.26f, 0.26f);
        DrawRay(pos, right * length, width);
        Handles.color = new Color(0.36f, 1f, 0.32f);
        DrawRay(pos, up * length, width);
        Handles.color = new Color(0.22f, 0.6f, 1f);
        DrawRay(pos, forward * length, width);
        Handles.color = new Color(0.5f, 0.5f, 0.5f, 0.5f);
        Handles.DrawAAPolyLine(prevPos, pos);
        Handles.color = new Color(1f, 1f, 1f, 0.2f);
        DrawRay(prevPos, prevDir * (Vector3.Distance(prevPos, pos) / 4), width);

        prevPos = pos;
        prevDir = Kief.Reflect(up, prevDir);
      }
      else
      {
        Handles.color = new Color(1f, 0.26f, 0.26f);
        DrawRay(prevPos, prevDir / 8, width * 2f);

        points = new List<Vector3> { trans.position };
        foreach (RaycastHit hit in hits)
          if (hit.point != Vector3.zero)
            points.Add(hit.point);
        break;
      }
    }

    Vectors.LaserStart(points);
  }
}