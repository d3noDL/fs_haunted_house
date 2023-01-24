using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class S_MAIN : MonoBehaviour
{
    public static S_MAIN i;
    public AudioClip[] musicClip;
    public AudioSource audioSource;
    public s_player player;
    public GameObject[] inv;
    public AudioClip pickup, lockedDoor, keyBreak, bedMove;

    public bool seenGhost = false;
    public bool hasPikovit = false;
    public bool hasKey = false;
    public bool hasBedroomKey = false;
    public bool hasNesController = false;
    public bool hasWrench = false;
    public bool hasMap = false;
    public bool isEntranceBroken = false;
    public bool releaseMother = false;
    public bool releaseTwin = false;
    public bool releaseDemon = false;
    public bool isGhostActive = true;

    private void Awake()
    {
        i = this;
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

        switch (nm)
        {
            case "i_key":
                hasKey = true;
                break;
            case "i_pikovit":
                hasPikovit = true;
                break;
            case "i_keyBedroom":
                hasBedroomKey = true;
                seenGhost = false;
                break;
            case "i_nes":
                hasNesController = true;
                break;
            case "i_wrench":
                hasWrench = true;
                break;
            case "i_map":
                hasMap = true;
                break;
        }
    }

    public void RemoveFromInv(string nm)
    {
        foreach (GameObject item in inv)
        {
            if (item.name == nm)
            {
                item.SetActive(false);
            }
        }

        switch (nm)
        {
            case "i_key":
                hasKey = false;
                break;
            case "i_pikovit":
                hasPikovit = false;
                break;
            case "i_keyBedroom":
                hasBedroomKey = false;
                break;
            case "i_nes":
                hasNesController = false;
                break;
            case "i_wrench":
                hasWrench = false;
                break;
            case "i_map":
                hasMap = false;
                break;
        }
    }
}
