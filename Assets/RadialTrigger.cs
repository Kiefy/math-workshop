using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;

#endif

public class RadialTrigger : MonoBehaviour
{
    private Transform objTf;

    [Range(0f, 4f)] public float radius = 1f;

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Vector2 origin = transform.position;
        Handles.DrawWireDisc(origin, Vector3.forward, radius);
    }
#endif
}