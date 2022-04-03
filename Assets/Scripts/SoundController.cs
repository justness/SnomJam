using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : MonoBehaviour
{
    [SerializeField] AudioClip sound;
    
    AudioSource src;

    void Start()
    {
        src = GetComponent<AudioSource>();

        if (src == null)
        {
            src = gameObject.AddComponent<AudioSource>();
            src.playOnAwake = false;
        }

        src.clip = sound;
    }

    public void PlaySound()
    {
        src.Play();
    }
}
