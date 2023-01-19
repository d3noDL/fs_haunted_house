using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class s_demon : MonoBehaviour
{

    public Texture tex;
    public Material mat;
    
    public void Spawn()
    {
        StartCoroutine(Spawner());
    }

    public void Despawn()
    {
        StartCoroutine(Despawner());
    }

    IEnumerator Spawner()
    {
        for (float i = 0; i < 0.75f; i += Time.deltaTime)
        {
            mat.color = new Color(1, 1, 1, i);
            yield return null;
        }
    }

    IEnumerator Despawner()
    {
        for (float i = 0.75f; i > 0; i -= Time.deltaTime)
        {
            mat.color = new Color(1, 1, 1, i);
            yield return null;
        }
    }
}
