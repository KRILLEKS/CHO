using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KRICore : MonoBehaviour
{
   /// <summary>
   /// <para>It will change amount of child objects in parent<br />
   /// (duplicate the first one to make more children objects or destroy extra)</para>
   /// <para>3rd parameter determines which menu to instantiate. If it's == null then first menu will be duplicated </para>
   /// </summary>
   /// <param name="parent"></param>
   /// <param name="amount"></param>
   /// <param name="menuPrefab"></param>
   public static void ChangeMenusAmount(Transform parent, int amount, GameObject menuPrefab = null)
   {
      // remove extra
      if (parent.childCount - amount > 0)
         for (int i = parent.childCount; i > amount; i--)
            Destroy(parent.GetChild(i - 1).gameObject);
      
      // add extra
      else if (parent.childCount - amount < 0)
      {
         GameObject original = menuPrefab == null ? parent.GetChild(0).gameObject : menuPrefab;
         
         for (int i = parent.childCount; i < amount; i++)
            Instantiate(original, parent);
      }
   }

   /// <summary>
   /// <para>input must be in range [0;1]</para>
   /// <para>if u have params like 0.4f, 0.8f<br />
   /// output will be [-0.8f;-0.4f] and [0.4f; 0.8f]</para>
   /// </summary>
   /// <returns></returns>
   public static float GetRandomVal(float minVal, float maxVal)
   {
      return Random.Range(0, 2) == 0 ? Random.Range(minVal, maxVal) : Random.Range(-maxVal, -minVal);
   }
}
