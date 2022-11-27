using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Threading.Tasks;


public class DatabaseSubstances : MonoBehaviour
{
    [SerializeField]
    private Substance[] SubstancesSerializable;
    public static Substance[] Substances;
    public static DatabaseSubstances instance=null;
    public Dictionary<int, List<GameObject>> selectedElements = new Dictionary<int, List<GameObject>>();
    public int numberChoice = 1;
    public int numbersInContainer = -1;
    public int number—orrectAnswers=0;
    static public int resultForTransfer;

    [Serializable]
    public class Substance
    {
        public string nameSubstance;
        public string formula;
        public Element[] elements;
    }

    private void Awake()
    {
        if(instance==null)
            instance = this;
        Substances = SubstancesSerializable;
        selectedElements.Add(numberChoice, new List<GameObject>());
    }
}