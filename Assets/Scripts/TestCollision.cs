using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCollision : MonoBehaviour
{
    private SpriteRenderer me;
    // Start is called before the first frame update
    void Start()
    {
        me = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Break()
    {
        if(me.color == Color.white)
        {
            me.color = Color.red;

            //I have a 50% chance to increment my coinNumber to a maximum of 999 when I break an enemy
            if (Random.Range(0, 2) == 1 && CoinTextManager.coinNumber <= 999)
            {
                CoinTextManager.coinNumber++;
            }
        }
        else
        {
            me.color = Color.white;
        }
    }
}
