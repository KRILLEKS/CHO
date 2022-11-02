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
        
        Save();
    }
    
    // it's mine.
    // Invokes when we exit and when level ends 
    public void Save()
    {
        FindObjectOfType<LevelsSummarySingleton>().SetLevelPercentage((float)Accept.countCorrectAnswer / 5); // 5 it's hard code. 
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
