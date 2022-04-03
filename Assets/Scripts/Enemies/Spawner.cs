using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject enemyType;
    public int maxNumEnemies = 10;
    float delayLength = 5f;
    public float spawnDelay;

    TutorialManager tutorial;
    EnemyManager manager;
    [HideInInspector] public bool canSpawn;
    [HideInInspector] public int numSpawned;

    void Start()
    {
        spawnDelay = delayLength;

        tutorial = FindObjectOfType<TutorialManager>();
        manager = GetComponent<EnemyManager>();
    }

    void Update()
    {
        if (!tutorial.isInTutorial)
        {
            if (canSpawn && spawnDelay <= 0 && numSpawned < maxNumEnemies) {
                Spawn();
            }
        
            spawnDelay -= Time.deltaTime;
        }
    }

    public void Spawn()
    {
        GameObject newEnemy = Instantiate(enemyType, transform);
        newEnemy.transform.localPosition = Vector3.zero;
        manager.NewSpawn(newEnemy.GetComponent<Enemy>());
        spawnDelay = delayLength;

        numSpawned++;
    }
}