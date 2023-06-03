using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DogController : GenericEnemyController
{
    protected BoxCollider2D verticalCollider;
    protected BoxCollider2D horizontalCollider;
    protected  Transform target;

    public float jumpSpeed;
    public float jumpRadius;
    public float alertRadius;


    protected new void Start()
    {
        base.Start();
        target = GameObject.FindWithTag("Player").transform;
        verticalCollider = transform.GetChild(0).GetComponent<BoxCollider2D>();
        horizontalCollider = transform.GetChild(1).GetComponent<BoxCollider2D>();
    }

    protected override void AttackSequence()
    {
        currentState = EnemyState.attacking;
        StartCoroutine(AttackRoutine());
    }

    protected override bool ConditionIsSatisfied()
    {
        return currentState == EnemyState.moving && 
              (target.transform.position - transform.position).magnitude < jumpRadius;
    }

    protected override void IdleBehaviour()
    {
        if (currentState == EnemyState.idle || currentState == EnemyState.moving)
        {
            movementDirection = (target.transform.position
                                 - transform.position);


            if (movementDirection.magnitude > alertRadius)
                return;
            
            movementDirection = movementDirection.normalized;

            currentState = EnemyState.moving;
            
            enemyRigidbody.MovePosition(
                    transform.position + speed * Time.deltaTime * movementDirection);

            if (MovementIsHorizontal(movementDirection))
            {
                verticalCollider.enabled = false;
                horizontalCollider.enabled = true;
            }
            else
            {
                verticalCollider.enabled = true;
                horizontalCollider.enabled = false;
            }
        }
    }

    public override void OnHit(OnHitPayload payload)
    {
        return;
    }

    protected IEnumerator AttackRoutine()
    {
        attackingDirection = (target.transform.position
                         - transform.position).normalized;
        yield return new WaitForSeconds(0.5f);
        enemyRigidbody.AddForce(
                    GetComponent<Rigidbody2D>().mass * jumpSpeed * attackingDirection, 
                    ForceMode2D.Impulse);
        yield return new WaitForSeconds(0.5f);
        enemyRigidbody.velocity= Vector3.zero;
        StartCoroutine(Stagger(Random.Range(1f,2f)));
    }
}
