using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public enum EnemyState
{
    idle,
    moving,
    attacking,
    staggered,
    dying
}


public abstract class GenericEnemyController : MonoBehaviour
{
    public float speed;
    public string enemyName;
    public EnemyState currentState;

    protected Rigidbody2D enemyRigidbody;
    protected Animator enemyAnimator;
    protected Vector3 movementDirection;
    protected Vector3 attackingDirection;
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
                enemyAnimator.SetBool("attacking", false);
                break;

            case EnemyState.moving:
                enemyAnimator.SetFloat("moveX", movementDirection.x);
                enemyAnimator.SetFloat("moveY", movementDirection.y);
                enemyAnimator.SetBool("moving", true);
                enemyAnimator.SetBool("attacking", false);
                break;

            case EnemyState.staggered:
                enemyAnimator.SetBool("moving", false);
                enemyAnimator.SetBool("attacking", false);
                break;
            case EnemyState.dying:
                enemyAnimator.SetBool("dying", true);
                break;
            case EnemyState.attacking:
                enemyAnimator.SetBool("attacking", true);
                enemyAnimator.SetFloat("moveX", attackingDirection.x);
                enemyAnimator.SetFloat("moveY", attackingDirection.y);
                
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

    public virtual void DeathSequence()
    {
        currentState = EnemyState.dying;
    }


    public virtual void OnHit(OnHitPayload payload)
    {
        movementDirection = Vector3.zero;
        StartCoroutine(Stagger(0.32f));
    }

    protected IEnumerator Stagger(float seconds)
    {
        currentState = EnemyState.staggered;
        yield return new WaitForSeconds(seconds);
        currentState = EnemyState.idle;
    }

    protected void WakeUp()
    {
        this.enabled = true;
        SendMessageUpwards("RegisterEnemy");
    }

    protected static bool MovementIsHorizontal(Vector3 direction)
    {
        return Mathf.Abs(Vector3.Dot(direction, Vector3.right))
                >=
               Mathf.Abs(Vector3.Dot(direction, Vector3.up));
    }

}
