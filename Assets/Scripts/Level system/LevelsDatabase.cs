using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

// TODO: move it somewhere in loading menu cause it's singleton
public class LevelsDatabase : MonoBehaviour
{
   [SerializeField] private Subject[] subjectsSerializable;
   
   // public static variables
   public static Subject[] Subjects;

   // private static variables
   public static int CurrentSubjectIndex { get; private set; }

   [Serializable]
   public class Subject
   {
      // TODO: handle scenes (maybe level names) to load them based on database 
      public string name;
      public int levelsAmount;
      public Texture background;
      public Texture subjectIcon;
      public Vector2 unlockedTextureSize;
      public Texture lockedTexture;
      public Vector2 lockedTextureSize;
      public string[] sceneNames;
   }
   
   private void Awake()
   {

      // default initialization
      Subjects = subjectsSerializable;
      
      // means that data is initialized before (data is singleton)
      if (Data.MaxLevels != null)
         return;
      
      // initialize data
      Data.MaxLevels = new int[Subjects.Length];
      
      // for (int i = 0; i < Data.MaxLevels.Length; i++)
      //    Data.MaxLevels[i] = 1;

      for (var subjectIndex = 0; subjectIndex < Subjects.Length; subjectIndex++)
      {
         Data.Percentages.Add(new float[Subjects[subjectIndex].levelsAmount]);
      }
   }

   public static void SetSubjectIndex(int index) =>
      CurrentSubjectIndex = index;
}
