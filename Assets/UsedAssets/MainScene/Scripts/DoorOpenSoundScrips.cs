using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpenSoundScrips : MonoBehaviour
{
    AudioSource audio;
    
    public AudioClip AudioClip1;
    public AudioClip AudioClip2;
    void Awake()
    {
        audio = GetComponent<AudioSource>();
    }

    public void OpenSound()
    {
        audio.clip = AudioClip1;
        audio.Play();
    }

    public void CloseSound()
    {
        audio.clip = AudioClip2;
        audio.Play();
    }
}
