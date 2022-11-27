using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SpawnSubstances : MonoBehaviour
{
    [SerializeField]
    GameObject contentList;
    [SerializeField]
    GameObject elements;
    [SerializeField]
    TextMeshProUGUI substanceText;

    void Start()
    {
        foreach (var c in DatabaseSubstances.Substances)
        {
            substanceText.text = c.nameSubstance;
            substanceText.transform.name = c.formula;
            Instantiate(substanceText,contentList.transform);
            for (int i = 0; i < c.elements.Length; i++)
            {
               Instantiate(c.elements[i].Prefab,elements.transform);
            }
            
        }
    }


}
