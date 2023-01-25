using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class s_openingController : MonoBehaviour
{

    public TMP_Text textObject;
    public string[] dialogue;
    public GameObject dialog;
    public AudioSource audio;
    public AudioClip openDoor;
    public string scene;

    private int pos = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        
        dialog.SetActive(true);
        NextLine();
        
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (textObject.text == dialogue[pos])
            {
                NextLine();
            }
            else
            {
                StopAllCoroutines();
                textObject.text = dialogue[pos];
            }
        }
    }
    
    void StartDialogue()
    {
        pos = 0;
        StartCoroutine(TypeLine());
    }

    IEnumerator TypeLine()
    {
        foreach (char c in dialogue[pos].ToCharArray())
        {
            textObject.text += c;
            yield return new WaitForSeconds(0.05f);
        }
    }

    void NextLine()
    {
        if (pos < dialogue.Length - 1)
        {
            pos++;
            textObject.text = string.Empty;
            StartCoroutine(TypeLine());
        }
        else
        {
            audio.PlayOneShot(openDoor);
            SceneManager.LoadScene(scene);
        }
    }
    
    
    
}
