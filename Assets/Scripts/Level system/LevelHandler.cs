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

   private void OnEnable()
   {
      // means we just launched app
      if (LevelsSummarySingleton.CurrentLevel == -1)
         return;

      // We spawn objects in generate level content
      // and unity needs time to generate them
      // TODO: change that. So we won't destroy/spawn objects this often
      // TODO: scale with loading animation (if delay will be used)
      StartCoroutine(OnEnableWithDelay());
      
      IEnumerator OnEnableWithDelay()
      {
         yield return new WaitForSeconds(0.8f);

         StartCoroutine(ChangePercentageOverTime(_content.GetChild((LevelsSummarySingleton.CurrentLevel - 1) * 2), LevelsSummarySingleton.Instance.GetLevelPercentage()));
         UnlockLevel(LevelsSummarySingleton.CurrentLevel + 1);
      }
   }

   // step can be -1 or +1
   // this function for buttons
   // with step = 0 it can be used to update current info
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

   // we use this method to unlock next level 
   public static void UnlockLevel(int level)
   {
      Debug.Log("Try to unlock level ");

      // first condition means that we completed last level
      // second condition means that we don't have to unlock level (it's already unlocked)
      if ((_content.childCount >= (level - 1) * 2 + 1) == false ||
          (Data.MaxLevels[_currentSubject] >= level))
         return;

      Debug.Log("Unlock level" + level);

      // unlock level
      // fade out locked image
      Transform levelTransform = _content.GetChild((level - 1) * 2);
      Transform levelIsLockedTransform = levelTransform.Find("LevelIsLocked");

      levelIsLockedTransform.GetComponent<RawImage>()
                            .DOFade(0, Constants.LevelUnlockTime)
                            .OnComplete(() =>
                            {
                               levelIsLockedTransform.gameObject.SetActive(false);
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