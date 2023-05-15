using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : GenericEnemyController
{
    Rigidbody2D targetPlayer;
    bool fireCooldown = false;

    protected new void Start()
    {
        base.Start();

    }
    protected override void AttackSequence()
    {
        fireCooldown = false;
        StartCoroutine(SetTimer(5));
    }

    protected override bool ConditionIsSatisfied()
    {
        return fireCooldown;
    }

    protected override void IdleBehaviour()
    {
        movementDirection = targetPlayer.transform.position - transform.position;
        currentState = EnemyState.moving;
    }

    protected IEnumerator SetTimer(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        fireCooldown = true;
    }


}
