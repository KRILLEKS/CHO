using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;
using System.Threading.Tasks;
using System;

public class OpenEndLevelWindow : MonoBehaviour
{
    [SerializeField]
    GameObject mainEndLevelWindow;
    [SerializeField]
    GameObject levelEndWindow;
    [SerializeField]
    TextMeshProUGUI mainText;
    [SerializeField]
    TextMeshProUGUI numCompletedText;
    [SerializeField]
    TextMeshProUGUI percentCompletedText;

    private float countTrue = 8;
    private float countAllSub = 15;
    private int per = 100;
    private string mainT;
    private string trueT;

    public async void PrintText()
    {
        mainT = "Колличество правильных ответов:";
        trueT = countTrue + " из " + countAllSub;
        Debug.Log(per);
        await WriteTextDelay(mainT, mainText, 2f);
        await WriteTextDelay(trueT, numCompletedText, 2f);
        await IncreasePercentText(percentCompletedText, (int)Math.Round(countTrue / countAllSub * 100), 0.03f, 0.05f, 0.085f, 0.8f);
    }
    private async Task IncreasePercentText(TextMeshProUGUI textMesh, int per, float duration1, float duration2, float duration3, float duration4)
    {
        float i = 0.0f;
        float rate;
        while (i < per)
        {
            if ((int)Math.Round(i) < per / 2)
            {
                rate = 1.0f / duration1;
            }
            else if ((int)Math.Round(i) < per * 3 / 4)
            {
                rate = 1.0f / duration2;
            }
            else if ((int)Math.Round(i) < per - 2)
            {
                rate = 1.0f / duration3;
            }
            else
            {
                rate = 1.0f / duration4;
            }
            i += Time.deltaTime * rate;
            textMesh.text = (int)Math.Round(i) + "%";
            await Task.Yield();
        }
    }
    private async Task WriteTextDelay(string text,TextMeshProUGUI textMesh,float duration)
    {
        float i = 0.0f;
        float rate = 1.0f / duration;
        while (i < text.Length)
        {
            foreach (var c in text)
            {
                i += Time.deltaTime * rate;
                textMesh.text = text.Substring(0, (int)Math.Round(i));        
            }
            await Task.Yield();
        }
    }

    public void ViewEndLevelWindow()
    {
        mainEndLevelWindow.SetActive(true);
        levelEndWindow.transform.DOMove(new Vector3(ConstantsMiniGame1.openEndLevelWindowX, ConstantsMiniGame1.endLevelWindowY, ConstantsMiniGame1.endLevelWindowZ), ConstantsMiniGame1.durationEndLevelWindow, false);
    }

}