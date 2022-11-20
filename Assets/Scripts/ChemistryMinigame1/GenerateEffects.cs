using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateEffects : MonoBehaviour
{
    [SerializeField]
    GameObject electricityEffect;
    public GameObject elec;

    public class elecEffect
    {

    }
    public GameObject GetElectricityEffect
    {
        get { return electricityEffect; }
    }
    public GameObject GetElec
    {
        get { return elec; }
        set { elec = value; }
    }
    public void FollowingElec(GameObject firstEl,GameObject secondEl)
    {
        Vector2 dirBetweenElements;
        dirBetweenElements = firstEl.transform.position - secondEl.transform.position;
        var angle = Mathf.Atan(dirBetweenElements.y / dirBetweenElements.x) * Mathf.Rad2Deg;
        Debug.Log(angle);
    }
}
