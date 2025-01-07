using System.Collections;
using UnityEngine;

public class MonsterMove : MonoBehaviour {
    [SerializeField] private float moveSpeed = 2f;  // Speed of the mummy's movement
    [SerializeField] private float rotationSpeed = 45f;  // Degrees per second for rotation
    [SerializeField] private float moveDistance = 30f;  // Distance between points A and B

    private const float TARGET_THRESHOLD = 0.01f; // Threshold to detect when target is reached

    private Vector3 pointA;  // Start position
    private Vector3 pointB;  // Target position
    private Vector3 targetPosition;  // Current target
    private bool movingForward = true;  // Track movement direction

    void Start() {
        pointA = transform.position;
        pointB = pointA + transform.forward * moveDistance;
        targetPosition = pointB;

        StartCoroutine(PatrolBetweenPoints());
    }

    IEnumerator PatrolBetweenPoints() {
        while (true) {
            // Calculate the direction towards the target and normalize it
            Vector3 direction = (targetPosition - transform.position).normalized;

            // Move towards the target position frame-by-frame
            transform.position += direction * moveSpeed * Time.deltaTime;

            // Check if the mummy is close enough to the target position
            if (Vector3.Distance(transform.position, targetPosition) < TARGET_THRESHOLD) {
                // Swap the target position
                targetPosition = movingForward ? pointA : pointB;
                movingForward = !movingForward;

                // Smoothly rotate towards the new target direction
                yield return SlowlyRotate(targetPosition - transform.position);
            }

            yield return null;  // Wait for the next frame
        }
    }

    IEnumerator SlowlyRotate(Vector3 targetDirection) {
        // Calculate the target rotation
        Quaternion targetRotation = Quaternion.LookRotation(targetDirection);

        // Rotate gradually towards the target rotation
        while (Quaternion.Angle(transform.rotation, targetRotation) > TARGET_THRESHOLD) {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            yield return null;  // Wait for the next frame
        }
    }
}
