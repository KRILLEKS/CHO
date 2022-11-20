using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorSwitchElements : MonoBehaviour
{
    public void SelectColor(GameObject element)
    {
        StartCoroutine(GradualChange(element));
    }
    static public void OriginColor(GameObject element)
    {
        element.GetComponent<Image>().color = Color.white;
    }

    private IEnumerator GradualChange(GameObject element)
    {
        float x=0;
        while(x==1)
        {
            element.GetComponent<Image>().color = new Color(x, x, x, 1);
            x += 0.1f;
            yield return null;
        }

    }
}
