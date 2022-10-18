using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class TimerScript : MonoBehaviour
{
    public float minute;
    public float second;
    [SerializeField]
    TextMeshProUGUI timerText;
    [SerializeField]
    GameObject levelEnd;
    void Start()
    {

        timerText.text = string.Format("{0:d1}:{1:d2}", (int)minute, (int)second);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (second > 0 || minute > 0)
        {
            if (second < 0)
            {
                minute -= 1;
                second = 59;
            }
            second -= Time.deltaTime;

            timerText.text = string.Format("{0:d1}:{1:d2}", (int)minute, (int)Mathf.Round(second));
        }
        else
        {
            levelEnd.SetActive(true);
            timerText.text = string.Format("{0:d1}:{1:d2}", 0, 0);
        }

    }
    public void FreezeTime()
    {
       // Time.timeScale = 0f;
    }
    public void ContinueTime()
    {
        Time.timeScale = 1f;
    }
}
