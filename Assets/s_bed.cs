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
        StartCoroutine(Bed());
    }

    IEnumerator Bed()
    {
        ui.Fade(false);
        player.isActive = false;

        yield return new WaitForSeconds(2);
        
        gameObject.SetActive(false);
        bed1.SetActive(false);
        bed1moved.SetActive(true);
        wardrobe.SetActive(false);
        wardrobeMoved.SetActive(true);
        bed2moved.SetActive(true);

        ui.Fade(true);
        
        yield return new WaitForSeconds(2);

        player.isActive = true;
    }
}
