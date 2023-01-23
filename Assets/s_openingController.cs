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
        StartCoroutine(OpeningDialog());
        dialog.SetActive(true);
        audio.Play();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            StartCoroutine(Change());
        }
    }

    IEnumerator OpeningDialog()
    {
        
        
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
            StartCoroutine(OpeningDialog());
        }
        else
        {
            audio.Stop();
            dialog.SetActive(false);
            StartCoroutine(Change());

        }
    }

    IEnumerator Change()
    { 
        audio.PlayOneShot(openDoor);
        SceneManager.LoadScene(scene);
        yield break;
    }
    
    
}
