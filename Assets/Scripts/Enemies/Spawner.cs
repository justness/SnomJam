using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject enemyType;
    public int numEnemies = 10;
    float delayLength = 10f;
    public float spawnDelay;

    void Start()
    {
        spawnDelay = delayLength;
    }

    void Update()
    {
        if (spawnDelay <= 0 && numEnemies != 0){
            GameObject newEnemy = Instantiate(enemyType, transform);
            numEnemies--;
            spawnDelay = delayLength;
        }
        spawnDelay -= Time.deltaTime;
    }
}