using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SelectElement : MonoBehaviour, IPointerClickHandler
{
    [SerializeField]
    GameObject contentElements;
    
    public void OnPointerClick(PointerEventData eventData)
    {
        this.gameObject.GetComponent<Collider2D>().enabled = false;
        this.gameObject.GetComponent<Rigidbody2D>().bodyType=RigidbodyType2D.Static;
        this.gameObject.transform.SetParent(GameObject.Find("ContentElements").transform);
    }

}
