using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Test : MonoBehaviour
{
   [SerializeField] private TextMeshProUGUI tmp;

   private void Awake()
   {
      // Debug.Log(LayoutUtility.GetPreferredWidth(tmp.rectTransform));
   }
}
