using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : GenericEnemyController
{
    public Rigidbody2D targetPlayer;
    bool fireCooldown = true;
    public ProjectileBehaviour projectilePrefab;
    public Transform launchOffSet;

    protected new void Start()
    {
        base.Start();
        StartCoroutine(Helpers.SetTimer(2, resetCooldown));
    }
    protected override void AttackSequence()
    {
        fireCooldown = true;
        currentState = EnemyState.attacking;
        StartCoroutine(Helpers.RepeatWithDelay(10, 0.1f, Fire));
        StartCoroutine(Helpers.SetTimer(1, resetAttacking));
        StartCoroutine(Helpers.SetTimer(5, resetCooldown));
    }

    protected override bool ConditionIsSatisfied()
    {
        return !fireCooldown;
    }

    protected override void IdleBehaviour()
    {
        if (currentState == EnemyState.idle || currentState == EnemyState.moving)
        {
            movementDirection = (targetPlayer.transform.position
                                 - transform.position).normalized;
            currentState = EnemyState.moving;
            enemyRigidbody.MovePosition(
                    transform.position + speed * Time.deltaTime * movementDirection);
        }
    }

    protected void Fire()
    {
        Vector3 shootDirection = (targetPlayer.transform.position
                                      - transform.position).normalized;
        launchOffSet.position = transform.position + 2 * shootDirection;
        ProjectileBehaviour newBullet = Instantiate(projectilePrefab, launchOffSet.position, Quaternion.identity);
        newBullet.transform.right = shootDirection;
    }
    protected void resetCooldown()
    {
        fireCooldown = false;
    }
    protected void resetAttacking()
    {
        currentState = EnemyState.idle;
    }

}
