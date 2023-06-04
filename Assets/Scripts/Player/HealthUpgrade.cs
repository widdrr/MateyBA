using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthUpgrade : Pickup
{
    public int healthUp;

    //Increases max health and fully heals player
    public override void OnPickup(GameObject picker)
    {
        HealthManager manager = picker.GetComponent<HealthManager>();
        manager.maxHealth += healthUp;
        manager.RestoreHealth(manager.maxHealth - manager.CurrentHealth);
    }
}
