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
        }
        else
        {
            me.color = Color.white;
        }
    }
}
