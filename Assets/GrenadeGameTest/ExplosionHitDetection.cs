using System.Threading;
using UnityEngine;

public class ExplosionHitDetection : MonoBehaviour
{
    public float explosionRadius = 5f;
    public float explosionForce = 100f;
    public int damage = 10;

    public void Explode()
    {
        Vector3 position = this.transform.position;

        // Create a sphere collider temporarily
        Collider[] colliders = Physics.OverlapSphere(position, explosionRadius);

        foreach (Collider hit in colliders)
        {
            // Check if the collided object has the DestructibleObject script
            DestructibleObject destructible = hit.GetComponent<DestructibleObject>();
            if (destructible != null)
            {
                // Apply damage to the destructible object
                //destructible.TakeDamage(damage);

                // Calculate direction from the explosion to the object
                Vector3 direction = hit.transform.position - position;

                // Calculate force to apply based on the distance from the explosion
                float distance = direction.magnitude;
                float force = 1 - (distance / explosionRadius);
                if (force <= 0) continue;

                // Apply force to the object
                hit.GetComponent<Rigidbody>().AddForce(direction.normalized * explosionForce * force);

                if(destructible.isInvincible == false)
                {
                    //damage it
                    // Apply damage to the destructible object
                    destructible.TakeDamage(damage);
                }

                // You can add additional calculations for applying force based on the angle if needed
            }
        }




    }

    void OnTriggerEnter(Collider other)
    {
        // Assuming this collider is the explosion collider
        //Explode(transform.position);
    }
}
