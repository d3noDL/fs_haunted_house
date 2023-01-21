using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class S_MAIN : MonoBehaviour
{
    public static S_MAIN i;

    public AudioClip[] musicClip;

    private AudioSource audioSource;

    public s_player player;

    public GameObject[] inv;

    public AudioClip pickup;

    private void Awake()
    {
        i = this;

        audioSource = GetComponent<AudioSource>();
    }

    public void PlayMusic(int clip)
    {
        audioSource.clip = musicClip[clip];
        audioSource.Play();
    }

    public void StopMusic()
    {
        StartCoroutine(FadeMusic());
    }

    public void StopMusicI()
    {
        audioSource.Stop();
    }
    

    IEnumerator FadeMusic()
    {
        for (float i = 0.5f; i > -1f; i -= Time.deltaTime / 2)
        {
            audioSource.volume = i;
            yield return null;
        }
        audioSource.Stop();
        audioSource.volume = 0.5f;
    }

    public void InventoryManager(string nm)
    {
        foreach (GameObject item in inv)
        {
            if (item.name == nm)
            {
                item.SetActive(true);
                audioSource.PlayOneShot(pickup);
            }
        }
    }
}
