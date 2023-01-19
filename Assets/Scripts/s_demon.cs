using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class s_demon : MonoBehaviour
{

    public Material mat;
    public GameObject player;
    public Animator anim;

    private void Update()
    {
        transform.LookAt(player.transform);
        Vector3 eulerAngles = transform.rotation.eulerAngles;
        eulerAngles.x = 0;
        eulerAngles.z = 0;

        transform.rotation = Quaternion.Euler(eulerAngles);
    }

    public void Spawn()
    {
        StartCoroutine(Spawner());
    }

    public void Despawn()
    {
        StartCoroutine(Despawner());
    }

    public void LightDespawn()
    {
        StopCoroutine(Spawner());
        StopCoroutine(Despawner());
        StartCoroutine(LightDespawner());
    }

    IEnumerator Spawner()
    {
        for (float i = 0; i < 0.35f; i += Time.deltaTime / 2)
        {
            mat.color = new Color(1, 1, 1, i);
            yield return null;
        }
        yield return new WaitForSeconds(2);
        anim.SetTrigger("point");
        yield return new WaitForSeconds(2);
        Despawn();
    }

    IEnumerator Despawner()
    {
        yield return new WaitForSeconds(2);
        for (float i = 0.35f; i > 0; i -= Time.deltaTime / 2)
        {
            mat.color = new Color(1, 1, 1, i);
            yield return null;
        }
        gameObject.SetActive(false);
    }
    
    IEnumerator LightDespawner()
    {
        anim.SetTrigger("scare");
        yield return new WaitForSeconds(1);
        for (float i = 0.35f; i > 0; i -= Time.deltaTime / 2)
        {
            mat.color = new Color(1, 1, 1, i);
            yield return null;
        }
        gameObject.SetActive(false);
    }
}
