using UnityEngine;

public class NPCCarController : MonoBehaviour
{
    public float moveSpeed = 10f;
    public Transform[] waypoints;
    private int currentWaypointIndex = 0;

    void FixedUpdate()
    {
        // Move towards the current waypoint
        Vector3 targetPosition = waypoints[currentWaypointIndex].position;
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

        // Check if the NPC car has reached the current waypoint
        if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
        {
            // Move to the next waypoint
            currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;
        }
    }
}
