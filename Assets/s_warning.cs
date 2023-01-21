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
            dialogue.pointer.SetActive(false);
            StartCoroutine(Warning());
        }
    }

    IEnumerator Warning()
    {
        mom.SetActive(false);
        yield return new WaitForSeconds(1);
        mom.SetActive(true);
        mom.transform.position = new Vector3(-4.3f, 0, -10);
        mom.transform.rotation = Quaternion.Euler(0, 90, 0);
        yield return new WaitForSeconds(1);
        dialogue.Talk("motherStayHere");
        yield return new WaitForSeconds(4);
        StartCoroutine(Despawn());
        
    }
    

    IEnumerator Despawn()
    {
        mom.GetComponent<s_mom>().Fader();
        yield return new WaitForSeconds(3);
        mom.SetActive(false);
        trigger1.SetActive(false);
        gameObject.SetActive(false);
        sectors.SetActive(true);
    }
}
