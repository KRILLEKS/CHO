using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RelatedElements : MonoBehaviour
{
    public List<RelElements> relatedElements = new List<RelElements>();
    static public GameObject firstElement;
    static public GameObject secondElement;
    static public GameObject elEffect;
    public class RelElements
    {
        public GameObject firstElement;
        public GameObject secondElement;
        public bool isLink ;
        public GameObject elEffect;
    }


    public void RegisterElement(GameObject firstEl, GameObject secondEl, GameObject elEffect)
    {
        RelElements rel = new RelElements();
        rel.firstElement = firstEl;
        rel.secondElement = secondEl;
        rel.isLink = true;
        rel.elEffect = elEffect;
        relatedElements.Add(rel);
    }

}
