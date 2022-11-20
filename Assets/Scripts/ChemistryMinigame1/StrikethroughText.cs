using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

public class StrikethroughText : MonoBehaviour, IPointerClickHandler,IPointerEnterHandler,IPointerExitHandler
{
    private bool isStrikethrough=false;
    public void OnPointerClick(PointerEventData eventData)
    {
        isStrikethrough=Strikethrough(isStrikethrough);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        this.gameObject.GetComponent<TextMeshProUGUI>().color = Color.red;
        Strikeout();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        this.gameObject.GetComponent<TextMeshProUGUI>().color = Color.white;
        if (isStrikethrough == false)
        {
            NotStrikeout();
        }
    }


    private bool Strikethrough(bool isStrikethrough) 
    {
        if (isStrikethrough == false)
        {
            Strikeout();
            isStrikethrough = true;
        }
        else
        {
            NotStrikeout();
            isStrikethrough = false;
        }
        return isStrikethrough;
    }
    private string Strikeout()
    {
        return this.gameObject.GetComponent<TextMeshProUGUI>().text = "<s>" + this.gameObject.GetComponent<TextMeshProUGUI>().text + "</s>";

    }
    private string NotStrikeout()
    {
       return this.gameObject.GetComponent<TextMeshProUGUI>().text = (this.gameObject.GetComponent<TextMeshProUGUI>().text.Replace("<s>", "")).Replace("</s>", "");

    }

}
