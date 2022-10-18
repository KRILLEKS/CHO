using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class ChemistryMinigame2ExercisesHandler : MonoBehaviour
{
   [Header("Essentials")]
   [SerializeField] private Transform levelsFolder;
   [SerializeField] private RectTransform levelPointerRect;
   [Header("Values")]
   [SerializeField] private float pointerYFlyTime;
   [SerializeField] private float pointerYFlyDelta;
   [SerializeField] private Color finishedExerciseColourSerializable;

   // private static variables
   private static Color _finishedExerciseColour;
   private static Transform[] _levelRectTransforms;
   private static float _pointerStartY;
   private static ExerciseData[] _levelsData;
   private static int _currentExerciseIndex = 0;
   private static ChemistryMinigame2ExercisesHandler _exercisesHandler;

   private void Awake()
   {
      _exercisesHandler = this;

      _finishedExerciseColour = finishedExerciseColourSerializable;
   }

   private void Start()
   {
      KRICore.ChangeMenusAmount(levelsFolder, ChemistryMinigame2Exercises.Levels.Length);

      // set levels data
      _levelsData = new ExerciseData[ChemistryMinigame2Exercises.Levels.Length];
      for (int i = 0; i < ChemistryMinigame2Exercises.Levels.Length; i++)
         _levelsData[i] = new ExerciseData {StartFormula = ChemistryMinigame2Exercises.Levels[i].StartFormula + " -> "};

      // get level rect transforms
      _levelRectTransforms = new Transform[ChemistryMinigame2Exercises.Levels.Length + 1]; // +1 cause we include result tab
      for (int i = 0; i < ChemistryMinigame2Exercises.Levels.Length; i++)
         _levelRectTransforms[i] = levelsFolder.GetChild(i);
      
      // set result tab (last tab)
      Instantiate(Resources.Load<GameObject>("ChemistryMinigame2/Result"),levelsFolder);
      _levelRectTransforms[^1] = levelsFolder.GetChild(_levelRectTransforms.Length - 1).GetComponent<RectTransform>();

      // set buttons
      for (int i = 0; i < ChemistryMinigame2Exercises.Levels.Length + 1; i++) // +1 cause we include result tab
      {
         var index = i;
         levelsFolder.GetChild(i).GetComponent<Button>().onClick.AddListener(() => SetExercise(index));
      }

      // set text on buttons
      for (int i = 0; i < ChemistryMinigame2Exercises.Levels.Length; i++)
         levelsFolder.GetChild(i).GetChild(0).GetComponent<TextMeshProUGUI>().text = (i + 1).ToString();
      
      // initialize pointer
      _pointerStartY = levelPointerRect.position.y;
      levelPointerRect.DOMoveY(_pointerStartY - pointerYFlyDelta, pointerYFlyTime).SetEase(Ease.InOutSine).SetLoops(-1, LoopType.Yoyo);
      
      // we want to update info after all elements will be generated and horizontal layout will be applied to them
      StartCoroutine(LateStart());

      IEnumerator LateStart()
      {
         yield return new WaitForSeconds(0.1f);

         SetExercise(0, false);
      }
   }

   public static void NextExercise()
   {
      _levelRectTransforms[_currentExerciseIndex].GetComponent<RawImage>().color = _finishedExerciseColour;

      _exercisesHandler.SetExercise(_currentExerciseIndex + 1);
   }
   
   public void SetExercise(int index, bool toSave = true)
   {
      // save currentExercise
      if (toSave)
         _levelsData[_currentExerciseIndex] = ChemistryMinigame2FormulaHandler.GetExerciseData();

      levelPointerRect.position = new Vector2(_levelRectTransforms[index].position.x, _pointerStartY);
      
      // result
      if (index == _levelRectTransforms.Length - 1)
      {
         
      }
      // set exercise
      else
      {
         _currentExerciseIndex = index;
         ChemistryMinigame2FormulaHandler.LoadExercise(_levelsData[index]);
      }
   }

   public class ExerciseData
   {
      public string StartFormula;
      public string UserFormula;
      public string[] Indexes;
      public bool InEditState;
   }
}