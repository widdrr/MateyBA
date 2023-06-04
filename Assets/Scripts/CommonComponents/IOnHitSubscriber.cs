using UnityEngine;
using UnityEngine.EventSystems;

public interface IOnHitSubscriber : IEventSystemHandler
{    
    //OnHit message handler to implement
    void OnHit(OnHitPayload payload);
}

//Payload class for usefull hit properties
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