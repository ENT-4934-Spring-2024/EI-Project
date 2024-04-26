using UnityEngine;

public class FallingLeaves : MonoBehaviour
{
    // Parameters
    public float minSpeed = 1f;     // Minimum speed of the leaf
    public float maxSpeed = 3f;     // Maximum speed of the leaf
    public float tumbleSpeed = 50f; // Tumble speed of the leaf
    public float minX = -5f;        // Minimum X position
    public float maxX = 5f;         // Maximum X position
    public float minY = -5f;        // Minimum Y position
    public float maxY = 5f;         // Maximum Y position

    // Initial movement direction and speed
    private Vector3 moveDirection;
    private float moveSpeed;

    void Start()
    {
        // Generate a random movement direction
        moveDirection = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0f).normalized;

        // Randomize the speed within the specified range
        moveSpeed = Random.Range(minSpeed, maxSpeed);
    }

    void Update()
    {
        // Move the object based on the random movement direction and speed
        transform.Translate(moveDirection * moveSpeed * Time.deltaTime);

        // Tumble the leaf
        transform.Rotate(Vector3.forward, tumbleSpeed * Time.deltaTime);

        // If the leaf goes out of bounds, reset its position
        if (transform.position.x < minX || transform.position.x > maxX ||
            transform.position.y < minY || transform.position.y > maxY)
        {
            // Reset position
            transform.position = new Vector3(Random.Range(minX, maxX), Random.Range(minY, maxY), transform.position.z);

            // Generate a new random movement direction and speed
            moveDirection = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0f).normalized;
            moveSpeed = Random.Range(minSpeed, maxSpeed);
        }
    }
}
