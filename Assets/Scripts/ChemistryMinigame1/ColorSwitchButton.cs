using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ColorSwitchButton : MonoBehaviour, IPointerEnterHandler,IPointerExitHandler,IPointerClickHandler
{
    private Image colorBut;
    private Color old;

    public void OnPointerClick(PointerEventData eventData)
    {
        colorBut.color = old;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        colorBut.color = Color.red;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        colorBut.color = old;
    }

    // Start is called before the first frame update
    void Start()
    {
        colorBut = gameObject.GetComponent<Image>();
        old = colorBut.color;
    }
   

}
