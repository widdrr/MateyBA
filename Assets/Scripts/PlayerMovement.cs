using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Security.Cryptography;
using System.Threading;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public float speed;
    private Rigidbody2D playerRigidbody;
    //Sprite will be affected by gravity and can be controlled from the script by using forces.
    private Vector3 change;
    private Animator animator;
    void Start()
    {
        animator = GetComponent<Animator>();
        playerRigidbody = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        change = Vector3.zero;
        //If the user uses a movement key, the value will change accordingly based on the axis. It can have 3 values (1,0,-1).
        change.x = Input.GetAxisRaw("Horizontal");
        change.y = Input.GetAxisRaw("Vertical");
        UpdateAnimationAndMove();
        //Call the function to update the animation.
    }

    void UpdateAnimationAndMove()
    {
        if (change != Vector3.zero)
        {
            //We check if we have a movement input, if we do we change the position of the character.
            MoveCharacter();
            animator.SetFloat("moveX", change.x);
            animator.SetFloat("moveY", change.y);
            animator.SetBool("moving", true);
            playerRigidbody.AddRelativeForce(change);
        }
        else
        {
            animator.SetBool("moving", false);
        }

    }

    void MoveCharacter()
    {
        playerRigidbody.MovePosition(
          transform.position + change.normalized * speed * Time.deltaTime
            );
    }
}
