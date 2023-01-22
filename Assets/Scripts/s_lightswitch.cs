using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class s_lightswitch : MonoBehaviour
{
    public GameObject[] lights;
    public GameObject switchLight;
    public bool isOn = false;
    public UnityEvent lightOn, lightOff;

    public void Toggle()
    {
        if (isOn)
        {
            foreach (GameObject light in lights)
            {
                light.SetActive(false);
                lightOff.Invoke();
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
                lightOn.Invoke();
            }
            switchLight.SetActive(false);
            isOn = true;
            GameObject.Find("Breaker").GetComponent<e_breaker>().AddLight();
        }
    }
}
