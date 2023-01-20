using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class e_piano : MonoBehaviour
{

    private AudioSource audio;
    private bool isPlaying;
    public AudioClip tune;
    
    void Start()
    {
        audio = GetComponent<AudioSource>();
    }

    public void Play()
    {
        if (!audio.isPlaying)
        {
            audio.PlayOneShot(tune);
        }
        
    }
}
