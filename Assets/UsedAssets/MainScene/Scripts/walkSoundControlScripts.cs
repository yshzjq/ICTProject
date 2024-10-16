using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class walkSoundControlScripts : MonoBehaviour
{
    AudioSource walkSound;
    private void Awake()
    {
        walkSound = GetComponent<AudioSource>();
    }
    public void playWalkSound()
    {
        walkSound.Play();
    }
}
