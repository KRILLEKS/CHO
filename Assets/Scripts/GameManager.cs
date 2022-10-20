using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
   [SerializeField] private string[] additiveScenesNames; // to load extra scenes

   private void Awake()
   {
      foreach (var sceneName in additiveScenesNames)
      {
         if (SceneManager.GetSceneByName(sceneName).isLoaded == false)
            SceneManager.LoadScene(sceneName, LoadSceneMode.Additive);
      }
   }
}