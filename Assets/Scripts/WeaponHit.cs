using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHit : MonoBehaviour
{
    void Start()
    {
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            GenericEnemy target = other.transform.parent.GetComponent<GenericEnemy>();
            target.TakeDamage(transform.position,5);
        }
    }
}
