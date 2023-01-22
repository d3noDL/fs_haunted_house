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
    [SerializeField] public GameObject panel, pointer, sectors;
    [SerializeField] private string[] 
        dOpening, dMotherIntro, dMotherFirst, dMotherStayHere, 
        dMotherWarning, dTwinPikovitLost, dTwinPikovitFound, 
        dTwinControllerLost, dTwinControllerFound, 
        itemi_key, itemi_keyBedroom, itemi_pikovit, itemi_wrench, itemi_nes,
        doorLocked, keyBroke, needTool, tooHeavy, bedNotMove,
        remains, motherFinal, twinGiveforDemon, twinDemonReleased, demonGiveController;
    

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
            S_MAIN.i.player.isActive = false;
            panel.SetActive(true);
            pointer.SetActive(false);
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
            case "demonGiveController":
                dialogue = demonGiveController;
                break;
            case "twinDemonReleased":
                dialogue = twinDemonReleased;
                break;
            case "twinGiveForDemon":
                dialogue = twinGiveforDemon;
                break;
            case "remains":
                dialogue = remains;
                break;
            case "motherFinal":
                dialogue = motherFinal;
                break;
            case "bedNotMove":
                dialogue = bedNotMove;
                break;
            case "tooHeavy":
                dialogue = tooHeavy;
                break;
            case "doorLocked":
                dialogue = doorLocked;
                break;
            case "keyBroke":
                dialogue = keyBroke;
                break;
            case "needTool":
                dialogue = needTool;
                break;
            case "itemi_key":
                dialogue = itemi_key;
                break;
            case "itemi_keyBedroom":
                dialogue = itemi_keyBedroom;
                break;
            case "itemi_wrench":
                dialogue = itemi_wrench;
                break;
            case "itemi_pikovit":
                dialogue = itemi_pikovit;
                break;
            case "itemi_nes":
                dialogue = itemi_nes;
                break;
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
            case "motherWarning":
                dialogue = dMotherWarning;
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
