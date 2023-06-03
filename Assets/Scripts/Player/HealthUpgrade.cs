using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthUpgrade : Pickup
{
    public override void OnPickup(GameObject picker)
    {
        HealthManager manager = picker.GetComponent<HealthManager>();
        manager.maxHealth += 2;
        manager.RestoreHealth(manager.maxHealth);
    }
}
