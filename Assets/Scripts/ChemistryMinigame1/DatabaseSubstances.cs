using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;



public class DatabaseSubstances : MonoBehaviour
{
    [SerializeField]
    private Substance[] SubstancesSerializable;
    public static Substance[] Substances;

    static public List<GameObject> elementsPrefab = new List<GameObject>();
    static public Dictionary<int, List<GameObject>> selectedElements = new Dictionary<int, List<GameObject>>();
    static public int numberChoice = 1;
    static public int numberInContainer = -1;

    [Serializable]
    public class Substance
    {
        public string nameSubstance;
        public string formula;
        public Element[] elements;
    }

    private void Awake()
    {
        Substances = SubstancesSerializable;
        selectedElements.Add(numberChoice, new List<GameObject>());
    }

  


}