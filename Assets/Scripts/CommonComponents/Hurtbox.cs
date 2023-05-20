using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class Hurtbox : MonoBehaviour
{

    public int attackDamage;
    public List<HitboxType> targets;

    void OnTriggerEnter2D(Collider2D other)
    {
        GameObject target = other.gameObject;
        target.TryGetComponent(out Hitbox hitbox);
        if (hitbox == null)
            return;

        if (targets.Contains(hitbox.type) && !hitbox.Invulnerable)
        {
            OnHitPayload payload = new(attackDamage, transform.position);
            ExecuteEvents.Execute<IOnHitSubscriber>(target, null, (handler, _) => handler.OnHit(payload));
        }
    }
}
