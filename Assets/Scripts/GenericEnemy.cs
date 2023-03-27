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

            default:
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

}
