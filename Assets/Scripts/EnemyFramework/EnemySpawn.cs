using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    public GenericEnemyController enemyPrefab;
    private GenericEnemyController enemy;
    
    public void spawnEnemy()
    {
        enemy = Instantiate(enemyPrefab);
    }

    public void despawnEnemy()
    {
        if (enemy != null)
        {
            Destroy(enemy);
        }
    }
}
