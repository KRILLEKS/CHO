using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Constants : MonoBehaviour
{
   // Chemistry minigame 2
   public const int IndexNumSize = 40;
   public const float MaxFormulaLength = 1000; // in pixels
   
   // Level selection
   // level content
   public const float LevelPercentageChangeTime = 3f;
   public const float PercentageTextAppearTime = 1f;
   public const float LevelUnlockTime = 1.5f;
   public static Color StartColor = new Color32(255, 88, 96,255); // 0 percentage
   public static Color EndColor = new Color32(255, 255, 255, 255); // 100 percentage
   // switch between subjects
   public const float BackgroundSwitchTime = 1; 
   public const float TextSwitchTime = 0.8f; // total time
}
