using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class DestructibleObject : MonoBehaviour


{
    public int maxHealth = 100;
    private int currentHealth;
    public bool isInvincible;

    public bool explodeOnDestroy;

    public SimpleExplosion simpleExplosion;
    


    void Start()
    {
        currentHealth = maxHealth;
    }

    public void SetHealth(int health)
    {
        currentHealth = Mathf.Clamp(health, 0, maxHealth);
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        Debug.Log(currentHealth);
        
        if (currentHealth <= 0)
        {
            DestroyObject();
        }
    }

    void DestroyObject()
    {
        if (explodeOnDestroy)
        {
            simpleExplosion.Explode();
        }

        // Perform any destruction effects or cleanup here
        Destroy(gameObject);
    }

    
}
