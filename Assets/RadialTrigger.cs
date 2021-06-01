using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;

#endif

public class RadialTrigger : MonoBehaviour
{
    public Transform objTf;

#if UNITY_EDITOR
    [Range(0f, 4f)] public float radius = 1f;
    private float offset;

    private void OnDrawGizmos()
    {
        Vector2 position = transform.position;
        Vector2 objPos = objTf.transform.position;

        float dist = Vector2.Distance(position, objPos);
        bool isInside = dist < radius;

        if (isInside)
        {
            Handles.color = Color.red;
            Handles.DrawDottedLine(position, objPos, 0.1f);
            // Traveling spheres
            if (offset < dist)
            {
                offset += 0.005f;
                Handles.color = Color.Lerp(Color.yellow, Color.red, offset / dist);
                Vector2 originPlayerOffset = position + KUtil.Direction(position, objPos) * offset;
                Handles.DrawSolidDisc(originPlayerOffset, Vector3.forward, 0.025f);
            }
            else if (offset > dist)
            {
                offset = 0f;
            }
        }

        Handles.color = isInside ? Color.red : Color.gray;
        Handles.DrawWireDisc(position, Vector3.forward, radius, 2f);
    }
#endif
}