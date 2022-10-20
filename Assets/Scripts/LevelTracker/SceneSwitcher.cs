using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace LevelTracker
{
    public class SceneSwitcher : MonoBehaviour
    {
        private static SceneSwitcher _sceneSwitcher;

        private void Awake()
        {      
            if (_sceneSwitcher != null)
            {
                Destroy(gameObject);
                return;
            }
            _sceneSwitcher = this;
      
            DontDestroyOnLoad(_sceneSwitcher);
        }

        public static void SwitchSceneToSelection() => 
            SceneManager.LoadScene("LevelSelection");

        public static void SwitchSceneToTracker() => 
            SceneManager.LoadScene("LevelTracker");
    }
}
