using System.Collections;
using UnityEngine;

public class Knockback : MonoBehaviour
{
    public float knockbackAmount = 10f;

    private Rigidbody2D playerRigidbody;
    private PlayerState currentState;

    void Start()
    {
        playerRigidbody = GetComponent<Rigidbody2D>();
        currentState = PlayerState.idle;
    }

    public void ApplyKnockback(Vector3 hitDirection)
    {
        currentState = PlayerState.staggered;

        Vector2 knockbackDirection = transform.position - hitDirection;
        playerRigidbody.AddForce(knockbackDirection.normalized * knockbackAmount, ForceMode2D.Impulse);

        StartCoroutine(StopKnockback(0.3f));
    }

    IEnumerator StopKnockback(float seconds)
    {
        yield return new WaitForSeconds(seconds);

        playerRigidbody.velocity = Vector3.zero;

        currentState = PlayerState.idle;
    }
}