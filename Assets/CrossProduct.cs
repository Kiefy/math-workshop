using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CrossProduct : MonoBehaviour
{
    [SerializeField] [Range(0.05f, 1.0f)] private float length = 0.05f;
    [SerializeField] [Range(1f, 10f)] private float width = 4f;
    [SerializeField] [Range(1, 100)] private int maxBounces = 100;

    public Mesh mesh;
    public float area;
    public static List<Vector3> points;

    private void OnValidate()
    {
        points = new List<Vector3>();

        // Vector3[] verts = mesh.vertices;
        // int[] tris = mesh.triangles;
        //
        // area = 0f;
        // for (int i = 0; i < tris.Length; i += 3)
        // {
        //     Vector3 a = verts[tris[i]];
        //     Vector3 b = verts[tris[i + 1]];
        //     Vector3 c = verts[tris[i + 2]];
        //     area += Vector3.Cross(b - a, c - a).magnitude;
        // }
        //
        // area /= 2;
    }

    private void OnDrawGizmos()
    {
        // Draw mesh verts
        //Vector3[] verts = mesh.vertices;
        //foreach (Vector3 t in verts) Gizmos.DrawSphere(transform.TransformPoint(t), 0.01f);

        // Laser bouncer
        Transform trans = transform;
        RaycastHit[] hits = new RaycastHit[maxBounces];
        bool didHit = true;
        Vector3 prevPos = trans.position;
        Vector3 prevDir = trans.forward;

        void DrawRay(Vector3 pos, Vector3 dir) => Handles.DrawAAPolyLine(width, pos, pos + dir);

        for (int i = 0; i < maxBounces; i++)
        {
            if (didHit && Physics.Raycast(prevPos, prevDir, out hits[i]))
            {
                Vector3 pos = hits[i].point;
                Vector3 up = hits[i].normal;
                Vector3 right = Vector3.Cross(up, prevDir).normalized;
                Vector3 forward = Vector3.Cross(right, up);

                Handles.color = new Color(1f, 0.26f, 0.26f);
                DrawRay(pos, right * length);
                Handles.color = new Color(0.36f, 1f, 0.32f);
                DrawRay(pos, up * length);
                Handles.color = new Color(0.22f, 0.6f, 1f);
                DrawRay(pos, forward * length);

                //Vector3 currDir = prevDir - Vector3.Dot(up, prevDir) * up * 2f;
                Vector3 currDir = Kief.Reflect(up, prevDir); // Same as ^^

                Handles.color = Color.magenta;
                Handles.DrawAAPolyLine(prevPos, pos);
                Handles.color = Color.yellow;
                DrawRay(prevPos, prevDir * (Vector3.Distance(prevPos, pos) / 2));

                prevPos = pos;
                prevDir = currDir;
            }
            else
            {
                didHit = false; // Cease casting rays
                Handles.color = new Color(0.36f, 0.36f, 0.36f);
                DrawRay(prevPos, prevDir / 4);

                points = new List<Vector3> {trans.position};
                for (int j = 0; j < hits.Length - 1; j++)
                {
                    if (hits[j].point != Vector3.zero) points.Add(hits[j].point);
                }
            }
        }
    }
}