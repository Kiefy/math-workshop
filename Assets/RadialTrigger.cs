using UnityEngine;
using UnityEngine.Serialization;
#if UNITY_EDITOR
using UnityEditor;

#endif

// Vector2.Distance()
// Handles.color
// Handles.DrawDottedLine()
// Handles.DrawSolidDisc()
// Color.Lerp()
// KUtil.Direction()
// Handles.DrawWireDisc()
// GetComponent<>()

public class RadialTrigger : MonoBehaviour
{
    public Transform target;
    [Range(0f, 4f)] public float viewRadius = 1f;

    private LookTrigger lookTrigger;
    private float counter;

    private void OnDrawGizmos()
    {
        lookTrigger = GetComponent<LookTrigger>();
        Vector2 center = transform.position;
        Vector2 targetPos = target.position;
        float distance = Vector2.Distance(center, targetPos);
        bool isInside = distance < viewRadius;
        if (isInside && lookTrigger.canSeeTarget)
        {
#if UNITY_EDITOR
            Handles.color = Color.red;
            Handles.DrawDottedLine(center, targetPos, 0.1f);
#endif
            if (counter < distance)
            {
                counter += 0.005f;
#if UNITY_EDITOR
                Handles.color = Color.Lerp(Color.yellow, Color.red, counter / distance);
                Vector2 originPlayerOffset = center + Kief.Direction(center, targetPos) * counter;
                Handles.DrawSolidDisc(originPlayerOffset, Vector3.forward, 0.025f);
#endif
            }
            else if (counter > distance) counter = 0f;
        }
#if UNITY_EDITOR
        Handles.color = isInside ? Color.red : Color.gray;
        Handles.DrawWireDisc(center, Vector3.forward, viewRadius, 2f);
#endif
    }
}