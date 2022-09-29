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
   private static LevelsDatabase _levelsDatabase;

   [Serializable]
   public class Subject
   {
      // TODO: handle scenes (maybe level names) to load them based on database 
      public string name;
      public Texture background;
      public Texture subjectIcon;
      public Texture LockedTexture;
      public int levelsAmount;
   }
   
   private void Awake()
   {
      // singleton
      if (_levelsDatabase != null)
         Destroy(_levelsDatabase);

      _levelsDatabase = this;
      
      DontDestroyOnLoad(_levelsDatabase);
      
      // default initialization
      Subjects = subjectsSerializable;
      
      // initialize data
      Data.MaxLevels = new int[Subjects.Length];
      
      for (int i = 0; i < Data.MaxLevels.Length; i++)
         Data.MaxLevels[i] = 1;

      for (var index = 0; index < Data.MaxLevels.Length; index++)
      {
         Data.Percentages.Add(new List<float>());
      }
   }
}
