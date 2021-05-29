using UnityEngine;

public class Looker : MonoBehaviour
{
    // The target marker.
    public Transform targetTransform;

    // Angular speed in radians per sec.
    public float speed = 1.0f;

    private void Start()
    {
        targetTransform = FindObjectOfType<Vectors>().gameObject.transform;
    }

    private void Update()
    {
        Vector3 targetDirection = targetTransform.position - transform.position;
        float singleStep = speed * Time.deltaTime;
        Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, singleStep, 0.0f);
        Debug.DrawRay(transform.position, newDirection, Color.red);
        transform.rotation = Quaternion.LookRotation(newDirection);

        //Vector3 lookDirection = targetTransform.transform.position - transform.position;
        //Quaternion rotation = Quaternion.LookRotation(Vector3.up, -lookDirection)
        //                      * Quaternion.AngleAxis(90f, Vector3.right);
        //transform.rotation = rotation;
        //Vector3 cylPos = transform.position;
        //Vector3 targetPos = target.transform.position;
        //Vector3 fixedPosition = new Vector3(0f, 0f, targetPos.x);
        //transform.LookAt(targetPos, Vector3.left);
    }
}