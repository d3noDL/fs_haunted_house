using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class s_motherrelease : MonoBehaviour
{

    public GameObject mom;
    public e_breaker breaker;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Player")
        {
            S_MAIN.i.releaseMother = true;
            S_MAIN.i.StopMusic();
            mom.SetActive(false);
            gameObject.SetActive(false);
            if (breaker.isOn)
            {
                breaker.ToggleBreaker();
            }
            
        }
    }
}
