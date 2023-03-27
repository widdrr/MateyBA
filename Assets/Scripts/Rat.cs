using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rat : GenericEnemy
{

    private bool changeTime = true;

    //Rats only move and deal contact damage, no active attacking
    protected override bool ConditionIsSatisfied() 
    {
        return false;
    }

    protected override void AttackSequence()
    {
        return;
    }

    //Enemy should pick a direction and move towards it for 3 seconds, then stopping for another 3
    protected override void IdleBehaviour()
    {
        if (changeTime)
        {
            //If we're currently idle, pick a random direction and begin moving
            if (currentState == EnemyState.idle)
            {    
                movementDirection = (new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0)).normalized;
                currentState = EnemyState.moving;
            }
            //If we're currently moving, stop
            else
            {
                movementDirection = Vector3.zero;
                currentState = EnemyState.idle;
            }

            //Perform the next state-movementDirection after 3 seconds
            changeTime = false;
            StartCoroutine(SetTimer(3));
        }

        enemyRigidbody.MovePosition(
          transform.position + movementDirection * speed * Time.deltaTime
            );
    }

    //this coroutine sets changeTime to true after waiting the given seconds
    //by calling this asynchronously I can delay setting the flag.
    protected IEnumerator SetTimer(int seconds)
    {
        yield return new WaitForSeconds(seconds);
        changeTime = true;
    }
}