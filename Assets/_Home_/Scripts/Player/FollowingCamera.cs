using ExtensionMethods;
using UnityEngine;

public class FollowingCamera : MonoBehaviour
{
    [SerializeField]
    Transform target;
    private Vector2 targetPosition => (Vector2)target.position;

    public float maxTimeToReach = 1f;
    public float defaultSpeed = 10f;

    void FixedUpdate()
    {
        float speedToUse = Mathf.Max(defaultSpeed, Vector2.Distance(transform.position, targetPosition) / maxTimeToReach);
        transform.position = Vector2.MoveTowards(transform.position, targetPosition, speedToUse * Time.fixedDeltaTime).WithZ(transform.position.z);
    }
}
