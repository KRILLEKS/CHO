using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GenerateTaskButtons : MonoBehaviour
{
    public static Transform[] GenerateTasks(Transform folder, int tasksAmount, Action<int> method2InvokeOnPress)
    {
        KRICore.ChangeMenusAmount(folder, tasksAmount);
      
        // get level rect transforms
        Transform[] rectTransformArray = new Transform[tasksAmount]; // +1 cause we include result tab
        for (int i = 0; i < tasksAmount; i++)
            rectTransformArray[i] = folder.GetChild(i);
        
        // set buttons
        for (int i = 0; i < tasksAmount; i++) // +1 cause we include result tab
        {
            var index = i;
            folder.GetChild(i).GetComponent<Button>().onClick.AddListener(() => method2InvokeOnPress(index));
        }

        return rectTransformArray;
    }
}
