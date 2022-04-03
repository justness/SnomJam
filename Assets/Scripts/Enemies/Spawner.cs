using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject enemyType;
    public int maxNumEnemies = 10;
    float delayLength = 5f;
    public float spawnDelay;

    EnemyManager manager;
    [HideInInspector] public bool canSpawn;
    [HideInInspector] public int numSpawned;

    void Start()
    {
        spawnDelay = delayLength;
        manager = GetComponent<EnemyManager>();
    }

    void Update()
    {
        if (canSpawn && spawnDelay <= 0 && numSpawned < maxNumEnemies) {
            Spawn();
        }
        
        spawnDelay -= Time.deltaTime;
    }

    public void Spawn()
    {
        GameObject newEnemy = Instantiate(enemyType, transform);
        manager.NewSpawn(newEnemy.GetComponent<Enemy>());
        spawnDelay = delayLength;

        numSpawned++;
    }
}