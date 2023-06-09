using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LayerChanger : MonoBehaviour
{
    private Transform targetPlayer;
    public void Start()
    {
        targetPlayer = GameObject.FindWithTag("Player").transform;
    }
    //Controls layer priority so that the object will be behind the player
    //if the player is below it, or above otherwise
    protected void FixedUpdate()
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
