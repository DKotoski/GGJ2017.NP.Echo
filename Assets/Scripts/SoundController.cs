using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : MonoBehaviour
{

    public List<AudioClip> Clips;

    AudioSource audioSource;
    // Use this for initialization
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    
    private void Update()
    {

        if (Input.GetAxis("Fire1") == 1)
        {
            PlaySound("LowFreq");
        }
        if (Input.GetAxis("Fire2") == 1)
        {
            PlaySound("HighFreq");
        }
    }

    public void PlaySound(string name)
    {
        switch (name)
        {
            case "HighFreq":
                audioSource.clip = Clips[0];
                break;
            case "LowFreq":
                audioSource.clip = Clips[1];
                break;
            default:
                break;
        }
        audioSource.Play();
    }
}
