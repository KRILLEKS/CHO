using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Threading.Tasks;
using DG.Tweening;

public class MoveWindow : MonoBehaviour
{
    [SerializeField]
    GameObject button;
    [SerializeField]
    GameObject window;


    public async void OpenWindowAlert()
    {
        MoveObject(window, ConstantsMiniGame1.posOpenAlertWindow,ConstantsMiniGame1.durationAlertWindow);
        await Task.Yield();
        MoveObject(button,ConstantsMiniGame1.posCloseAlertButton,ConstantsMiniGame1.durationAlertWindow);

    }
    public async void CloseWindowAlert()
    {
        MoveObject(window,ConstantsMiniGame1.posCloseAlertWindow,ConstantsMiniGame1.durationAlertWindow);
        await Task.Yield();
        MoveObject(button,ConstantsMiniGame1.posOpenAlertButton,ConstantsMiniGame1.durationAlertWindow);
    } 
    public async void OpenList()
    {
        MoveObject(window, ConstantsMiniGame1.posOpenList,ConstantsMiniGame1.durationAlertWindow);
        await Task.Yield();
        MoveObject(button,ConstantsMiniGame1.posCloseListButton,ConstantsMiniGame1.durationAlertWindow);

    }
    public async void CloseList()
    {
        MoveObject(window,ConstantsMiniGame1.posCloseList,ConstantsMiniGame1.durationAlertWindow);
        await Task.Yield();
        MoveObject(button,ConstantsMiniGame1.posOpenListButton,ConstantsMiniGame1.durationAlertWindow);
    }

    private async void MoveObject(GameObject windowAlert,Vector3 newPos,float duration)
    {
        windowAlert.transform.DOMove(newPos,duration, false);
        await Task.Yield();
    } 

}
