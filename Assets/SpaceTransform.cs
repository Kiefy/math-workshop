using UnityEngine;

public class SpaceTransform : MonoBehaviour
{
    public Vector2 localPoint;
    public Vector2 worldPoint;
    public Transform childTransform;

    private readonly Color red = new Color(1f, 0.26f, 0.26f);
    private readonly Color green = new Color(0.36f, 1f, 0.32f);

    private void OnDrawGizmos()
    {
        Transform trans = transform;
        Vector2 pos = trans.position;
        Vector3 right = trans.right;
        Vector3 up = trans.up;

        Vector2 LocalToWorld(Vector2 localPt)
        {
            Vector2 worldOffset = right * localPt.x + up * localPt.y;
            return pos + worldOffset;
        }

        Vector2 WorldToLocal(Vector2 worldPt)
        {
            Vector2 relativePt = worldPt - pos;
            float x = Vector2.Dot(relativePt, right);
            float y = Vector2.Dot(relativePt, up);
            return new Vector2(x, y);
        }

        DrawBasisVectors(pos, right, up);
        //DrawBasisVectors(Vector2.zero, Vector2.right, Vector2.up);

        //Vector2 localToWorld = trans.ToWorld(localPoint);
        //Vector2 localToWorld = Kief.LocalToWorld(trans, localPoint);
        Vector2 localToWorld = LocalToWorld(localPoint);

        Gizmos.color = Color.cyan;
        Gizmos.DrawSphere(localToWorld, 0.05f);
        //Gizmos.color = Color.yellow;
        //Gizmos.DrawSphere(worldPoint, 0.05f);

        childTransform.localPosition = WorldToLocal(worldPoint);
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