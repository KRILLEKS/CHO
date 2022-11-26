using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class OpenList : MonoBehaviour
{
    [SerializeField]
    GameObject ScrollView;
    [SerializeField]
    GameObject listOpenButton;


    public void ViewList()
    {
        listOpenButton.SetActive(false);
        ScrollView.transform.DOMove(ConstantsMiniGame1.posOpenList, ConstantsMiniGame1.durationList, false);
    }

}
