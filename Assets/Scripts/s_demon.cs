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

    public string currentSector;
    public bool isVisibleToPlayer;
    public bool isHeartbeat = false;

    private Renderer renderer;

    private void Start()
    {
        
    }

    private void Update()
    {
        transform.LookAt(player.transform);
        Vector3 eulerAngles = transform.rotation.eulerAngles;
        eulerAngles.x = 0;
        eulerAngles.z = 0;

        transform.rotation = Quaternion.Euler(eulerAngles);

        if (GetComponentInChildren<SkinnedMeshRenderer>().isVisible)
        {
            isVisibleToPlayer = true;
            player.GetComponent<s_player>().health += Time.deltaTime / 4;
        }
        else
        {
            isVisibleToPlayer = false;
            player.GetComponent<s_player>().health -= Time.deltaTime / 8;
            
        }
    }

    private void OnBecameVisible()
    {
        player.GetComponent<AudioSource>().PlayOneShot(player.GetComponent<s_player>().heartBeat);
    }

    private void OnBecameInvisible()
    {
        player.GetComponent<AudioSource>().Stop();
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

        transform.position = new Vector3(100, 100, 100);
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
        transform.position = new Vector3(100, 100, 100);
        currentSector = null;
        gameObject.SetActive(false);
        isVisibleToPlayer = false;

    }

    private void OnTriggerStay(Collider other)
    {
        currentSector = other.name;
    }

    private void OnTriggerExit(Collider other)
    {
        currentSector = null;
    }
    
}
