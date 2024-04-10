using UnityEngine;

public class DestructibleObject : MonoBehaviour
{
    public int maxHealth = 100;
    private int currentHealth;
    public bool isInvincible;

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
        
        if (currentHealth <= 0)
        {
            DestroyObject();
        }
    }

    void DestroyObject()
    {
        // Perform any destruction effects or cleanup here
        Destroy(gameObject);
    }

     
}
