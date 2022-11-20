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

    //list который хранит формулу веществ для сравнения формул составленных из элементов  с списком веществ
    static public List<string> substancesFormula= new List<string>();
    static public List<List<string>> selectedElements = new List<List<string>>();

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
    }

  


}