using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GenerateLevelContent : MonoBehaviour
{
   [SerializeField] private Transform contentSerializable;

   // private static variables
   // Basically we have 4 elements and we can make as much levels as we want with these elements
   // I give each element a separate field to make it more readable although we can use array instead 
   private static Transform _content;
   private static Transform _thinElement;
   private static Transform _transactionBot2Top;
   private static Transform _thickElement;
   private static Transform _transactionTop2Bot;
   private static LevelHandler _levelHandler;

   private void Awake()
   {
      _content = contentSerializable;
      _levelHandler = FindObjectOfType<LevelHandler>();

      _thinElement = Resources.Load<Transform>($"LevelSelection/Thin element");
      _thickElement = Resources.Load<Transform>($"LevelSelection/Thick element");
      _transactionBot2Top = Resources.Load<Transform>($"LevelSelection/Transaction bot2top");
      _transactionTop2Bot = Resources.Load<Transform>($"LevelSelection/Transaction top2bot");
   }

   public static void GenerateLevels(int amount, int currentSubject)
   {
      // TODO: optimize a bit. destroy only if amount < currentAmount, Instantiate if vice versa
      // we remove all elements cause they can be in incorrect order
      for (int i = 0; i < _content.childCount; i++)
         Destroy(_content.GetChild(i).gameObject);

      bool instantiateThinElement = false;
      InstantiateElement(_thinElement, 1, false);

      for (int i = 1; i < amount; i++)
      {
         if (instantiateThinElement)
         {
            Instantiate(_transactionTop2Bot.gameObject, _content);
            InstantiateElement(_thinElement, i + 1, Data.MaxLevels[currentSubject] < i + 2);
         }
         else
         {
            Instantiate(_transactionBot2Top.gameObject, _content);
            InstantiateElement(_thickElement, i + 1, Data.MaxLevels[currentSubject] < i + 2);
         }

         instantiateThinElement = instantiateThinElement == false;
      }

      void InstantiateElement(Transform elementTransform, int level, bool isLocked)
      {
         var instance = Instantiate(elementTransform.gameObject, _content);
         instance.transform.Find($"Filler").GetComponent<Image>().fillAmount = 0;
         instance.transform.Find($"Level number").GetComponent<TextMeshProUGUI>().text = level.ToString();
         instance.transform.Find($"Level image").GetComponent<RawImage>().texture = LevelsDatabase.Subjects[currentSubject].subjectIcon;
         instance.transform.Find($"LevelIsLocked").GetComponent<RawImage>().texture = LevelsDatabase.Subjects[currentSubject].LockedTexture;


         if (isLocked == false)
         {
            if (Data.Percentages[currentSubject].Count >= level && Data.Percentages[currentSubject][level - 1] != -1)
               _levelHandler.StartCoroutine(_levelHandler.ChangePercentageOverTime(instance.transform, Data.Percentages[currentSubject][level - 1]));
            else
               instance.transform.Find($"Percentage").gameObject.SetActive(false);
         }
         else
         {
            instance.transform.Find($"LevelIsLocked").gameObject.SetActive(true);
            instance.transform.Find($"Percentage").gameObject.SetActive(false);
         }

         instance.transform.Find($"Level image").GetComponent<Button>().onClick.AddListener(() => _levelHandler.StartLevel(level));
      }
   }
}