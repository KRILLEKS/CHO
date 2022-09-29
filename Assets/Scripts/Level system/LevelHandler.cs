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
   private static int _currentSubject;
   private static RawImage _background;
   private static RawImage _extraBackground;
   private static bool _isChangingMenus = false; // TODO: we can remove this flag
   private static TextMeshProUGUI _subjectTMP;

   private void Awake()
   {
      _content = contentSerializable;
      _currentSubject = 0;
      _background = backgroundSerializable;
      _extraBackground = extraBackgroundSerializable;
      _subjectTMP = subjectTMPSerializable;

      _extraBackground.gameObject.SetActive(false);

      GenerateLevelContent.GenerateLevels(LevelsDatabase.Subjects[0].levelsAmount, _currentSubject);
   }

   // step can be -1 or +1
   // this function for buttons
   public void SwitchLevel(int step)
   {
      if (_isChangingMenus)
         return;

      _isChangingMenus = true;

      // change current subject 
      if (_currentSubject + step >= Data.MaxLevels.Length)
         _currentSubject = 0;
      else if (_currentSubject + step < 0)
         _currentSubject = Data.MaxLevels.Length - 1;
      else
         _currentSubject += step;

      // change background
      _extraBackground.gameObject.SetActive(true);
      _extraBackground.color = new Color(_extraBackground.color.r, _extraBackground.color.g, _extraBackground.color.b, 0);
      _extraBackground.texture = LevelsDatabase.Subjects[_currentSubject].background;

      _background.DOFade(0, Constants.BackgroundSwitchTime);
      _extraBackground.DOFade(1, Constants.BackgroundSwitchTime)
                      .OnComplete(() =>
                      {
                         _background.texture = _extraBackground.texture;
                         _background.color = new Color(_background.color.r, _background.color.g, _background.color.b, 1);
                         _extraBackground.gameObject.SetActive(false);
                         _isChangingMenus = false;
                      });

      // change text
      _subjectTMP.DOFade(0, Constants.TextSwitchTime / 2)
                 .OnComplete(() =>
                 {
                    _subjectTMP.text = LevelsDatabase.Subjects[_currentSubject].name;
                    _subjectTMP.DOFade(1, Constants.TextSwitchTime / 2);
                 });

      // change levels
      GenerateLevelContent.GenerateLevels(LevelsDatabase.Subjects[_currentSubject].levelsAmount, _currentSubject);
   }

   public static void UnlockLevel(int level)
   {
      if ((_content.childCount >= (level - 1) * 2 + 1) == false ||
          (Data.MaxLevels[_currentSubject] >= level))
         return;

      Data.MaxLevels[_currentSubject]++;

      var LevelIsLockedImage = _content.GetChild((level - 1) * 2).Find("LevelIsLocked");

      LevelIsLockedImage.GetComponent<RawImage>()
                        .DOFade(0, Constants.LevelUnlockTime)
                        .OnComplete(() =>
                        {
                           LevelIsLockedImage.gameObject.SetActive(false);
                        });
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
      if (Data.Percentages[_currentSubject].Count < level)
         Data.Percentages[_currentSubject].Add(Random.Range(50, 100) / 100f);
      else
         Data.Percentages[_currentSubject][level - 1] = Random.Range(50, 100) / 100f;

      _currentCoroutine = StartCoroutine(ChangePercentageOverTime(_content.GetChild((level - 1) * 2), Data.Percentages[_currentSubject][level - 1]));

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