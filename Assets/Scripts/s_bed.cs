using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class s_bed : MonoBehaviour
{

    public GameObject bed1, bed1moved, wardrobe, wardrobeMoved, bed2moved;
    public s_ui ui;
    public s_player player;

    public void MoveBed()
    {
        gameObject.SetActive(false);
        bed1.SetActive(false);
        bed1moved.SetActive(true);
        wardrobe.SetActive(false);
        wardrobeMoved.SetActive(true);
        bed2moved.SetActive(true);
        GetComponent<BoxCollider>().enabled = false;
        bed2moved.GetComponent<BoxCollider>().enabled = false;
        bed1moved.GetComponent<BoxCollider>().enabled = false;
    }
    
}
