using UnityEngine;

public class Looker : MonoBehaviour
{
    // The target marker.
    public Transform targetTransform;
    private Transform lookTransform;

    // Angular speed in radians per sec.
    public float speed = 1.0f;

    private void Start()
    {
        targetTransform = FindObjectOfType<Vectors>().gameObject.transform;
        lookTransform = transform;
    }

    private void Update()
    {
        Vector3 targetDirection = targetTransform.position - lookTransform.position;
        float singleStep = speed * Time.deltaTime;
        Vector3 newDirection = Vector3.RotateTowards(lookTransform.forward, targetDirection, singleStep, 0.0f);
        lookTransform.rotation = Quaternion.LookRotation(newDirection);
    }
}