using UnityEngine;

public enum ComparisonOperator
{
    LessThan,
    LessThanOrEqual,
    GreaterThan,
    GreaterThanOrEqual,
    Equal
}

public class VelocityWatcher : MonoBehaviour
{
    public float targetSpeed = 10f; // The speed at which the trigger should occur
    public MonoBehaviour targetScript; // The script to trigger
    public ComparisonOperator comparisonOperator = ComparisonOperator.GreaterThanOrEqual; // Comparison operator for target speed
    public float triggerDelay = 1f; // Delay before starting to check velocity
    public bool displayVelocityInEditor = true; // Whether to display velocity in the editor

    private Rigidbody rb;
    private float delayTimer = 0f;
    private bool delayComplete = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (rb == null)
        {
            Debug.LogError("VelocityWatcher requires a Rigidbody component attached to the GameObject.");
        }
    }

    private void Update()
    {
        if (displayVelocityInEditor)
        {
            Debug.Log("Current Velocity: " + rb.velocity.magnitude);
        }

        if (!delayComplete)
        {
            delayTimer += Time.deltaTime;
            if (delayTimer >= triggerDelay)
            {
                delayComplete = true;
            }
        }

        if (delayComplete)
        {
            switch (comparisonOperator)
            {
                case ComparisonOperator.LessThan:
                    if (rb.velocity.magnitude < targetSpeed)
                    {
                        TriggerScript();
                    }
                    break;
                case ComparisonOperator.LessThanOrEqual:
                    if (rb.velocity.magnitude <= targetSpeed)
                    {
                        TriggerScript();
                    }
                    break;
                case ComparisonOperator.GreaterThan:
                    if (rb.velocity.magnitude > targetSpeed)
                    {
                        TriggerScript();
                    }
                    break;
                case ComparisonOperator.GreaterThanOrEqual:
                    if (rb.velocity.magnitude >= targetSpeed)
                    {
                        TriggerScript();
                    }
                    break;
                case ComparisonOperator.Equal:
                    if (Mathf.Approximately(rb.velocity.magnitude, targetSpeed))
                    {
                        TriggerScript();
                    }
                    break;
                default:
                    Debug.LogWarning("Invalid comparison operator.");
                    break;
            }
        }
    }

    private void TriggerScript()
    {
        if (targetScript != null)
        {
            targetScript.enabled = true; // Enable the target script
        }
        else
        {
            Debug.LogWarning("VelocityWatcher target script is not assigned.");
        }
    }
}
