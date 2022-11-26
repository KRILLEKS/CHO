using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class CloseList : MonoBehaviour
{
    [SerializeField]
    GameObject ScrollView;

    [SerializeField]
    GameObject openListButton;


    public void ReturnList()
    {
        ScrollView.transform.DOMove(ConstantsMiniGame1.posCloseList, ConstantsMiniGame1.durationList, false);
        StartCoroutine(DelayButtonView());
    }
    IEnumerator DelayButtonView()
    {
        yield return new WaitForSeconds(ConstantsMiniGame1.durationList);
        openListButton.SetActive(true);

    }
}

