using System;
using System.Collections;
using System.Collections.Generic;
using Febucci.UI;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class WaveController : MonoBehaviour
{
    public int[] waveCounts = { 5, 10, 15, 20 };
    public int enemiesToGo = 5;

    [SerializeField] TextAnimator waveUI;

    int waveNum;
    Spawner spawn;
    
    TutorialManager tutorial;

    void Start()
    {
        tutorial = FindObjectOfType<TutorialManager>();
        
        enemiesToGo = waveCounts[0];

        spawn = GetComponent<Spawner>();
    }

    void Update()
    {
        if (!tutorial.isInTutorial && enemiesToGo <= 0)
        {
            if (waveNum < waveCounts.Length)
            {
                SetSpawnerValues(waveNum + 1);
            }
            else
            {
                SetSpawnerValues(waveCounts.Length);
            }
        }
    }

    public void StartWaves()
    {
        SetSpawnerValues(1);
    }

    void SetSpawnerValues(int wave)
    {
        waveNum = wave;
        enemiesToGo = waveCounts[wave - 1];
        spawn.numSpawned = 0;
        spawn.maxNumEnemies = waveCounts[wave - 1];
        spawn.canSpawn = true;

        waveUI.SetText("<fade d=3><wave>Wave " + waveNum + "!</>", false);
    }
}
