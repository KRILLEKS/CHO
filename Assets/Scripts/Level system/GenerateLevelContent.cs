using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// TODO: add all level instances into array to access them easily
// 
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
      Debug.Log("Awake");
      _content = contentSerializable;
      _levelHandler = FindObjectOfType<LevelHandler>();

      _thinElement = Resources.Load<Transform>($"LevelSelection/Thin element");
      _thickElement = Resources.Load<Transform>($"LevelSelection/Thick element");
      _transactionBot2Top = Resources.Load<Transform>($"LevelSelection/Transaction bot2top");
      _transactionTop2Bot = Resources.Load<Transform>($"LevelSelection/Transaction top2bot");

   }

   public static void GenerateLevels(int amount, int currentSubjectIndex)
   {
      
      // TODO: optimize a bit. destroy only if amount < currentAmount, Instantiate if vice versa
      // we remove all elements cause they can be in incorrect order
      for (int i = 0; i < _content.childCount; i++)
         Destroy(_content.GetChild(i).gameObject);

      bool instantiateThinElement = false;
      InstantiateElement(_thinElement, 1, false);

      for (int i = 1; i < amount; i++)
      {
         GameObject transaction;
         
         if (instantiateThinElement)
         {
            transaction = Instantiate(_transactionTop2Bot.gameObject, _content);
            InstantiateElement(_thinElement, i + 1, Data.MaxLevels[currentSubjectIndex] < i + 1);
         }
         else
         {
            transaction = Instantiate(_transactionBot2Top.gameObject, _content);
            InstantiateElement(_thickElement, i + 1, Data.MaxLevels[currentSubjectIndex] < i + 1);
         }
         
         SetTransaction(transaction, Data.MaxLevels[currentSubjectIndex] < i);
         
         instantiateThinElement = instantiateThinElement == false;
      }
      
      Debug.Log("Levels were generated");

      void InstantiateElement(Transform elementTransform, int level, bool isLocked)
      {
         var instance = Instantiate(elementTransform.gameObject, _content);
         // we get this Transforms cause we work (at least can work) with them multiple times
         var levelImage = instance.transform.Find($"Level image");
         var levelIsLocked = instance.transform.Find($"LevelIsLocked");
         var percentage = instance.transform.Find($"Percentage");
         var button = instance.transform.Find($"Button").GetComponent<Button>();
         
         instance.transform.Find($"Filler").GetComponent<Image>().fillAmount = 0;
         instance.transform.Find($"Level number").GetComponent<TextMeshProUGUI>().text = level.ToString();
         levelImage.GetComponent<RawImage>().texture = LevelsDatabase.Subjects[currentSubjectIndex].subjectIcon;
         levelIsLocked.GetComponent<RawImage>().texture = LevelsDatabase.Subjects[currentSubjectIndex].lockedTexture;

         levelImage.GetComponent<RectTransform>().sizeDelta = LevelsDatabase.Subjects[currentSubjectIndex].unlockedTextureSize;
         levelIsLocked.GetComponent<RectTransform>().sizeDelta = LevelsDatabase.Subjects[currentSubjectIndex].lockedTextureSize;
         
         // unlocked
         if (isLocked == false)
         {
            levelIsLocked.gameObject.SetActive(false);

            Debug.Log($"subject: {currentSubjectIndex} level: {level} percentage: {Data.Percentages[currentSubjectIndex][level - 1]}");
            if (Data.Percentages[currentSubjectIndex].Length >= level && Data.Percentages[currentSubjectIndex][level - 1] != -1)
               _levelHandler.StartCoroutine(_levelHandler.ChangePercentageOverTime(instance.transform, Data.Percentages[currentSubjectIndex][level - 1]));
            else
               percentage.gameObject.SetActive(false);
         }
         // locked
         else
         {
            levelIsLocked.gameObject.SetActive(true);
            levelImage.gameObject.SetActive(false);
            percentage.gameObject.SetActive(false);
         }

         // set button
         button.onClick.AddListener(() =>
         {
            // prevent starting level that isn't unlocked
            if (Data.MaxLevels[currentSubjectIndex] < level - 1)
               return;
            
            Debug.Log("current subject = " + currentSubjectIndex);
            Debug.Log("current level = " + level);

            LevelsSummarySingleton.CurrentSubjectIndex = currentSubjectIndex;
            LevelsSummarySingleton.CurrentLevel = level;
            TransitionScene.Instance.SwapToScene(LevelsDatabase.Subjects[currentSubjectIndex].sceneNames[level - 1]);
         });
      }

      void SetTransaction(GameObject transactionGO, bool isLocked)
      {
         // locked
         if (isLocked)
         {
            transactionGO.transform.Find("Locked").gameObject.SetActive(true);
            transactionGO.transform.Find("Unlocked").gameObject.SetActive(false);
         }
         // unlocked
         else
         {
            transactionGO.transform.Find("Locked").gameObject.SetActive(false);
            transactionGO.transform.Find("Unlocked").gameObject.SetActive(true);
         }
      }
   }
}