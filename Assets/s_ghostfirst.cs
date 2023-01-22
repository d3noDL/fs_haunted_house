using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class s_ghostfirst : MonoBehaviour
{

    public GameObject sectors;
    public s_demon demon;
    public s_player player;
    public GameObject cam;
    public s_lightswitch light;
    public GameObject pointer;
    public e_dialogue dialogue;

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Player")
        {
            StartCoroutine(GhostFirst());
        }
    }

    IEnumerator GhostFirst()
    {
        yield return new WaitForSeconds(1.2f);
        demon.gameObject.SetActive(true);
        demon.transform.position = new Vector3(1.5f, 0.65f, -6);
        demon.Spawn();
        player.isActive = false;
        pointer.SetActive(false);
        yield return new WaitForSeconds(1f);
        for (float i = 90; i > 15; i -= Time.deltaTime * 100)
        {
            cam.transform.rotation = Quaternion.Euler(0, i, 0);
            yield return null;
        }
        yield return new WaitForSeconds(4);
        light.Toggle();
        light.GetComponent<AudioSource>().Play();
        demon.LightDespawn();
        yield return new WaitForSeconds(2);
        dialogue.Talk("motherWarning");
        gameObject.SetActive(false);
        sectors.SetActive(true);
    }
}
