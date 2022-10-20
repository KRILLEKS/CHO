using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LevelEnd : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform box;
    public CanvasGroup Back;
    [SerializeField]
    TextMeshProUGUI textResult;
    [SerializeField]
    GameObject content;

    private void OnEnable()
    {
        Back.alpha = 0;
        Back.LeanAlpha(1, 0.5f);

        box.localPosition = new Vector2(0, -Screen.height);
        box.LeanMoveLocalY(0, 0.5f).setEaseOutExpo().delay = 0.1f;

        textResult.text += Accept.countCorrectAnswer.ToString() + " из " + content.transform.childCount.ToString();
    }


    public void CloseDialog()
    {
        Back.LeanAlpha(0, 0.5f);
        box.LeanMoveLocalY(-Screen.height, 0.5f).setEaseInExpo().setOnComplete(Complete);
    }

    void Complete()
    {
        gameObject.SetActive(false);
    }

}
