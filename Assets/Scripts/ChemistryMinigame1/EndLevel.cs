using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

public class EndLevel : MonoBehaviour
{
    static public EndLevel instance = null;
    private void Awake()
    {
        if (instance == null)
            instance = this;
    }
    public async Task CountingResult()
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
                            numberMatches++;
                        }
                        else continue;
                    }
                    if (numberMatches == c.elements.Length)
                        DatabaseSubstances.instance.number—orrectAnswers++;
                }
            }
            await Task.Yield();
        }
    }


}
