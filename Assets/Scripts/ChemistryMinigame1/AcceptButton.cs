using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AcceptButton : MonoBehaviour
{
    static public bool isButtonAccept = false;
    public void ButtonAccept()
    {
      foreach (var c in DatabaseSubstances.selectedElements[DatabaseSubstances.numberChoice])
      {
        c.GetComponent<ElementControl>().RemoveRotation();      
        StartCoroutine(c.GetComponent<ElementControl>().MoveToContainer(ConstantsMiniGame1.posTo1, ConstantsMiniGame1.posTo2, ConstantsMiniGame1.posTo3, ConstantsMiniGame1.timeTo1, ConstantsMiniGame1.timeTo2, ConstantsMiniGame1.timeTo3));
      }
        foreach (var c in DatabaseSubstances.selectedElements[DatabaseSubstances.numberChoice])
        {
            Debug.Log(c.name);
        }
            DatabaseSubstances.numberChoice++;
        DatabaseSubstances.selectedElements.Add(DatabaseSubstances.numberChoice, new List<GameObject>());
    }

}
