using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SelectElement : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        this.gameObject.GetComponent<MovedElements>().isSelectElements = true;
        this.gameObject.transform.SetParent(GameObject.Find("ContentElements").transform);
        RemoveRotation();
        this.gameObject.transform.localScale = new Vector3(ConstantsMiniGame1.scaleSelectElementX, ConstantsMiniGame1.scaleSelectElementY,ConstantsMiniGame1.scaleSelectElementZ);

    }
   
    private void FixedUpdate()
    {
        StartCoroutine(Startfluctuations());
    }

    private void RemoveRotation()
    {
        this.gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
        this.gameObject.transform.rotation = ConstantsMiniGame1.rotationElementZero;
        this.gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
    }

    IEnumerator Startfluctuations()
    {
        Vector3 pointA = new Vector3(this.gameObject.transform.position.x, ConstantsMiniGame1.upPositionFluctuation, ConstantsMiniGame1.zPositionFluctuation);
        Vector3 pointB = new Vector3(this.gameObject.transform.position.x, ConstantsMiniGame1.downPositionFluctuation,ConstantsMiniGame1.zPositionFluctuation);
        while (this.gameObject.GetComponent<MovedElements>().isSelectElements == true)
        {
            yield return StartCoroutine(Fluctuation(transform, pointA, pointB, ConstantsMiniGame1.timeFluctuation));
            yield return StartCoroutine(Fluctuation(transform, pointB, pointA, ConstantsMiniGame1.timeFluctuation));

        }
    }
    IEnumerator Fluctuation(Transform thisTransform, Vector3 startPos, Vector3 endPos, float time)
    {
        float i = 0.0f;
        float rate = 1.0f / time;
        while (i < 1.0f)
        {
            i += Time.deltaTime * rate;
            thisTransform.position = Vector3.Lerp(startPos, endPos, i);
            yield return null;
        }
    }
    
}
