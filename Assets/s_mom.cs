using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class s_mom : MonoBehaviour
{
    public Material mat;
    
    private void Start()
    {
        StartCoroutine(FadeIn());
    }

    IEnumerator FadeIn()
    {
        for (float i = 0; i < 0.75f; i += Time.deltaTime / 4)
        {
            mat.color = new Color(0, 0, 0, i);
            yield return null;
        }
        
    }
}
