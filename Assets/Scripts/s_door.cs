using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class s_door : MonoBehaviour
{
    public GameObject connectedDoor;
    public GameObject spawn;
    public bool isLocked;
    public AudioSource audioSource;
    public AudioClip door_open, door_close, door_locked;

    public void Open()
    {
        audioSource.PlayOneShot(door_open);
    }

    public void Close()
    {
        audioSource.PlayOneShot(door_close);
    }

    public void Locked()
    {
        audioSource.PlayOneShot(door_locked);
    }
}
