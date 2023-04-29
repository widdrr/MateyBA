using UnityEngine;
using UnityEngine.EventSystems;

public interface IOnHitSubscriber : IEventSystemHandler
{
    void OnHit(OnHitPayload payload);
}

public class OnHitPayload
{
    public int damage;
    public Vector3 position;

    public OnHitPayload(int damage, Vector3 position)
    {
        this.damage = damage;
        this.position = position;
    }
}