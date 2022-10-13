using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssetProvider : MonoBehaviour
{
    private AssetProvider _assetProvider;
    public static GameObject ElementPrefab;
    public static GameObject AverageElementPrefab;

    private void Awake()
    {      
        // singleton
        if (_assetProvider != null)
        {
            Destroy(gameObject);
            return;
        }

        _assetProvider = this;
        DontDestroyOnLoad(this);
        LoadValues();
    }
    

    private void LoadValues()
    {
        ElementPrefab = Resources.Load<GameObject>($"LevelTracker/ElementPrefab");
        AverageElementPrefab = Resources.Load<GameObject>($"LevelTracker/AverageElementPrefab");
    }
    
}
