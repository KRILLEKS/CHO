using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// can be reworked if needed
public class PauseController : MonoBehaviour
{
   public static void PauseGame()
   {
      Time.timeScale = 0;
   }

   public static void ResumeGame()
   {
      Time.timeScale = 1;
   }

   /// <summary>
   /// if application is paused - resume
   /// if application isn't - pause 
   /// </summary>
   public static void InvertGameState()
   {
      // can be changed
      if (Time.timeScale == 0)
         ResumeGame();
      else
         PauseGame();
   }
}