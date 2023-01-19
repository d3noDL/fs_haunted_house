using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class s_footsteps : MonoBehaviour
{
    public AudioClip[] f_wood, f_carpet, f_tile;
    public AudioSource audioSource;
    
    public void Footsteps(int type)
    {
        switch (type)
        {
            case 0:
                audioSource.PlayOneShot(f_wood[Random.Range(0, f_wood.Length)]);
                break;
            case 1:
                audioSource.PlayOneShot(f_carpet[Random.Range(0, f_carpet.Length)]);
                break;
            case 2:
                audioSource.PlayOneShot(f_tile[Random.Range(0, f_tile.Length)]);
                break;
        }
    }
}
