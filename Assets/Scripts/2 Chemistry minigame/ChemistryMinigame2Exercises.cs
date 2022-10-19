using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class ChemistryMinigame2Exercises : MonoBehaviour
{
   [SerializeField] private Level[] levelsSerializable;
   
   // public static variables
   public static Level[] Levels;

   private void Awake()
   {
      Levels = levelsSerializable;
   }

   [Serializable]
   public class Level
   {
      public string StartFormula;
      [FormerlySerializedAs("ExpectedResult")] public string ExpectedFormula;
      public string[] Indexes;
   }
}
