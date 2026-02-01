using System;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : MonoBehaviour
{
    [SerializeField] AudioSource audioSource;

    [SerializeField] List<AudioClip> sfxList = new();
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void reproduceSfxClip(int numAudio)
    {
        if(!audioSource.isPlaying)
            audioSource.PlayOneShot(sfxList[numAudio]);
    }
}
