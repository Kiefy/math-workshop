using UnityEngine;

public class LookTrigger : MonoBehaviour
{
  public Transform target;
  [Range(0f, 1f)] public float accuracy = 0.5f;
  public bool canSeeTarget;

  private void OnDrawGizmos()
  {
    Transform center = transform;
    Vector2 centerPos = center.position;
    Vector2 targetPos = target.position;
    Vector2 lookDir = center.right; // positive x axis / right
    Vector2 centerToTargetDir = (targetPos - centerPos).normalized;
    float facingAmount = Vector2.Dot(centerToTargetDir, lookDir);
    canSeeTarget = facingAmount >= accuracy;
    if (canSeeTarget) return;
    Gizmos.color = canSeeTarget ? Color.red : Color.yellow;
    Gizmos.DrawLine(centerPos, centerPos + lookDir);
  }
}