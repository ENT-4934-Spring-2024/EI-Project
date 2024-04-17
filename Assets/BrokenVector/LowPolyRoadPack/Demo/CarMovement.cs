using UnityEngine;

public class CarMovement : MonoBehaviour
{
    public float speed = 5f; // Adjust this to set the speed of the car
    public float turnSpeed = 20f; // Adjust this to set the turning speed of the car
    public float acceleration = 2f;
    private float currentSpeed = 0f;

    // Update is called once per frame
    void Update()
    {

        float verticalInput = Input.GetAxis("Vertical");

        // Apply acceleration
        currentSpeed += verticalInput * acceleration * Time.deltaTime;
        currentSpeed = Mathf.Clamp(currentSpeed, 0f, speed);

        // Move the car forward based on its local forward direction and current speed
        transform.Translate(Vector3.forward * currentSpeed * Time.deltaTime);
    

        // Get user input for steering
        float horizontalInput = Input.GetAxis("Horizontal");
        // Rotate the car based on the horizontal input
        transform.Rotate(Vector3.up, horizontalInput * turnSpeed * Time.deltaTime);
    }
}
