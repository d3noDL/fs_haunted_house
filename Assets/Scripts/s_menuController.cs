using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class s_menuController : MonoBehaviour
{
    public AudioSource audio;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }


    public void StartGame()
    {
        audio.Stop();
        SceneManager.LoadScene("Opening");
    }
    
    public void Quit()
    {
        Application.Quit();
    }
    
}
