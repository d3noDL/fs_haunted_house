using System;
using System.Collections;
using System.Collections.Generic;
using Language.Lua;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class e_dialogue : MonoBehaviour
{
    [SerializeField] private TMP_Text textObject;
    [SerializeField] private GameObject panel, pointer;
    [SerializeField] private string[] 
        dOpening, dMotherIntro, dMotherStayHere, 
        dMotherYouCanLeave, dMotherDemonFaceLight, 
        dTwinControllerLost, dTwinControllerFound;

    private string[] dialogue;
    int pos = 0;
    private bool isRunning = false;
    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Talk("motherDemonFaceLight");
        }
    }

    public void Talk(string dName)
    {
        if (!isRunning)
        {
            panel.SetActive(true);
            pointer.SetActive(false);
            S_MAIN.i.player.isActive = false;
            pos = 0;
            textObject.text = "";
            StartCoroutine(StartTalking(dName));
            isRunning = true;
        }


    }

    IEnumerator StartTalking(string _dName)
    {
        switch (_dName)
        {
            case "opening":
                dialogue = dOpening;
                break;
            case "motherIntro":
                dialogue = dMotherIntro;
                break;
            case "motherStayHere":
                dialogue = dMotherStayHere;
                break;
            case "motherYouCanLeave":
                dialogue = dMotherYouCanLeave;
                break;
            case "motherDemonFaceLight":
                dialogue = dMotherDemonFaceLight;
                break;
            case "twinControllerLost":
                dialogue = dTwinControllerLost;
                break;
            case "twinControllerFound":
                dialogue = dTwinControllerFound;
                break;
            default:
                break;
        }

        foreach (char c in dialogue[pos])
        {
            textObject.text += c;
            yield return new WaitForSeconds(0.05f);
        }

        yield return new WaitForSeconds(2);
        textObject.text = "";

        if (pos < dialogue.Length-1)
        {
            pos++;
            StartCoroutine(StartTalking(_dName));
        }
        else
        {
            isRunning = false;
            panel.SetActive(false);
            pointer.SetActive(true);
            S_MAIN.i.player.isActive = true;
            yield break;  
        }
        

    }
    
   
}
