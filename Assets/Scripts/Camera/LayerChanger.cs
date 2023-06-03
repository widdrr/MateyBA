using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M : MonoBehaviour
{
    private Transform targetPlayer;


    public void Start()
    {
        targetPlayer = GameObject.FindWithTag("Player").transform;
    }
    protected new void FixedUpdate()
    {
        if (targetPlayer.position.y - 0.5 > transform.position.y)
        {
            GetComponent<SpriteRenderer>().sortingOrder = 5;
        }
        else
        {
            GetComponent<SpriteRenderer>().sortingOrder = 3;
        }
    }
}
