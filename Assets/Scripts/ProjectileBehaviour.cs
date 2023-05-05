using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBehaviour : MonoBehaviour
{
    public float speed = 10f;

    void Update()
    {
        transform.position += speed * Time.deltaTime * transform.right;
    }
    //destoy object on contact with another object
    public void OnTriggerEnter2D(Collider2D other)
    {
        Destroy(gameObject);
    }
}

