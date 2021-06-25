using UnityEngine;

public class AreaCalc : MonoBehaviour
{
    public Mesh mesh;
    public float area;

    private void OnValidate()
    {
        Vector3[] verts = mesh.vertices;
        int[] tris = mesh.triangles;

        area = 0f;
        for (int i = 0; i < tris.Length; i += 3)
        {
            Vector3 a = verts[tris[i]];
            Vector3 b = verts[tris[i + 1]];
            Vector3 c = verts[tris[i + 2]];
            area += Vector3.Cross(b - a, c - a).magnitude;
        }

        area /= 2;
    }

    private void OnDrawGizmos()
    {
        // Draw mesh verts
        Vector3[] verts = mesh.vertices;
        foreach (Vector3 t in verts) Gizmos.DrawSphere(transform.TransformPoint(t), 0.01f);
    }
}
