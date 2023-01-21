using System;
using System.Collections;
using System.Collections.Generic;
using Language.Lua;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class e_dialogue : MonoBehaviour
{
    [SerializeField] private TMP_Text textObject;
    [SerializeField] public GameObject panel, pointer;
    [SerializeField] private string[] 
        dOpening, dMotherIntro, dMotherFirst, dMotherStayHere, 
        dMotherYouCanLeave, dTwinPikovitLost, dTwinPikovitFound, 
        dTwinControllerLost, dTwinControllerFound;
    

    private string[] dialogue;
    int pos = 0;
    public bool isRunning = false;


    private void Update()
    {
        
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
            case "motherFirst":
                dialogue = dMotherFirst;
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
            case "twinPikovitLost":
                dialogue = dTwinPikovitLost;
                break;
            case "twinPikovitFound":
                dialogue = dTwinPikovitFound;
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
        
        if (Input.GetMouseButton(0))
        {
            isRunning = false;
            pos = 0;
            panel.SetActive(false);
            pointer.SetActive(true);
            S_MAIN.i.player.isActive = true;
            StopAllCoroutines();
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
            StopAllCoroutines();
            yield break;
        }
        

    }
    
   
}
