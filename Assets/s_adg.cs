using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class s_adg : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(WaitForVideo());
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            SceneManager.LoadScene("Menu");
        }
    }

    IEnumerator WaitForVideo()
    {
        yield return new WaitForSeconds(8);
        SceneManager.LoadScene("Menu");
    }
}
