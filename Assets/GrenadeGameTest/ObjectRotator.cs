using UnityEngine;

public class ObjectRotator : MonoBehaviour
{
    public float rotationSpeed = 50f; // Speed of rotation
    public Vector3 rotationAxis = Vector3.up; // Axis of rotation
    private bool isRotating = true; // Toggle for rotation

    void Update()
    {
        if (isRotating)
        {
            // Rotate the object continuously
            transform.Rotate(rotationAxis, rotationSpeed * Time.deltaTime);
        }
    }

    // Method to toggle rotation on/off
    public void ToggleRotation()
    {
        isRotating = !isRotating; // Toggle rotation state
    }
}
