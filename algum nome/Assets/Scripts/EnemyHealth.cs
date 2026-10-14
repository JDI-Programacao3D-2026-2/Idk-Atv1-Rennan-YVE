using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public float maxHealth = 100f;
    private float currentHealth;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentHealth = maxHealth;
        
    }
    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        if (currentHealth <= 0f) ;
        {
            Die();
        }
    }

    // Update is called once per frame
    void Die()
    {
        gameObject.SetActive(false);
        
    }
}
