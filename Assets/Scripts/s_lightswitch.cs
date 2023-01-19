using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class s_lightswitch : MonoBehaviour
{
    public GameObject[] lights;
    public GameObject switchLight;
    public bool isOn = false;

    public void Toggle()
    {
        if (isOn)
        {
            foreach (GameObject light in lights)
            {
                light.SetActive(false);
            }
            switchLight.SetActive(true);
            isOn = false;
            GameObject.Find("Breaker").GetComponent<e_breaker>().RemoveLight();
        }
        else
        {
            foreach (GameObject light in lights)
            {
                light.SetActive(true);
            }
            switchLight.SetActive(false);
            isOn = true;
            GameObject.Find("Breaker").GetComponent<e_breaker>().AddLight();
        }
    }
}
