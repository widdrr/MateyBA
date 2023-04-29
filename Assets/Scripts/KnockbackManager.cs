using System.Collections;
using UnityEngine;

public class KnockbackManager : MonoBehaviour, IOnHitSubscriber
{
    public float knockbackAmount;

    private Rigidbody2D playerRigidbody;

    private void Start()
    {
        playerRigidbody = GetComponent<Rigidbody2D>();
    }

    public void OnHit(OnHitPayload payload)
    {
        Vector3 hitDirection = transform.position - payload.position;
        Vector2 knockbackDireciton = hitDirection.normalized;
        playerRigidbody.AddForce(knockbackDireciton * knockbackAmount, ForceMode2D.Impulse);

        StartCoroutine(StopKnockback(0.3f));
    }

    private IEnumerator StopKnockback(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        playerRigidbody.velocity = Vector3.zero;
    }
}