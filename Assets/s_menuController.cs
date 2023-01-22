using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class s_menuController : MonoBehaviour
{
    public AudioSource audio;
    

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
