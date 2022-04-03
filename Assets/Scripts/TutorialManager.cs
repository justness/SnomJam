using System;
using System.Collections;
using System.Collections.Generic;
using Febucci.UI;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    public bool isInTutorial = true;
    public Transform tutorialWall;

    [SerializeField] GameObject[] popups;
    int popupNum;

    WaveController waves;
    
    TextAnimator anim;

    void Start()
    {
        waves = FindObjectOfType<WaveController>();
        
        Invoke("ShowPopup", 2);
    }

    void FixedUpdate()
    {
        if (!isInTutorial)
        {
            if (tutorialWall.localPosition.y < 0.25f)
            {
                tutorialWall.localPosition += Vector3.up * 0.001f;
            }
        }
    }

    void ShowPopup()
    {
        if (popupNum < popups.Length)
        {
            popups[popupNum].SetActive(true);
            
            anim = popups[popupNum].GetComponent<TextAnimator>();
            anim.onEvent -= OnEvent;
            anim.onEvent += OnEvent;
        }
        else
        {
            StartGame();
        }
        
        popupNum++;
    }
    
    void OnEvent(string message)
    {
        if (message.Equals("next"))
        {
            Invoke("ShowPopup", 2);
        }
    }
    
    void StartGame()
    {
        isInTutorial = false;
        waves.StartWaves();
    }
}
