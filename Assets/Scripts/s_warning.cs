using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class s_warning : MonoBehaviour
{
    public GameObject mom;
    public GameObject player;
    public e_dialogue dialogue;
    public GameObject trigger1,trigger4;
    public GameObject sectors;
    public s_lightswitch lightswitch;

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Player")
        {
            StartCoroutine(Warning());
        }
    }

    IEnumerator Warning()
    {
        S_MAIN.i.StopMusic();
        yield return new WaitForSeconds(1.1f);
        player.GetComponent<s_player>().isActive = false;
        lightswitch.Toggle();
        lightswitch.GetComponent<AudioSource>().Play();
        dialogue.Talk("motherStayHere");
        mom.SetActive(false);
        trigger1.SetActive(false);
        trigger4.SetActive(true);
        gameObject.SetActive(false);
        yield break;

    }
    
}
