using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    private HealthManager healthManager;

    void Start()
    {
        healthManager = GameObject.Find("HealthContainers").GetComponent<HealthManager>();
    }

    public virtual void TakeDamage(Vector3 hitDirection, int damage = 0)
    {
        healthManager.SubtractHealth(damage);
    }

}