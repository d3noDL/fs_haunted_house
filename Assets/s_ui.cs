using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class s_ui : MonoBehaviour
{
    public Image img;

    public void Fader(bool fade)
    {
        StartCoroutine(Fade(fade));
    }
    
    public IEnumerator Fade(bool fadeAway)
    {
        if (fadeAway)
        {
            for (float i = 1; i >= 0; i -= Time.deltaTime * 2)
            {
                img.color = new Color(0, 0, 0, i);
                yield return null;
            }
        }
        else
        {
            for (float i = 0; i <= 1; i += Time.deltaTime * 2)
            {
                img.color = new Color(0, 0, 0, i);
                yield return null;
            }
        }
    }
}
