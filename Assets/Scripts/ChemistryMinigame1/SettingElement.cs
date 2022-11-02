using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;
using System.Linq;
using System;
using System.Text.RegularExpressions;

public class SettingElement : MonoBehaviour
{
    public float mass;
    public float initialspeed;
    public bool isSelect = false;
    

    private void OnMouseDown()
    {
        GameObject t= GameObject.Find("EnteredValues");
        if (!Accept.isAccept)
        {
            if (!isSelect)
            {
                GenerateListElements.decision += gameObject.name.Replace("(Clone)", "");
                Regex regex = new Regex(@"\.*^[Clone]\.*");

                //t.GetComponent<TextMeshProUGUI>().text += (this.gameObject.name).Replace("(Clone)", "");
                t.GetComponent<TextMeshProUGUI>().text += CreateFormulaText((this.gameObject.name).Replace("(Clone)", ""));
                SelectElement();
            }
            else
            {
                //t.GetComponent<TextMeshProUGUI>().text = t.GetComponent<TextMeshProUGUI>().text.Replace((this.gameObject.name).Replace("(Clone)", ""), "");
                //DeselectElement();
            }
        }
        
    }
    
    public void DeselectElement()
    {
        this.gameObject.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 255);
        this.gameObject.GetComponent<SettingElement>().isSelect = false;
    }

    public void SelectElement()
    {
        this.gameObject.GetComponent<SpriteRenderer>().color = new Color(65, 0, 12, 255);
        this.gameObject.GetComponent<SettingElement>().isSelect = true;
    }
    public string CreateFormulaText(string decision)
    {
        string numbersFound;
        numbersFound = new string(decision.Where(x => Char.IsDigit(x)).ToArray());
        foreach (var c in numbersFound)
        {
            decision = decision.Replace(c.ToString(), "<size=20>" + c.ToString() + "</size>");
        }

        return decision;

    }
}
