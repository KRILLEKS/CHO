using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AcceptButton : MonoBehaviour
{
    static public bool isButtonAccept = false;
    public void ButtonAccept()
    {
        //if (isButtonAccept == false)
        //{
            foreach (var c in DatabaseSubstances.instance.selectedElements[DatabaseSubstances.instance.numberChoice])
            {             
                c.GetComponent<ElementControl>().status = ElementControl.Mode.MoveInContainer;
                c.GetComponent<ElementControl>().PerformStatus(ElementControl.Mode.MoveInContainer);
            }
            DatabaseSubstances.instance.numberChoice++;
            DatabaseSubstances.instance.selectedElements.Add(DatabaseSubstances.instance.numberChoice, new List<GameObject>());
            isButtonAccept = true;
        //}
    }

}
