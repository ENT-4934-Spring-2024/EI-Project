using UnityEngine;

public class ScaleDecrease : MonoBehaviour
{
    public float decreaseRate = 0.1f; // Rate at which the scale decreases
    public float minScale = 0.1f; // Minimum scale to clamp to
    public float duration = 1f; // Duration of the scale decrease
    public bool destroyOnMinScale = true; // Whether to destroy the object when it reaches minScale

    private Vector3 initialScale; // Initial scale of the object
    private bool isDecreasing = false; // Flag to track if scale is currently decreasing
    private float elapsedTime = 0f; // Elapsed time since scale decrease started

    private void Start()
    {
        initialScale = transform.localScale;
    }

    private void Update()
    {
        if (isDecreasing)
        {
            // Calculate the scale decrease factor based on time
            float decreaseFactor = 1f - (elapsedTime / duration);

            // Apply the scale decrease
            Vector3 newScale = initialScale * decreaseFactor;
            transform.localScale = newScale;

            // Clamp the scale to minScale
            if (newScale.magnitude < minScale)
            {
                transform.localScale = Vector3.one * minScale;
                if (destroyOnMinScale)
                {
                    Destroy(gameObject); // Destroy the object if destroyOnMinScale is true
                }
                isDecreasing = false;
            }

            // Update elapsed time
            elapsedTime += Time.deltaTime;

            // Stop decreasing scale if duration has elapsed
            if (elapsedTime >= duration)
            {
                isDecreasing = false;
            }
        }
    }

    public void StartScaleDecrease()
    {
        // Reset elapsed time and start decreasing scale
        elapsedTime = 0f;
        isDecreasing = true;
    }
}
