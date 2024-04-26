using UnityEngine;

public class WaveMotion : MonoBehaviour
{
    // Parameters
    public float amplitude = 1f;    // Amplitude of the wave
    public float frequency = 1f;    // Frequency of the wave
    public float speed = 1f;        // Speed of the wave motion
    public float verticalOffset = 0f; // Vertical offset of the wave

    // Initial position
    private Vector3 startPos;

    void Start()
    {
        // Save the initial position
        startPos = transform.position;
    }

    void Update()
    {
        // Calculate the new position based on sine wave motion
        float yPos = startPos.y + amplitude * Mathf.Sin(Time.time * speed * frequency) + verticalOffset;
        transform.position = new Vector3(transform.position.x, yPos, transform.position.z);
    }
}
