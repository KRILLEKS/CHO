using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class OpenEndLevelWindow : MonoBehaviour
{
    [SerializeField]
    GameObject mainEndLevelWindow;
    [SerializeField]
    GameObject levelEndWindow;


    public void ViewEndLevelWindow()
    {
        mainEndLevelWindow.SetActive(true);
        levelEndWindow.transform.DOMove(new Vector3(ConstantsMiniGame1.openEndLevelWindowX, ConstantsMiniGame1.endLevelWindowY, ConstantsMiniGame1.endLevelWindowZ), ConstantsMiniGame1.durationEndLevelWindow, false);
    }
}
