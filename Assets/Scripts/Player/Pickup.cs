using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Pickup : MonoBehaviour
{
    public int price;
    public Inventory inventory;
    
    //Method to implement for any class extending pickup
    public abstract void OnPickup(GameObject picker);

    //The pickup mechanic itself
    //Non-shop items should have price = 0 and no price display
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
