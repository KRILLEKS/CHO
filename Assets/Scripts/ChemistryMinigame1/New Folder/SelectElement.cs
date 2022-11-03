using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SelectElement : MonoBehaviour, IPointerClickHandler
{
    private bool isElementStanding=false;
    private void FixedUpdate()
    {
        if (isElementStanding == true)
        {
            Vector3 pointA = new Vector3(this.gameObject.transform.position.x, ConstantsMiniGame1.upPositionFluctuation, ConstantsMiniGame1.zPositionFluctuation);
            Vector3 pointB = new Vector3(this.gameObject.transform.position.x, ConstantsMiniGame1.downPositionFluctuation, ConstantsMiniGame1.zPositionFluctuation);
            StartCoroutine(Startfluctuations(pointA, pointB));
        }

    }
    public void OnPointerClick(PointerEventData eventData)
    {
        if (this.gameObject.GetComponent<MovedElements>().isSelectElements == false)
        {
            this.gameObject.GetComponent<MovedElements>().isSelectElements = true;
            //this.gameObject.transform.SetParent(GameObject.Find("ContentElements").transform);
            RemoveRotation();
            this.gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
            // this.gameObject.transform.localScale = new Vector3(ConstantsMiniGame1.scaleSelectElementX, ConstantsMiniGame1.scaleSelectElementY,ConstantsMiniGame1.scaleSelectElementZ);
            StartCoroutine(MoveToContainer(new Vector3(0f, -2f, 0f), new Vector3(0f, -4.5f, 0f), new Vector3(-8.12f, -4.5f, 0f), 3f, 3f, 3f));
        }

    }
   private IEnumerator AssemblePairElements (Vector3 endPos, float time)
   {
        yield return StartCoroutine(MoveElement(transform, this.gameObject.transform.position, endPos,time));
   }

    private void RemoveRotation()
    {
        this.gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
        this.gameObject.transform.rotation = ConstantsMiniGame1.rotationElementZero;
        this.gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
    }

   private IEnumerator MoveToContainer(Vector3 pos1,Vector3 pos2, Vector3 pos3,float time1, float time2, float time3)
    {
       
            yield return StartCoroutine(AssemblePairElements(pos1, time1));
            yield return StartCoroutine(AssemblePairElements(pos2, time2));
            yield return StartCoroutine(AssemblePairElements(pos3, time3));
            yield return isElementStanding = true;
}
   private IEnumerator Startfluctuations(Vector3 pointA, Vector3 pointB)
    {       
        while (this.gameObject.GetComponent<MovedElements>().isSelectElements == true)
        {
            yield return StartCoroutine(MoveElement(transform, pointA, pointB, ConstantsMiniGame1.timeFluctuation));
            yield return StartCoroutine(MoveElement(transform, pointB, pointA, ConstantsMiniGame1.timeFluctuation));

        }
    }
    IEnumerator MoveElement(Transform thisTransform, Vector3 startPos, Vector3 endPos, float time)
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
