using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class s_bossDemon : MonoBehaviour
{
    public s_player player;
    public BoxCollider controller;
    
    
    public void Release()
    {
        
        S_MAIN.i.StopMusic();
        controller.enabled = false;
        player.hurt = false;
        S_MAIN.i.RemoveFromInv("i_keyBedroom");
        S_MAIN.i.RemoveFromInv("i_nes");
        S_MAIN.i.releaseDemon = true;
    }
}
