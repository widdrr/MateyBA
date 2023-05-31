using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomTransition : MonoBehaviour
{
    public CameraMovement mainCamera;
    public Vector2 maxPosition;
    public Vector2 minPosition;
    public Vector3 moveOffset;
    public EnemyHandler enemyHandler;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            mainCamera.maxPosition = maxPosition;
            mainCamera.minPosition = minPosition;
            collision.transform.position += moveOffset;
            if(enemyHandler != null )
                enemyHandler.WakeUpEnemies();
        }

    }
}
