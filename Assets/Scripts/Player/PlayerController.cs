using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public enum PlayerState
{
    idle,
    moving,
    attacking,
    staggered,
}

public class PlayerController : MonoBehaviour, IOnHitSubscriber
{
    public float speed = 5;

    private Animator animator;
    public PlayerState currentState;
    private Rigidbody2D playerRigidbody;

    public Inventory inventory;
    public SaveManager saveManager;
    public ProjectileBehaviour projectilePrefab;
    public Transform launchOffSet;
    public HealthManager healthManager;
    public AudioSource deathSound;
    public AudioSource gunShotSound;
    public AudioSource crowbarAttackSound;
    public AudioSource healingSound;

    private Vector3 _change;
    public void HandleMovement(InputAction.CallbackContext context)
    {
        var change = context.ReadValue<Vector2>();
        _change.x = change.x;
        _change.y = change.y;
    }

    public void Heal(InputAction.CallbackContext context)
    {
        if (context.started && healthManager.CurrentHealth < healthManager.maxHealth && inventory.potions > 0)
        {
            inventory.potions--;
            healingSound.Play();
            healthManager.RestoreHealth(2);
        }
    }

    public void LeftAttack(InputAction.CallbackContext context)
    {
        if(context.started  &&
           currentState != PlayerState.attacking && 
           currentState != PlayerState.staggered &&
           inventory.leftWeapon)
        {
            LeftAttackSequence();
        }
    }

    public void RightAttack(InputAction.CallbackContext context)
    {
        if (context.started &&
           currentState != PlayerState.attacking &&
           currentState != PlayerState.staggered &&
           inventory.rightWeapon)
        {
            RightAttackSequence();
        }
    }
    private void Awake()
    {
        animator = GetComponent<Animator>();
        playerRigidbody = GetComponent<Rigidbody2D>();
        healthManager = GetComponent<HealthManager>();
        currentState = PlayerState.idle;
        gunShotSound.time = 0.3f;
        deathSound.time = 0.6f;
        // Set default animation
        animator.SetFloat("moveX", 0);
        animator.SetFloat("moveY", -1);
        transform.position = saveManager.state.playerPosition;
    }

    private void Start()
    {
        healthManager.CurrentHealth = saveManager.state.health;
    }
    private void FixedUpdate()
    {
        //Forcefully stops physics pushing
        if (currentState != PlayerState.staggered)
        {
            playerRigidbody.velocity = Vector3.zero;
        }

        // Call the function to update the animation.
        if (currentState == PlayerState.idle || currentState == PlayerState.moving)
        {
            UpdateAnimationAndMove(_change);
        }
    }

    public void UpdateAnimationAndMove(Vector3 change)
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

    //Activates the LeftWeapon
    private void LeftAttackSequence()
    {
        animator.speed = inventory.leftWeapon.attackSpeed;
        currentState = PlayerState.attacking;
        animator.SetBool(inventory.leftWeapon.type + "attacking", true);
    }

    //Activates the RightWeapon
    private void RightAttackSequence()
    {
        animator.speed = inventory.rightWeapon.attackSpeed;
        currentState = PlayerState.attacking;
        animator.SetBool(inventory.rightWeapon.type + "attacking", true);
    }

    public void ResetAttacking()
    {
        animator.speed = 1;
        animator.SetBool(inventory.leftWeapon.type + "attacking", false);
        animator.SetBool(inventory.rightWeapon.type + "attacking", false);
        currentState = PlayerState.idle;
    }

    //Instantiate a new projectile 
    public void Shoot()
    {
        float moveX = animator.GetFloat("moveX");
        float moveY = animator.GetFloat("moveY");
        Vector2 shootDirection = moveX != 0 ? new Vector2(moveX, 0) : new Vector2(0, moveY);
        gunShotSound.Play();
        ProjectileBehaviour newBullet = Instantiate(projectilePrefab, launchOffSet.position, Quaternion.identity);
        newBullet.transform.right = shootDirection;
    }
    private void MoveCharacter(Vector3 change)
    {
        playerRigidbody.MovePosition(
            transform.position + speed * Time.deltaTime * change.normalized
        );
    }

    //Stagger OnHit handler
    public void OnHit(OnHitPayload payload)
    {
        deathSound.Play();
        StartCoroutine(Stagger(0.32f));
    }

    private void PlayCrowbarSound()
    {
        crowbarAttackSound.Play();
    }
    
    protected IEnumerator Stagger(float seconds)
    {
        currentState = PlayerState.staggered;
        yield return new WaitForSeconds(seconds);
        currentState = PlayerState.idle;
    }
    //Upon Death, exit to Main Menu
    public void DeathSequence()
    {
        var death = GameObject.FindWithTag("MateyDeath");
        var deathMatey = death.GetComponent<AudioSource>();
        deathMatey.Play();
        deathSound.Stop();
        StartCoroutine(Helpers.SetTimer(0.46f, () =>
        {
            SceneManager.LoadScene("RestartScreen");
        }));
    }
}
