using System;
using System.Collections;
using UnityEngine;
public class HealthManager : MonoBehaviour, IOnHitSubscriber
{
    public int maxHealth = 10;
    public int CurrentHealth { get; private set; }

    private void Start()
    {
        CurrentHealth = maxHealth;
    }

    public void OnHit(OnHitPayload payload)
    {
        CurrentHealth -= payload.damage;
        if (CurrentHealth <= 0)
        {
            Die();
        }
    }
    private void Die()
    {
        // Perform death logic
        Debug.Log("Player died");
    }
}