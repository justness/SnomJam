using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    public bool isInTutorial = true;
    public Transform tutorialWall;

    WaveController waves;
    bool hasStartedGame;

    void Start()
    {
        isInTutorial = false;
        
        waves = FindObjectOfType<WaveController>();
    }

    void FixedUpdate()
    {
        if (!hasStartedGame)
        {
            if (Time.time >= 5)
            {
                StartGame();
            }
        }
        else
        {
            if (tutorialWall.localPosition.y < 0.25f)
            {
                tutorialWall.localPosition += Vector3.up * 0.001f;
            }
        }
    }

    void StartGame()
    {
        waves.StartWaves();
        hasStartedGame = true;
    }
}
