using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// TODO: scale
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

   public void Load(string sceneName)
   {
      // TransitionScene.Instance.SwapToScene(sceneName);
      SceneManager.LoadScene(sceneName);
   }
}