using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHandler : MonoBehaviour
{
    private int enemies = 0;
    public bool RoomCleared { get; private set; } = true;

    public void WakeUpEnemies()
    {
        BroadcastMessage("WakeUp",null,SendMessageOptions.DontRequireReceiver);
        if(!RoomCleared)
        {
            BroadcastMessage("EnableWall", null, SendMessageOptions.DontRequireReceiver);
        }
    }

    public void RegisterEnemy()
    {
        ++enemies;
        RoomCleared = false;
    }

    public void UnregisterEnemy()
    {
        --enemies;
        if(enemies == 0)
        {
            RoomCleared = true;
            BroadcastMessage("DisableWall",null,SendMessageOptions.DontRequireReceiver);
        }
    }
}
