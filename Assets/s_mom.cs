using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class s_mom : MonoBehaviour
{
    public Material mat;


    private void OnEnable()
    {
        StartCoroutine(FadeIn());
    }

    private void Start()
    {
        StartCoroutine(FadeIn());
    }

    public void Fader()
    {
        StartCoroutine(FadeOut());
    }
    
    
    IEnumerator FadeIn()
    {
        for (float i = 0; i < 0.75f; i += Time.deltaTime / 4)
        {
            mat.color = new Color(0, 0, 0, i);
            yield return null;
        }
        
    }
    
    IEnumerator FadeOut()
    {
        for (float i = 0.75f; i > 0; i -= Time.deltaTime / 4)
        {
            mat.color = new Color(0, 0, 0, i);
            yield return null;
        }
    }
}
