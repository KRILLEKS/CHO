using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;

public class DeselectObject : MonoBehaviour
{
    Element element;
    Image image;
   [SerializeField]
   TextMeshProUGUI text;
   [SerializeField]
   GameObject listElements;
    public void Deselect()
    {
        text.text = "";
        RemoveSelection(listElements);

    }
    public static void RemoveSelection(GameObject listElements)
    {
        GenerateListElements.decision = "";
        foreach (var child in listElements.GetComponentsInChildren<Transform>())
            child.GetComponentInChildren<SettingElement>().DeselectElement();
    }
}
