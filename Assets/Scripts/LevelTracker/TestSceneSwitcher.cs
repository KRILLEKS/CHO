using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace LevelTracker
{
    public class TestSceneSwitcher : MonoBehaviour
    {
        private static TestSceneSwitcher _sceneSwitcher;

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

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.O))
                SceneManager.LoadScene("LevelTracker");
            if (Input.GetKeyDown(KeyCode.P))
                SceneManager.LoadScene("LevelSelection");
        }
    }
}
