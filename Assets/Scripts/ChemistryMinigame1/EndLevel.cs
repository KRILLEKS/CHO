using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndLevel : MonoBehaviour
{
    public void CountingResult()
    {
        foreach (var c in DatabaseSubstances.Substances)
        {
            for (int i = 1; i <= DatabaseSubstances.instance.numberChoice; i++)
            {
                if (DatabaseSubstances.instance.selectedElements[i].Count == c.elements.Length)
                {
                    int numberMatches = 0;
                    foreach (var s in DatabaseSubstances.instance.selectedElements[i])
                    {
                        if (c.formula.Contains(s.GetComponent<ElementControl>().nameElements) == true)
                        {
                            Debug.Log(numberMatches);
                            numberMatches++;
                        }
                        else continue;
                    }
                    if (numberMatches == c.elements.Length)
                        DatabaseSubstances.instance.number—orrectAnswers++;
                }
                Debug.Log(DatabaseSubstances.instance.number—orrectAnswers);
            }
        }
    }


}
