using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class s_explanation : MonoBehaviour
{
    public GameObject mom;
    public GameObject player;
    public e_dialogue dialogue;

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Player")
        {
            StartCoroutine(Explanation());
        }
    }

    IEnumerator Explanation()
    {
        yield return new WaitForSeconds(1.1f);
        player.GetComponent<s_player>().isActive = false;
        mom.SetActive(true);
        mom.transform.position = new Vector3(-8, 0, -4.3f);
        mom.transform.rotation = Quaternion.identity;
        yield return new WaitForSeconds(1.1f);
        dialogue.Talk("motherFinal");
        gameObject.SetActive(false);

    }
}
