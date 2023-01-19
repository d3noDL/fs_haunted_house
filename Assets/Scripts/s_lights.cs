using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class s_lights : MonoBehaviour
{
    public List<GameObject> poweredLights;

    private void Update()
    {
        if (poweredLights.Count >= 6)
        {
            poweredLights.Clear();
        }
    }
}
