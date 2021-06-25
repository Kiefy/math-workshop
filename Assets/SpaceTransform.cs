using UnityEngine;

public class SpaceTransform : MonoBehaviour
{
    public Vector2 localPoint;
    public Vector2 worldPoint;
    public Transform childTransform;

    private void OnDrawGizmos()
    {
        Transform trans = transform;
        Vector2 pos = trans.position;
        Vector2 up = trans.up;
        Vector2 right = trans.right;

        Vector2 LocalToWorld(Vector2 localPt)
        {
            //return pos + (right * localPt.x + up * localPt.y);
            return trans.TransformPoint(localPt); // Same as ^^
        }

        Vector2 WorldToLocal(Vector2 worldPt)
        {
            //Vector2 relativePt = worldPt - pos;
            //float x = Vector2.Dot(relativePt, right);
            //float y = Vector2.Dot(relativePt, up);
            //return new Vector2(x, y);
            return trans.InverseTransformPoint(worldPt); // Same as ^^
        }

        //Vector2 localToWorld = trans.ToWorld(localPoint);
        //Vector2 localToWorld = Kief.LocalToWorld(trans, localPoint);
        Vector2 localToWorld = LocalToWorld(localPoint); // Same as ^^

        Gizmos.color = Color.cyan;
        Gizmos.DrawSphere(localToWorld, 0.05f);
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(worldPoint, 0.125f);

        //childTransform.localPosition = Kief.WorldToLocal(trans, worldPoint);
        childTransform.localPosition = WorldToLocal(worldPoint); // Same as ^^
    }
}