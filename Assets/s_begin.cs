using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class s_begin : MonoBehaviour
{

    public s_player player;
    public e_dialogue dialogue;
    public GameObject mom, trigger2;
    
    void Start()
    {
        StartCoroutine(Begin());
    }

    IEnumerator Begin()
    {
        yield return new WaitForSeconds(1);
        mom.SetActive(true);
        yield return new WaitForSeconds(1);
        dialogue.Talk("motherIntro");
        yield return new WaitForSeconds(5);

    }
}
