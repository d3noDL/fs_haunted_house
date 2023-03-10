using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using Random = UnityEngine.Random;

public class s_demonSpawner : MonoBehaviour
{
    public GameObject light;
    public GameObject demon;
    public GameObject player;
    public GameObject[] spawnPoints;
    public bool isInSector;
    public BoxCollider box;
    public bool lightIsOn;
    



    private void Update()
    {
        if (light.activeSelf == true && demon.activeSelf == true && demon.GetComponent<s_demon>().currentSector == gameObject.name)
        {
            demon.GetComponent<s_demon>().LightDespawn();
        }
    }


    private void SpawnGhost()
    {

        if (!S_MAIN.i.isGhostActive)
        {
            return;
        }
        if (demon.activeSelf == false && light.activeSelf == false && isInSector == true)
        {
            var spawnX = box.center.x - box.size.x / 2 + Random.Range(0, box.size.x);
            var spawnY = box.center.y - 0.65f;
            var spawnZ = box.center.z - box.size.z / 2 + Random.Range(0, box.size.z);

            Vector3 spawnPosition = new Vector3(spawnX, spawnY, spawnZ);
            
            Debug.Log("Spawning demon on: " + spawnPosition);
            
            demon.transform.position = spawnPosition;
            demon.SetActive(true);
            demon.GetComponent<s_demon>().currentSector = gameObject.name;
            demon.GetComponent<s_demon>().Spawn();

        }
        else
        {
            Debug.Log("Failed to spawn" + "Demon " + demon.activeSelf + "Light " + light.activeSelf + "Is in sector: " + isInSector);
        }

        
        
    }
    
    

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Entered sector");
        if (other.name == "Player")
        {
            isInSector = true;
            InvokeRepeating("SpawnGhost", Random.Range(2, 15), 15);
        }

        if (name == "1" || name == "2" || name == "3" && other.name == "Player")
        {
            GameObject.Find("grandfatherclock").GetComponent<AudioLowPassFilter>().enabled = false;
        }
        
        else
        {
            GameObject.Find("grandfatherclock").GetComponent<AudioLowPassFilter>().enabled = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("Left sector");
        if (other.name == "Player")
        {
            isInSector = false;
            CancelInvoke("SpawnGhost");
            // demon.GetComponent<s_demon>().LightDespawn();
        }
    }

    public void LightOn() => lightIsOn = true;
    public void LightOff() => lightIsOn = false;



}
