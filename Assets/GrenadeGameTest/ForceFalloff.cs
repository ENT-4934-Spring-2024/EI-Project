using UnityEngine;

public class ForceFalloff : MonoBehaviour
{
    public float explosionEnergy = 1000f; // Energy of the explosion (in joules or TNT equivalent)
    public float maxRadius = 10f; // Maximum radius of the explosion
    public float forceModifier = 1f; // Modifier to adjust the force
    public float distanceDecay = 1f; // Distance decay factor

    public void ApplyExplosionForce(Vector3 explosionPosition)
    {
        Collider[] colliders = Physics.OverlapSphere(explosionPosition, maxRadius);
        foreach (Collider collider in colliders)
        {
            Rigidbody rb = collider.GetComponent<Rigidbody>();
            if (rb != null)
            {
                Vector3 explosionDirection = (rb.position - explosionPosition).normalized;
                float distance = Vector3.Distance(rb.position, explosionPosition);
                float normalizedDistance = distance / maxRadius; // Normalize the distance
                float decayedForceModifier = Mathf.Lerp(1f, distanceDecay, normalizedDistance); // Apply decay factor

                float forceMagnitude = explosionEnergy / (distance * distance) * forceModifier * decayedForceModifier;
                Vector3 force = explosionDirection * forceMagnitude;

                rb.AddForce(force, ForceMode.Impulse);
            }
        }
    }
}
