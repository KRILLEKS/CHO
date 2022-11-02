using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;


public class IncreaseWindowOnButton : MonoBehaviour
{
    [SerializeField] 
    private CinemachineVirtualCamera backgroundCamera;
    [SerializeField] 
    private Button imageButtonChange;
    [SerializeField]
    GameObject levelEnd;

    public Sprite increaseSprite;
    public Sprite reduceSprite;
    bool isIncreased = false;

    // Start is called before the first frame update
    public void IncreaseWindow()
    {
        if (!isIncreased)
        {
            backgroundCamera.Priority -= 2;
            imageButtonChange.image.sprite = increaseSprite;
            imageButtonChange.image.color = new Color(255, 255, 255, 255);
            isIncreased = true;
        }
        else
        {

            backgroundCamera.Priority += 2;
            imageButtonChange.image.sprite = reduceSprite;
            imageButtonChange.image.color = new Color(255, 255, 255, 165);
            isIncreased = false;
        }
    }
    public void Update()
    {
        if(levelEnd.activeSelf)
        {
            backgroundCamera.Priority += 2;
        }
    }
}
