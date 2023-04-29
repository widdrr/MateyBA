using System.Collections;
using UnityEngine;

public enum PlayerState
{
    idle,
    moving,
    attacking,
    staggered,
}

public class PlayerController : MonoBehaviour, IOnHitSubscriber
{
    public float speed;

    private Animator animator;
    private PlayerState currentState;
    private Rigidbody2D playerRigidbody;

    private void Start()
    {
        animator = GetComponent<Animator>();
        playerRigidbody = GetComponent<Rigidbody2D>();
        currentState = PlayerState.idle;

        // Set default animation
        animator.SetFloat("moveX", 0);
        animator.SetFloat("moveY", -1);
    }

    private void Update()
    {
        if (Input.GetButtonDown("Attack") && currentState != PlayerState.attacking)
        {
            StartCoroutine(AttackSequence());
        }
    }

    private void FixedUpdate()
    {
        var change = Vector3.zero;

        // If the user uses a movement key, the value will change accordingly based on the axis. It can have 3 values (1,0,-1).
        change.x = Input.GetAxisRaw("Horizontal");
        change.y = Input.GetAxisRaw("Vertical");

        // Call the function to update the animation.
        if (currentState == PlayerState.idle || currentState == PlayerState.moving)
        {
            UpdateAnimationAndMove(change);
        }
    }

    private void UpdateAnimationAndMove(Vector3 change)
    {
        // We check if we have a movement input, if we do we change the position of the character.
        if (change != Vector3.zero)
        {
            MoveCharacter(change);
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

    private IEnumerator AttackSequence()
    {
        currentState = PlayerState.attacking;
        animator.SetBool("attacking", true);
        yield return null;

        animator.SetBool("attacking", false);
        yield return new WaitForSeconds(0.32f);

        currentState = PlayerState.idle;
    }

    private void MoveCharacter(Vector3 change)
    {
        playerRigidbody.MovePosition(
            transform.position + speed * Time.deltaTime * change.normalized
        );
    }

    public void OnHit(OnHitPayload payload)
    {
        StartCoroutine(Stagger(0.3f));
    }

    protected IEnumerator Stagger(float seconds)
    {
        currentState = PlayerState.staggered;
        yield return new WaitForSeconds(seconds);
        currentState = PlayerState.idle;
    }
}
