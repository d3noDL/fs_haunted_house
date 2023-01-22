using System;
using System.Collections;
using System.Collections.Generic;
using Language.Lua;
using UnityEngine;

public class s_demon : MonoBehaviour
{

    public Material mat;
    public GameObject player;
    public Animator anim;
    public AudioSource audio;
    public AudioClip giggle;
    
    public bool isGhostActive = false;

    public string currentSector;


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
        if (!S_MAIN.i.seenGhost)
        {
            S_MAIN.i.PlayMusic(2);
            S_MAIN.i.seenGhost = true;
        }
        
        audio.PlayOneShot(giggle);
        StartCoroutine(Spawner());
    }

    public void Despawn()
    {
        StartCoroutine(Despawner());
    }

    public void LightDespawn()
    {
        S_MAIN.i.StopMusic();
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
        player.GetComponent<s_player>().hurt = false;
        player.GetComponent<AudioSource>().Stop();
        yield return new WaitForSeconds(2);
        for (float i = 0.35f; i > 0; i -= Time.deltaTime / 2)
        {
            mat.color = new Color(1, 1, 1, i);
            yield return null;
        }

        transform.position = new Vector3(100, 100, 100);
        gameObject.SetActive(false);
        
    }
    
    IEnumerator LightDespawner()
    {
        player.GetComponent<s_player>().hurt = false;
        player.GetComponent<AudioSource>().Stop();
        anim.SetTrigger("scare");
        yield return new WaitForSeconds(1);
        for (float i = 0.35f; i > 0; i -= Time.deltaTime / 2)
        {
            mat.color = new Color(1, 1, 1, i);
            yield return null;
        }
        transform.position = new Vector3(100, 100, 100);
        currentSector = null;
        gameObject.SetActive(false);
        isGhostActive = false;


    }
    
    public void Giggle()
    {
        if (!isGhostActive)
        {
            audio.PlayOneShot(giggle);
            isGhostActive = true;
        }
    }
}
