using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blackout : MonoBehaviour
{
    [SerializeField]
    CanvasGroup blackout;
    [SerializeField]
    GameObject canvas;
    // Start is called before the first frame update

    private void Update()
    {
        if (blackout.alpha > 0.01)
        {
            blackout.alpha -= 0.01f;

        }
        else canvas.SetActive(false);
    }

}
