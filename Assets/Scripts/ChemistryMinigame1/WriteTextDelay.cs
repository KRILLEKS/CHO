using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WriteTextDelay : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI text;
    public float delay;

    private void Start()
    {
        StartCoroutine(writeSymbolDelay(text,delay));
    }

    public  IEnumerator writeSymbolDelay (TextMeshProUGUI t, float delay)
    {
        string text = t.text;
        int count = 0;
        for( int i=0;i<=text.Length;i++)
        {
            t.text = text.Substring(0, count);
            count++;
            yield return new WaitForSeconds(delay);
        }

    }

    public static IEnumerator writeSymbolDelay(GameObject t, float delay)
    {
        string text = t.GetComponent<TextMeshProUGUI>().text;
        int count = 0;
        for (int i = 0; i <= text.Length; i++)
        {
            t.GetComponent<TextMeshProUGUI>().text = text.Substring(0, count);
            count++;
            yield return new WaitForSeconds(delay);
        }

    }

}
