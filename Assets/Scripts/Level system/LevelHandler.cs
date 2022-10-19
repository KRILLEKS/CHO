using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class LevelHandler : MonoBehaviour
{
   [SerializeField] private RawImage backgroundSerializable;
   [SerializeField] private RawImage extraBackgroundSerializable;
   [SerializeField] private TextMeshProUGUI subjectTMPSerializable;
   [SerializeField] private Transform contentSerializable;

   // private static variables 
   private static Transform _content;
   private static Coroutine _currentCoroutine;
   private static int CurrentSubjectIndex => LevelsDatabase.CurrentSubjectIndex;
   private static RawImage _background;
   private static RawImage _extraBackground;
   private static bool _isChangingMenus = false; // TODO: we can remove this flag
   private static TextMeshProUGUI _subjectTMP;

   private void Awake()
   {
      _content = contentSerializable;
      _background = backgroundSerializable;
      _extraBackground = extraBackgroundSerializable;
      _subjectTMP = subjectTMPSerializable;
      _isChangingMenus = false;
      _extraBackground.gameObject.SetActive(false);
      
      ChangeTextInstantly(CurrentSubjectIndex);
      ChangeLevels();
   }

   // step can be -1 or +1
   // this function for buttons
   public void SwitchLevel(int step)
   {
      if (_isChangingMenus)
         return;
      
      // change current subject 
      ChangeCurrentSubject(step);

      ChangeBackgroundThings(CurrentSubjectIndex);

      // change levels
      ChangeLevels();
   }

   public bool CanLevelSwitch()
   {
      return !_isChangingMenus;
   }
   
   public void ChangeBackgroundThings(int currentSubjectIndex)
   {      
      _isChangingMenus = true;

      // change background
      ChangeBackground(currentSubjectIndex);
      
      // change text
      ChangeText(currentSubjectIndex);
   }


   private void ChangeLevels()
   {
      GenerateLevelContent.GenerateLevels(LevelsDatabase.Subjects[CurrentSubjectIndex].levelsAmount, CurrentSubjectIndex);
      ChangeText(CurrentSubjectIndex);
   }

   private void ChangeCurrentSubject(int step)
   {
      int newIndex = CurrentSubjectIndex;
      if (newIndex + step >= Data.MaxLevels.Length)
         newIndex = 0;
      else if (newIndex + step < 0)
         newIndex = Data.MaxLevels.Length - 1;
      else
         newIndex += step;
      LevelsDatabase.SetSubjectIndex(newIndex);
   }

   private void ChangeTextInstantly(int currentSubjectIndex)
   {
      _subjectTMP.text = LevelsDatabase.Subjects[currentSubjectIndex].name;
   }
   
   private void ChangeText(int currentSubjectIndex)
   {
      _subjectTMP.DOFade(0, Constants.TextSwitchTime / 2)
         .OnComplete(() =>
         {
            ChangeTextInstantly(currentSubjectIndex);
            _subjectTMP.DOFade(1, Constants.TextSwitchTime / 2);
         });
   }

   private void ChangeBackground(int currentSubjectIndex)
   {
      _extraBackground.gameObject.SetActive(true);
      _extraBackground.color = new Color(_extraBackground.color.r, _extraBackground.color.g, _extraBackground.color.b, 0);
      _extraBackground.texture = LevelsDatabase.Subjects[currentSubjectIndex].background;

      _background.DOFade(0, Constants.BackgroundSwitchTime);
      _extraBackground.DOFade(1, Constants.BackgroundSwitchTime)
         .OnComplete(() =>
         {
            _background.texture = _extraBackground.texture;
            _background.color = new Color(_background.color.r, _background.color.g, _background.color.b, 1);
            _extraBackground.gameObject.SetActive(false);
            _isChangingMenus = false;
         });
   }

   public static void UnlockLevel(int level)
   {
      if ((_content.childCount >= (level - 1) * 2 + 1) == false ||
          (Data.MaxLevels[CurrentSubjectIndex] >= level))
         return;

      Data.MaxLevels[CurrentSubjectIndex]++;

      // unlock level
      // fade out locked image
      var LevelIsLockedImage = _content.GetChild((level - 1) * 2).Find("LevelIsLocked");
      LevelIsLockedImage.GetComponent<RawImage>()
                        .DOFade(0, Constants.LevelUnlockTime)
                        .OnComplete(() =>
                        {
                           LevelIsLockedImage.gameObject.SetActive(false);
                        });

      // fade in level image
      var LevelImageTransform = _content.GetChild((level - 1) * 2).Find("Level image");
      LevelImageTransform.gameObject.SetActive(true);
      var LevelRawImage = LevelImageTransform.GetComponent<RawImage>();
      LevelRawImage.color = new Color(LevelRawImage.color.r, LevelRawImage.color.g, LevelRawImage.color.b, 0);
      LevelRawImage.DOFade(1, Constants.LevelUnlockTime);

      // change transaction
      _content.GetChild((level - 1) * 2 - 1).Find("Locked").gameObject.SetActive(false);
      _content.GetChild((level - 1) * 2 - 1).Find("Unlocked").gameObject.SetActive(true);
   }

   // for buttons
   public void StartLevel(int level)
   {
      if (_currentCoroutine != null)
      {
         StopCoroutine(_currentCoroutine);
      }

      FinishLevel(level);
   }

   public void FinishLevel(int level)
   {
      // animation (percentage)
      if (Data.Percentages[CurrentSubjectIndex].Count < level)
         Data.Percentages[CurrentSubjectIndex].Add(Random.Range(50, 100) / 100f);
      else
         Data.Percentages[CurrentSubjectIndex][level - 1] = Random.Range(50, 100) / 100f;

      _currentCoroutine = StartCoroutine(ChangePercentageOverTime(_content.GetChild((level - 1) * 2), Data.Percentages[CurrentSubjectIndex][level - 1]));

      UnlockLevel(level + 1);
   }

   public IEnumerator ChangePercentageOverTime(Transform objTransform, float percentage)
   {
      // get objects
      var image = objTransform.Find($"Filler").GetComponent<Image>();
      var percentageText = objTransform.Find($"Percentage").GetComponent<TextMeshProUGUI>();

      // TODO: work on interpolation
      image.color = new Color(Constants.StartColor.r, Constants.StartColor.g, Constants.StartColor.b);

      image.DOColor(Constants.EndColor, Constants.LevelPercentageChangeTime);

      // percentage text smoothly appear
      percentageText.gameObject.SetActive(true);
      percentageText.alpha = 0;
      percentageText.DOFade(1, Constants.PercentageTextAppearTime).SetEase(Ease.InOutSine);

      // set up
      var startTime = Time.time;
      image.fillAmount = 0;

      // change percentage
      while (Time.time - startTime < Constants.LevelPercentageChangeTime)
      {
         // normalized value
         float x = (Time.time - startTime) / (Constants.LevelPercentageChangeTime);

         // easing (easeOutQuart)
         // we use quaternion cause DOTween doesn't support float tweening
         float value = (1 - Mathf.Pow(1 - x, 4)) * percentage;
         image.fillAmount = value;
         percentageText.text = ((int) (value * 100)).ToString() + "%";

         yield return null;
      }

      _currentCoroutine = null;
   }
}