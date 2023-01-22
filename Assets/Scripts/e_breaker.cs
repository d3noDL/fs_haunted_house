using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class e_breaker : MonoBehaviour
{
    public int numberOfLightsOn = 0;
    public GameObject[] lights;
    public GameObject[] lightSwitches;
    public GameObject mainLight;
    public bool isOn;
    public GameObject bLight;


    private void Start()
    {
        foreach (GameObject light in lights)
        {
            light.SetActive(false);
                
        }

        foreach (GameObject lightSwitch in lightSwitches)
        {
            if (lightSwitch.GetComponent<s_lightswitch>().isOn == true)
            {
                lightSwitch.GetComponent<s_lightswitch>().Toggle();
            }
        }

        mainLight.SetActive(true);
        isOn = true;
        numberOfLightsOn = 0;
        bLight.SetActive(false);
    }

    private void Update()
    {
        if (numberOfLightsOn >= 5)
        {
            ToggleBreaker();
            numberOfLightsOn = 0;
        }
    }


    public void ToggleBreaker()
    {
        
        GetComponent<AudioSource>().Play();
        if (isOn)
        {
            foreach (GameObject light in lights)
            {
                light.SetActive(false);
                
            }

            foreach (GameObject lightSwitch in lightSwitches)
            {
                if (lightSwitch.GetComponent<s_lightswitch>().isOn == true)
                {
                    lightSwitch.GetComponent<s_lightswitch>().Toggle();
                }
            }

            S_MAIN.i.PlayMusic(3);
            mainLight.SetActive(false);
            isOn = false;
            numberOfLightsOn = 0;
            bLight.SetActive(true);
        }
        else
        {
            foreach (GameObject light in lights)
            {
                light.SetActive(false);
                
            }

            foreach (GameObject lightSwitch in lightSwitches)
            {
                if (lightSwitch.GetComponent<s_lightswitch>().isOn == true)
                {
                    lightSwitch.GetComponent<s_lightswitch>().Toggle();
                }
            }

            S_MAIN.i.StopMusic();
            mainLight.SetActive(true);
            isOn = true;
            numberOfLightsOn = 0;
            bLight.SetActive(false);
        }
    }
    
    public void AddLight()
    {
        numberOfLightsOn++;
    }

    public void RemoveLight()
    {
        numberOfLightsOn--;
    }
}
