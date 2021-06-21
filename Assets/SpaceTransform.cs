using UnityEngine;

public class SpaceTransform : MonoBehaviour
{
    public Vector2 localPoint;

    private readonly Color red = new Color(1f, 0.26f, 0.26f);
    private readonly Color green = new Color(0.36f, 1f, 0.32f);

    private void OnDrawGizmos()
    {
        Transform trans = transform;
        Vector2 pos = trans.position;
        Vector3 right = trans.right;
        Vector3 up = trans.up;

        DrawBasisVectors(pos, right, up);

        Gizmos.color = Color.cyan;
        Gizmos.DrawSphere(trans.ToWorld(localPoint), 0.05f);
    }

    private void DrawBasisVectors(Vector2 pos, Vector2 right, Vector2 up)
    {
        Gizmos.color = red;
        Gizmos.DrawRay(pos, right);
        Gizmos.color = green;
        Gizmos.DrawRay(pos, up);
        Gizmos.color = Color.white;
    }
}