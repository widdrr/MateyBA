using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Pickup : MonoBehaviour
{
    public abstract void OnPickup(GameObject picker);

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            OnPickup(other.gameObject);
            Destroy(gameObject);
        }
    }
}
