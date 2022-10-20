using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelsSummarySingleton : MonoBehaviour
{
   // public static variables
   public static LevelsSummarySingleton Instance;

   public static int CurrentSubjectIndex = -1;
   public static int CurrentLevel = -1;
   
   // private variables
   private static float _previousLevelPercentage = -1;

   private void Awake()
   {
      if (Instance != null)
         Destroy(gameObject);
      else
      {
         Instance = this;
         DontDestroyOnLoad(Instance);
      }
   }

   // not static cause can be invoked via button click and you can get this method via static instance anyway
   public void SetLevelPercentage(float percentage)
   {
      _previousLevelPercentage = percentage;

      // we update everything here so it scales well
      if (CurrentLevel > Data.MaxLevels[CurrentSubjectIndex])
         Data.MaxLevels[CurrentSubjectIndex]++;
      
      Data.Percentages[CurrentSubjectIndex][CurrentLevel - 1] = Instance.GetLevelPercentage();
   }

   // not static cause can be invoked via button click (right now can't) and you can get this method via static instance anyway
   public float GetLevelPercentage()
   {
      return _previousLevelPercentage;
   }
}