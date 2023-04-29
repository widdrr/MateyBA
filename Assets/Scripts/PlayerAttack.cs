using System.Collections;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private Animator animator;
    private PlayerState currentState;

    void Start()
    {
        animator = GetComponent<Animator>();
        currentState = PlayerState.idle;
    }

    void Update()
    {
        if (Input.GetButtonDown("Attack") && currentState != PlayerState.attacking)
        {
            StartCoroutine(AttackSequence());
        }
    }

    IEnumerator AttackSequence()
    {
        currentState = PlayerState.attacking;
        animator.SetBool("attacking", true);
        yield return null;

        animator.SetBool("attacking", false);
        yield return new WaitForSeconds(0.3f);

        currentState = PlayerState.idle;
    }
}
