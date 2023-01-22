using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class s_bosscontroller : MonoBehaviour
{

    public s_player player;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Player")
        {
            player.hurt = true;
            S_MAIN.i.PlayMusic(6);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.name == "PLayer")
        {
            player.hurt = false;
            S_MAIN.i.StopMusic();
        }
    }
}
