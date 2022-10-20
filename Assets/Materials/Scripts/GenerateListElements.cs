using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Security.Cryptography;
using TMPro;

public  class GenerateListElements : MonoBehaviour
{
    [SerializeField]
    GameObject windowList;
    [SerializeField]
    TextMeshProUGUI textElement;
    [SerializeField]
    GameObject gameWindow;
    [SerializeField] 
    Vector2 range;
    



    public Transform spawnPos;
    public static string decision;
    public int countRequiredValues;
    public int[] randomValuesSubstances;
    public static List<SubstanceManager.Substance> list = new List<SubstanceManager.Substance>();
    public float delay;

    void Awake()
    {
        randomValuesSubstances = new int[countRequiredValues];

        randomValuesSubstances = GenerateRandomNumbers(countRequiredValues, SubstanceManager.Substances.Length);

        for (int i = 0; i < randomValuesSubstances.Length; i++)
        {
            list.Add(SubstanceManager.Substances[randomValuesSubstances[i]]);


        }
        foreach (var c in list)
        {
            textElement.text = c.nameSubstance;
            textElement.transform.name = c.formula;
            Instantiate(textElement).transform.SetParent(windowList.transform);
            StartCoroutine(WriteTextDelay.writeSymbolDelay(GameObject.Find(c.formula+"(Clone)"),delay));

            for (int i=0; i<c.elements.Length;i++)
            {
                Vector2 pos = spawnPos.position + new Vector3(0, Random.Range(-range.y, range.x));
                Instantiate(c.elements[i].Prefab,pos,Quaternion.identity).transform.SetParent(gameWindow.transform);

            }    
        }
        Destroy(GameObject.Find("ElementsDataBase"));
    }



    public int[] GenerateRandomNumbers(int countRequiredValues, int countSubstance)
    {
        System.Random rnd = new System.Random();
        int[] randomCountSubstance = new int[countSubstance-1];
        randomCountSubstance[0] = rnd.Next(0, countSubstance );
        for (int i = 1; i < countSubstance-1;)
        {
            int num = rnd.Next(0, countSubstance ); 
            int j;
    
            for (j = 0; j < i; j++)
            {
                if (num == randomCountSubstance[j])
                    break; 
            }
            if (j == i)
            { 
                randomCountSubstance[i] = num; 
                i++;
            }
        }
        int[] randomRequiredValues = new int[countRequiredValues];
        for (int i=0;i< countRequiredValues;i++)
        {
            randomRequiredValues[i] = randomCountSubstance[i];
        }

        return randomRequiredValues;
    }   
}
