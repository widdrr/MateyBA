using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyState
{
    idle,
    moving,
    attacking,
    staggered,
    dying
}


public abstract class GenericEnemy : MonoBehaviour
{
    public float speed;
    public int hitPoints;
    public int attackDamage;
    public int dropAmount;
    public int knockbackAmount;
    public string enemyName;
    public EnemyState currentState;

    protected Rigidbody2D enemyRigidbody;
    protected Animator enemyAnimator;
    protected Vector3 movementDirection;
    //Initialization
    protected void Start()
    {
        currentState = EnemyState.idle;
        enemyRigidbody = GetComponent<Rigidbody2D>();
        enemyAnimator = GetComponent<Animator>();
    }

    //Main Enemy Loop: Attack if condition is satisfied, else do IdleBehaviour
    protected void FixedUpdate()
    {
            if (ConditionIsSatisfied())
            {
                AttackSequence();
            }
            else
            {
                IdleBehaviour();
            }

            UpdateAnimation();
    }

    //This method updates the enemy animation
    //A default implementation is provided but it can be overridden
    protected virtual void UpdateAnimation()
    {
        switch (currentState)
        {
            case EnemyState.idle:
                enemyAnimator.SetBool("moving", false);
                break;

            case EnemyState.moving:
                enemyAnimator.SetFloat("moveX", movementDirection.x);
                enemyAnimator.SetFloat("moveY", movementDirection.y);
                enemyAnimator.SetBool("moving", true);
                break;

            case EnemyState.staggered:
                enemyAnimator.SetBool("moving", false);
                break;
            case EnemyState.dying:
                enemyAnimator.SetBool("dying", true);
                break;
        }
    }

    //Each enemy inheriting from Generic Enemy has to implement these methods

    //This method returns true when the enemy can attack,
    //possible checks include proximity, timer, etc
    protected abstract bool ConditionIsSatisfied();

    //This method starts the attack sequence,
    //which could be a melee hit, a ranged attack and more, etc
    protected abstract void AttackSequence();
    
    //This method determines the enemy behaviour when not attacking,
    //such as moving randomly, chasing the player, staying still, etc
    protected abstract void IdleBehaviour();

    protected virtual void DeathSequence()
    {
        this.currentState = EnemyState.dying;
    }

    public virtual void TakeDamage(Vector3 hitDirection, int damage = 0)
    {
        hitPoints -= damage;
        
        currentState = EnemyState.staggered;
        
        movementDirection = Vector3.zero;
        enemyRigidbody.isKinematic = false;
        Vector2 knockbackDireciton = transform.position - hitDirection;
        enemyRigidbody.AddForce(knockbackDireciton.normalized * knockbackAmount, ForceMode2D.Impulse);

        StartCoroutine(StopKnockback(0.3f));

        if(hitPoints <=0)
        {
            DeathSequence();
        }
    }
    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            PlayerHealth target = other.gameObject.GetComponent<PlayerHealth>();
            target.TakeDamage(transform.position, attackDamage);
        }
    }

    protected IEnumerator StopKnockback(float seconds)
    {
        yield return new WaitForSeconds(seconds);

        enemyRigidbody.velocity = Vector3.zero;
        enemyRigidbody.isKinematic = true;
        currentState = EnemyState.idle;
    }

    //band-aid solution until I think of something better
    protected IEnumerator Die(int seconds)
    {
        yield return null;
        
    }
}
