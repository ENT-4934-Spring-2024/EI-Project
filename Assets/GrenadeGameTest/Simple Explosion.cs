using UnityEngine;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using System.Threading;

public class SimpleExplosion : MonoBehaviour
{
    public Shader explosionShader; // Shader for explosion particles
    public int numberOfParticles = 100; // Number of particles to emit
    public float explosionForce = 10f; // Force of the explosion applied to particles
    public bool removeObjectOnExplosion = true; // Remove the object when exploded
    public ScaleDecrease particleScaleScript; // Reference to the ScaleDecrease script


    // Countdown Mode Settings
    public bool countdownMode = true; // Toggle between countdown mode and instant mode
    public float countdownDuration = 3f; // Countdown duration in seconds

    private bool countdownSet = false; // Countdown prep ready

    // Instant Mode Settings
    public bool instantMode = false; // Toggle between countdown mode and instant mode

    private bool hasExploded = false;
    private List<GameObject> particles = new List<GameObject>(); // List to store the emitted particles
    private float countdownTimer; // Internal countdown timer variable


    public void GrenadeStart()
    {
        if (instantMode)
        {
            Explode();
        }
        

        if (countdownDuration > 0 && !countdownMode && !instantMode)
        {
            Debug.LogWarning("Countdown Mode has not been enabled (Automatic Override)");
        }
        countdownTimer = countdownDuration; // Initialize countdown timer
        countdownMode = true;
        countdownSet = true;
    }

    private void Update()
    {
        if (countdownSet)
        {
            countdownTimer -= Time.deltaTime;
            //Debug.Log(countdownTimer.ToString());
            if (countdownTimer <= 0f && !hasExploded)
            {
                Explode();
            }
        }
    }

   

    private void Explode()
    {
        if (!hasExploded)
        {
            // Change material shader to explosion shader
            Renderer renderer = GetComponent<Renderer>();
            if (renderer != null && explosionShader != null)
            {
                Material[] materials = renderer.materials;
                foreach (Material material in materials)
                {
                    material.shader = explosionShader;
                }
            }

            // Emit particles
            EmitParticles();

            // Remove the object if enabled
            if (removeObjectOnExplosion)
            {
                Destroy(gameObject);
            }

            hasExploded = true;
        }
    }

    private void EmitParticles()
    {
        for (int i = 0; i < numberOfParticles; i++)
        {
            Vector3 randomDirection = Random.onUnitSphere;
            GameObject particle = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            particle.transform.position = transform.position;
            particles.Add(particle); // Add particle to the list

            Rigidbody rb = particle.AddComponent<Rigidbody>();
            if (rb != null)
            {
                rb.mass = 1f;
                rb.drag = 0.5f;
                rb.angularDrag = 0.5f;

                // Apply explosion force to the particle
                rb.AddForce(randomDirection * explosionForce, ForceMode.Impulse);
            }

            // Add ScaleDecrease script to the particle
            if (particleScaleScript != null)
            {
                ScaleDecrease scaleDecreaseScript = particle.AddComponent<ScaleDecrease>();
                if (scaleDecreaseScript != null)
                {
                    scaleDecreaseScript.StartScaleDecrease();
                    scaleDecreaseScript.minScale = 0.1f; // Example: Specify minimum scale
                    scaleDecreaseScript.destroyOnMinScale = true; // Example: Destroy particle on reaching minimum scale
                }
            }
        }
    }
}
