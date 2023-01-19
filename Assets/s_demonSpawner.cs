using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class s_demonSpawner : MonoBehaviour
{
    public GameObject light;
    public GameObject demon;
    public GameObject player;
    public GameObject[] spawnPoints;
    public bool isInSector;


    private void Start()
    {
        InvokeRepeating("SpawnGhost", 4, 2);
    }

    private void SpawnGhost()
    {
        if (demon.activeSelf == false && light.activeSelf == false && isInSector == true)
        {
            
            var point = Random.Range(0, spawnPoints.Length);
            Debug.Log("Spawning demon on " + point);
            demon.SetActive(true);
            demon.transform.position = spawnPoints[point].transform.position;
            demon.transform.LookAt(player.transform.position);
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
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("Left sector");
        if (other.name == "Player")
        {
            isInSector = false;
        }
    }
}
