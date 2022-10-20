using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Restart : MonoBehaviour
{
    [SerializeField] 
    GameObject content;
    [SerializeField]
    GameObject chemicalElements;

   public void RestartScene()
    {
        Time.timeScale = 1f;
        Destroy(content);
        Destroy(chemicalElements);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
  
}
