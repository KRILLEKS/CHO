using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;


public class Accept : MonoBehaviour
{
   [SerializeField]
   TextMeshProUGUI t;
   [SerializeField]
   GameObject listElements;
   [SerializeField]
   GameObject levelEnd;
   [SerializeField]
   GameObject content;
   public static bool isAccept = false;
   bool isLight = false;
   bool isCorrect;
   bool isFind = false;
   public static int countCorrectAnswer = 0;

   // Start is called before the first frame update
   public void CheckSubstance()
   {
      isAccept = true;
      foreach (var c in GenerateListElements.list)
      {
         if (GenerateListElements.decision == c.formula)
         {
            GameObject sub = GameObject.Find(c.formula + "(Clone)");
            sub.GetComponent<TextMeshProUGUI>().text = "<s>" + sub.GetComponent<TextMeshProUGUI>().text + "</s>";
            sub.GetComponent<TextMeshProUGUI>().color = new Color(0.00518f, 0.6522f, 1f, 0.2980f);
            for (int i = 0; i < c.elements.Length; i++)
               Destroy(GameObject.Find(c.elements[i].nameElements + "(Clone)"));
            c.formula = "Delete";
            countCorrectAnswer += 1;
            if (countCorrectAnswer == content.transform.childCount)
            {
               levelEnd.SetActive(true);
            }

            DeselectObject.RemoveSelection(listElements);
            t.text = "Вещество найдено";
            isLight = true;
            isCorrect = true;
            isFind = true;
         }
      }

      if (!isFind)
      {
         t.text = "Неправильно введена формула";
         DeselectObject.RemoveSelection(listElements);
         isLight = true;
         isCorrect = false;
      }
   }

   // Update is called once per frame
   void Update()
   {
      if (isLight)
      {
         StartCoroutine(SwitchColor());
         isLight = false;
      }
   }

   IEnumerator SwitchColor()
   {
      if (isCorrect)
      {
         for (int i = 0; i < 2; i++)
         {
            t.color = new Color(0.1003f, 0.4339f, 0.1396f, 1f);
            yield return new WaitForSeconds(0.5f);
            t.color = new Color(0, 0, 0, 0);
            yield return new WaitForSeconds(0.5f);
         }

         t.text = t.text.Replace("Вещество найдено", "");
         t.color = new Color(255, 255, 255, 255);
         isFind = false;
         isAccept = false;
      }
      else
      {
         for (int i = 0; i < 2; i++)
         {
            t.color = new Color(0.3490f, 0.0642f, 0.0642f, 1f);
            yield return new WaitForSeconds(0.5f);
            t.color = new Color(0, 0, 0, 0);
            yield return new WaitForSeconds(0.5f);
         }

         t.text = t.text.Replace("Неправильно введена формула", "");
         t.color = new Color(255, 255, 255, 255);
         isAccept = false;
      }
   }
}