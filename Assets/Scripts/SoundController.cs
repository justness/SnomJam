using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : MonoBehaviour
{
    [SerializeField] float volume = 0.1f;
    [SerializeField] AudioClip[] sounds;
    
    AudioSource src;

    void Start()
    {
        src = gameObject.AddComponent<AudioSource>();
        src.playOnAwake = false;
        src.volume = volume;
    }

    public void PlaySound()
    {
        if (!src.isPlaying)
        {
            src.clip = sounds[Random.Range(0, sounds.Length)];
            src.Play();
        }
    }
}
