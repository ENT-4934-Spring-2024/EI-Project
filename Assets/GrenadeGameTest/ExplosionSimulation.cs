using UnityEngine;
using System.Collections.Generic;

public class ExplosionSimulation : MonoBehaviour
{
    public Shader explosionShader; // Shader for explosion particles
    public int numberOfParticles = 100; // Number of particles to emit
    public float explosionForce = 10f; // Force of the explosion applied to particles
    public float explosionRadius = 5f; // Radius of the explosion
    public bool removeObjectOnExplosion = false; // Remove the object when exploded
    public ScaleDecrease particleScaleScript; // Reference to the ScaleDecrease script
    public float falloffFactor = 1f; // Factor controlling the falloff of explosion force

    // Countdown Mode Settings
    public bool countdownMode = true; // Toggle between countdown mode and instant mode
    public float countdownDuration = 3f; // Countdown duration in seconds

    // Instant Mode Settings
    public bool instantMode = false; // Toggle between countdown mode and instant mode

    private bool hasExploded = false;
    private List<GameObject> particles = new List<GameObject>(); // List to store the emitted particles
    private float countdownTimer; // Countdown timer variable

    public ForceFalloff forceFalloffScript; // Reference to the ForceFalloff script

    private void Start()
    {
        countdownTimer = countdownDuration; // Initialize countdown timer
        if (instantMode)
        {
            Explode();
        }
    }

    private void Update()
    {
        if (countdownMode)
        {
            countdownTimer -= Time.deltaTime;
            if (countdownTimer <= 0f && !hasExploded)
            {
                Explode();
            }
        }
    }

    public void TriggerExplosion()
    {
        if (!hasExploded && instantMode)
        {
            Explode();
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
