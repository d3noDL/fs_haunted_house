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
    public GameObject trigger1;
    public GameObject sectors;

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Player")
        {
            dialogue.Talk("motherStayHere");
            mom.SetActive(false);
            trigger1.SetActive(false);
            gameObject.SetActive(false);
            sectors.SetActive(true);
        }
    }
    
}
