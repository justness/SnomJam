using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveController : MonoBehaviour
{
    public int[] waveCounts = { 5, 10, 15, 20 };
    public int enemiesToGo = 5;

    int waveNum;
    Spawner spawn;

    void Start()
    {
        enemiesToGo = waveCounts[0];

        spawn = GetComponent<Spawner>();
        
        SetSpawnerValues(1);
    }

    void Update()
    {
        if (waveNum < waveCounts.Length && enemiesToGo <= 0)
        {
            SetSpawnerValues(waveNum + 1);
        }
    }

    void SetSpawnerValues(int wave)
    {
        waveNum = wave;
        enemiesToGo = waveCounts[wave - 1];
        spawn.numSpawned = 0;
        spawn.maxNumEnemies = waveCounts[wave - 1];
        spawn.canSpawn = true;
    }
}
