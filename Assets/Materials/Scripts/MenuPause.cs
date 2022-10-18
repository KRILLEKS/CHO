using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuPause : MonoBehaviour
{
    [SerializeField]
    Transform box;
    [SerializeField]
    CanvasGroup Back;
    public float wait = 0;

    public bool isdelay=false; 
    private void OnEnable()
    {

        Back.alpha = 0;
        Back.LeanAlpha(0.8f, 0.5f);

        box.localPosition = new Vector2(-300, Screen.height);
        box.LeanMoveLocalY(0, 0.5f).setEaseOutExpo().delay = 0.1f;
        Invoke("DelayTimeScale", 1);

    }

    private void FixedUpdate()
    {

    }

    public void CloseDialog()
    {
        Back.LeanAlpha(0, 0.5f);
        box.LeanMoveLocalY(Screen.height, 0.5f).setEaseInExpo().setOnComplete(Complete);
    }

    void Complete()
    {
        gameObject.SetActive(false);
    }
    void DelayTimeScale()
    {
                Time.timeScale = 0f;

    }

}
