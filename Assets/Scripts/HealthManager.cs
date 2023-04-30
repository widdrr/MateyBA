using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class HealthManager : MonoBehaviour, IOnHitSubscriber
{
    public int maxHealth = 10;
    public int CurrentHealth { get; private set; }

    public UnityEvent deathSequence;

    private void Start()
    {
        CurrentHealth = maxHealth;
    }

    public void OnHit(OnHitPayload payload)
    {
        CurrentHealth -= payload.damage;
        if (CurrentHealth <= 0)
        {
            deathSequence.Invoke();
        }
    }
}