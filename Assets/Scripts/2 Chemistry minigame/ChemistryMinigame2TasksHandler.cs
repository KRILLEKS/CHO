using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class ChemistryMinigame2TasksHandler : GenerateTaskButtons
{
   [Header("Essentials")]
   [SerializeField] private Transform levelsFolder;
   [SerializeField] private RectTransform levelPointerRect;
   [SerializeField] private GameObject resultButtonSerializable;
   [Header("Values")]
   [SerializeField] private float pointerYFlyTime;
   [SerializeField] private float pointerYFlyDelta;
   [SerializeField] private Color finishedExerciseColourSerializable;

   // public static variables
   public static ExerciseData[] LevelsData;
   public static GameObject ResultButtonGO;

   // private static variables
   private static ChemistryMinigame2TasksHandler _tasksHandler;
   private static Color _finishedExerciseColour;

   private static Transform[] _levelRectTransforms; // doesn't include result tab
   private static float _pointerStartY;
   private static int _currentExerciseIndex = 0;

   public class ExerciseData
   {
      public string StartFormula;
      public string UserFormula;
      public string[] Indexes;
      public bool InEditState;
   }

   private void Awake()
   {
      _tasksHandler = this;

      _finishedExerciseColour = finishedExerciseColourSerializable;
      ResultButtonGO = resultButtonSerializable;

      ResultButtonGO.SetActive(false);
   }

   private void Start()
   {
      // generate tasks
      _levelRectTransforms = GenerateTasks(levelsFolder, ChemistryMinigame2Exercises.Levels.Length, _ => SetExercise(_));

      // set levels data
      LevelsData = new ExerciseData[ChemistryMinigame2Exercises.Levels.Length];
      for (int i = 0; i < ChemistryMinigame2Exercises.Levels.Length; i++)
         LevelsData[i] = new ExerciseData {StartFormula = ChemistryMinigame2Exercises.Levels[i].StartFormula + " -> "};

      // set result tab (last tab)
      Instantiate(Resources.Load<GameObject>("ChemistryMinigame2/Result"), levelsFolder);
      levelsFolder.GetChild(levelsFolder.childCount - 1).GetComponent<Button>().onClick.AddListener(SetResultTab);

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

   // invokes when exercise is finished
   public static void NextExercise()
   {
      _levelRectTransforms[_currentExerciseIndex].GetComponent<RawImage>().color = _finishedExerciseColour;

      _tasksHandler.SetExercise(_currentExerciseIndex + 1);
   }

   public void SetExercise(int index, bool toSave = true)
   {
      // save currentExercise
      if (ResultButtonGO.activeSelf) // result tab
         ResultButtonGO.SetActive(false);
      else if (toSave)
         LevelsData[_currentExerciseIndex] = ChemistryMinigame2FormulaHandler.GetExerciseData();

      levelPointerRect.position = new Vector2(_levelRectTransforms[index].position.x, _pointerStartY);

      _currentExerciseIndex = index;
      ChemistryMinigame2FormulaHandler.LoadExercise(LevelsData[index]);
   }

   public void SetResultTab()
   {
      ChemistryMinigame2FormulaHandler.DisableAllElements();
      ResultButtonGO.SetActive(true);
   }
}