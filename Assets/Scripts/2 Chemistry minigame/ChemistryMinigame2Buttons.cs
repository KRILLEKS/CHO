using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class ChemistryMinigame2Buttons : MonoBehaviour
{
   private static float _finalPercentage;

   private void Awake()
   {
      var resultButton = ChemistryMinigame2TasksHandler.ResultButtonGO.GetComponent<Button>();
      resultButton.onClick.AddListener(() => LevelsSummarySingleton.Instance.SetLevelPercentage(_finalPercentage));
      resultButton.onClick.AddListener(() => TransitionScene.Instance.SwapToScene("LevelSelection"));
   }

   // invokes on Confirm
   public void Confirm()
   {
      ChemistryMinigame2FormulaHandler.IsEditState(true);
   }

   // invokes on EditButton
   public void Edit()
   {
      ChemistryMinigame2FormulaHandler._inputFieldIndexes = ChemistryMinigame2FormulaHandler._inputFieldList.Select(x => x.text).ToArray();
      ChemistryMinigame2FormulaHandler._inputFieldList = null;

      ChemistryMinigame2FormulaHandler.IsEditState(false);
   }

   // invokes on small confirm
   public void ConfirmIndexes()
   {
      ChemistryMinigame2TasksHandler.NextExercise();
   }

   // invokes on summarize button and calculates the result
   public void Summarize()
   {
      int correctAnswers = 0;

      for (int exercise = 0; exercise < ChemistryMinigame2Exercises.Levels.Length; exercise++)
      {
         if (ChemistryMinigame2Exercises.Levels[exercise].ExpectedFormula == ChemistryMinigame2TasksHandler.LevelsData[exercise].UserFormula)
         {
            Debug.Log("correct formula " + (exercise + 1));
            // compare indexes
            for (int i = 0; i < ChemistryMinigame2Exercises.Levels[exercise].Indexes.Length; i++)
            {
               if (ChemistryMinigame2Exercises.Levels[exercise].Indexes[i] != ChemistryMinigame2TasksHandler.LevelsData[exercise].Indexes[i])
               {
                  Debug.Log($"incorrect index i: {ChemistryMinigame2TasksHandler.LevelsData[exercise].Indexes[i]}, expected: {ChemistryMinigame2Exercises.Levels[exercise].Indexes[i]}");
                  return;
               }
            }

            correctAnswers++;
            Debug.Log((exercise + 1) + " is correct");
         }
         else
         {
            Debug.Log($"incorrect formula {exercise}: {ChemistryMinigame2TasksHandler.LevelsData[exercise].UserFormula}, expected: {ChemistryMinigame2Exercises.Levels[exercise].ExpectedFormula}");
         }
      }

      Debug.Log("final percentage: " + ((float) correctAnswers / ChemistryMinigame2Exercises.Levels.Length));
      _finalPercentage = ((float)correctAnswers / ChemistryMinigame2Exercises.Levels.Length);
   }
}