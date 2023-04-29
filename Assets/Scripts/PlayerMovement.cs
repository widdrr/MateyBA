using System.Collections;
using UnityEngine;

public enum PlayerState
{
    idle,
    moving,
    attacking,
    staggered,
}

public class PlayerMovement : MonoBehaviour
{
    public float speed;

    private Animator animator;
    private Vector3 change;
    private Rigidbody2D playerRigidbody;
    public PlayerState currentState;

    void Start()
    {
        animator = GetComponent<Animator>();
        playerRigidbody = GetComponent<Rigidbody2D>();

        //We want the player to face the camera by default
        animator.SetFloat("moveX", 0);
        animator.SetFloat("moveY", -1);
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
            change.x = Mathf.Round(change.x);
            change.y = Mathf.Round(change.y);

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

    //Moves the RigidBody according to the change vector
    void MoveCharacter()
    {
        playerRigidbody.MovePosition(
          transform.position + change.normalized * speed * Time.deltaTime
            );
    }
}
