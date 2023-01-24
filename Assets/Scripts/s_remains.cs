using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class s_remains : MonoBehaviour
{
    public GameObject trigger6;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Player")
        {
            trigger6.SetActive(true);
            gameObject.SetActive(false);
        }
    }
}
