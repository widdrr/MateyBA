using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Pickup : MonoBehaviour
{
    public int price;
    public Inventory inventory;
    public abstract void OnPickup(GameObject picker);

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (inventory.coins >= price)
        {
            OnPickup(other.gameObject);
            Destroy(gameObject);
            inventory.coins -= price;
        }
    }
}
