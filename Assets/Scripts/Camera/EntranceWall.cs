using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntranceWall : MonoBehaviour
{
    public void EnableWall()
    {
        GetComponent<BoxCollider2D>().enabled= true;
    }
    public void DisableWall()
    {
        GetComponent<BoxCollider2D>().enabled = false;
    }
}
