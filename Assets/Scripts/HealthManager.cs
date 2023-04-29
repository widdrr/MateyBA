using UnityEngine;
public class HealthManager : MonoBehaviour, IOnHitSubscriber
{
    public int maxHealth = 100;

    private int currentHealth;

    private void Start()
    {
        currentHealth = maxHealth;
    }

    public void SubtractHealth(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        // Perform death logic
        Debug.Log("Player died");
    }

    public void OnHit(OnHitPayload payload)
    {
        SubtractHealth(payload.damage);
    }
}