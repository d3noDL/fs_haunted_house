using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class s_openingController : MonoBehaviour
{

    public TMP_Text textObject;
    public string[] dialogue;

    private int pos = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(OpeningDialog());
    }

    IEnumerator OpeningDialog()
    {
        foreach (char c in dialogue[pos])
        {
            textObject.text += c;
            yield return new WaitForSeconds(0.05f);
        }
        
        yield return new WaitForSeconds(2);
        textObject.text = "";
        
        if (pos < dialogue.Length-1)
        {
            pos++;
            StartCoroutine(OpeningDialog());
        }
        else
        {
            // Go to game
        }
    }
    
    
}
