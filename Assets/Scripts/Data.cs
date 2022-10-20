using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// initialized by LevelsDatabase
public class Data : MonoBehaviour
{
   // public static variables
   public static Data Instance;
   
   public static int[] MaxLevels;
   public static List<float[]> Percentages = new List<float[]>(); // initialized in levels database

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
}
