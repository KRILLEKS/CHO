using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class OpenList : MonoBehaviour
{
    [SerializeField]
    GameObject ScrollView;


    public void ViewList()
    {
        gameObject.SetActive(false);
        ScrollView.transform.DOMove(new Vector3(ConstantsMiniGame1.openListX, ConstantsMiniGame1.listY, ConstantsMiniGame1.listZ), ConstantsMiniGame1.durationList, false);
    }
}
