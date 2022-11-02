using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;



public class SubstanceManager : MonoBehaviour
{
    [SerializeField]
    private Substance[] SubstancesSerializable;
    public static Substance[] Substances;


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
        Debug.Log(Substances[0].nameSubstance);
    }

    private void Start()
    {
        Debug.Log(Substances[0].nameSubstance);
    }
    public  Substance[] GetSubstances
    {
        get { return Substances; }

    }
    

}


