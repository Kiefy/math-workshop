using UnityEngine;

public class Looker : MonoBehaviour
{
  // The target marker.
  public Transform targetTransform;
  public float speed = 1.0f;

  private Transform lookTransform;

  private void Start()
  {
    targetTransform = FindObjectOfType<Vectors>().gameObject.transform;
    lookTransform = transform;
  }

  private void Update()
  {
    Vector3 targetDir = targetTransform.position - lookTransform.position;
    float singleStep = speed * Time.deltaTime;
    Vector3 newDir = Vector3.RotateTowards(lookTransform.forward, targetDir, singleStep, 0.0f);
    lookTransform.rotation = Quaternion.LookRotation(newDir);
  }
}