using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Security.Cryptography;
using System.Threading;
using UnityEngine;

public enum PlayerState
{
    idle,
    moving,
    attacking
}


public class PlayerController : MonoBehaviour
{

    public float speed;

    private PlayerState currentState;
    private Rigidbody2D playerRigidbody;
    private Vector3 change;
    private Animator animator;
    void Start()
    {
        animator = GetComponent<Animator>();
        playerRigidbody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (Input.GetButtonDown("Attack") && currentState != PlayerState.attacking)
        {
            StartCoroutine(AttackSequence());
        }
    }
    void FixedUpdate()
    {
        change = Vector3.zero;
        //If the user uses a movement key, the value will change accordingly based on the axis. It can have 3 values (1,0,-1).
        change.x = Input.GetAxisRaw("Horizontal");
        change.y = Input.GetAxisRaw("Vertical");

        //Call the function to update the animation.
        if (currentState == PlayerState.idle || currentState == PlayerState.moving)
        {
            UpdateAnimationAndMove();
        }
    }

    void UpdateAnimationAndMove()
    {
        //We check if we have a movement input, if we do we change the position of the character.
        if (change != Vector3.zero)
        {
            MoveCharacter();
            animator.SetFloat("moveX", change.x);
            animator.SetFloat("moveY", change.y);
            animator.SetBool("moving", true);
            currentState = PlayerState.moving;
        }
        else
        {
            animator.SetBool("moving", false);
            currentState = PlayerState.idle;
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

    void MoveCharacter()
    {
        playerRigidbody.MovePosition(
          transform.position + change.normalized * speed * Time.deltaTime
            );
    }
}
