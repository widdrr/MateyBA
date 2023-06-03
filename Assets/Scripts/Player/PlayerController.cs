using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    public Inventory inventory;
    public ProjectileBehaviour projectilePrefab;
    public Transform launchOffSet;
    public HealthManager healthManager;

    private void Start()
    {
        animator = GetComponent<Animator>();
        playerRigidbody = GetComponent<Rigidbody2D>();
        healthManager = GetComponent<HealthManager>();
        currentState = PlayerState.idle;

        // Set default animation
        animator.SetFloat("moveX", 0);
        animator.SetFloat("moveY", -1);

        inventory.potions = 3;
        inventory.coins = 0;
    }

    private void Update()
    {
        if (Input.GetButtonDown("LeftAttack") && currentState != PlayerState.attacking && inventory.leftWeapon)
        {
            StartCoroutine(LeftAttackSequence());
        }
        else if (Input.GetButtonDown("RightAttack") && currentState != PlayerState.attacking && inventory.rightWeapon)
        {
            StartCoroutine(RightAttackSequence());
        }

        if(Input.GetKeyDown(KeyCode.L)){
            if(healthManager.CurrentHealth < healthManager.maxHealth && inventory.potions > 0){
                inventory.potions--;
                healthManager.RestoreHealth(2);
                }
        }
    }

    private void FixedUpdate()
    {
        var change = Vector3.zero;
        if (currentState != PlayerState.staggered)
        {
            playerRigidbody.velocity = Vector3.zero;
        }

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

    private IEnumerator LeftAttackSequence()
    {
        currentState = PlayerState.attacking;
        animator.SetBool(inventory.leftWeapon.type + "attacking", true);
        yield return null;

        animator.SetBool(inventory.leftWeapon.type + "attacking", false);
        yield return new WaitForSeconds(inventory.leftWeapon.waitingTime);
        currentState = PlayerState.idle;
    }
    private IEnumerator RightAttackSequence()
    {
        currentState = PlayerState.attacking;
        animator.SetBool(inventory.rightWeapon.type + "attacking", true);
        yield return null;

        animator.SetBool(inventory.rightWeapon.type + "attacking", false);
        yield return new WaitForSeconds(inventory.rightWeapon.waitingTime);
        currentState = PlayerState.idle;
    }

    //Instantiate a new projectile 
    public void Shoot()
    {
        float moveX = animator.GetFloat("moveX");
        float moveY = animator.GetFloat("moveY");
        Vector2 shootDirection = moveX != 0 ? new Vector2(moveX, 0) : new Vector2(0, moveY);
        ProjectileBehaviour newBullet = Instantiate(projectilePrefab, launchOffSet.position, Quaternion.identity);
        newBullet.transform.right = shootDirection;
    }
    private void MoveCharacter(Vector3 change)
    {
        playerRigidbody.MovePosition(
            transform.position + speed * Time.deltaTime * change.normalized
        );
    }

    public void OnHit(OnHitPayload payload)
    {
        StartCoroutine(Stagger(0.32f));
    }

    protected IEnumerator Stagger(float seconds)
    {
        currentState = PlayerState.staggered;
        yield return new WaitForSeconds(seconds);
        currentState = PlayerState.idle;
    }

    public void DeathSequence()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
