using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class SelectElement : MonoBehaviour, IPointerClickHandler
{
    private bool isElementStanding=false;
    private bool isChoiceElements = false;
    private static int numberChoice = 0;
    private GameObject elEffect;
    private void FixedUpdate()
    {
        //if the elements are in a container, a swing effect appears
        if (isElementStanding == true)
        {
            Vector2 pointA = new Vector2(gameObject.transform.position.x, ConstantsMiniGame1.upPositionFluctuation);
            Vector2 pointB = new Vector2(gameObject.transform.position.x, ConstantsMiniGame1.downPositionFluctuation);
            StartCoroutine(Startfluctuations(pointA, pointB));
        }

        //if (elEffect.GetComponent<GenerateEffects>().elec != null && firstElement != null && secondElement!=null)
        //{

        //    elEffect.GetComponent<GenerateEffects>().FollowingElec(firstElement,secondElement);
        //    Debug.Log(firstElement.name);
        //    Debug.Log(secondElement.name);
        //}
        //if (isChoiceElements == true)
        //{
        //    Vector3 pointA = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y+0.5f, gameObject.transform.position.z);
        //    Vector3 pointB = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y - 0.5f, gameObject.transform.position.z);
        //    StartCoroutine(Startfluctuations(pointA, pointB));
        //}
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        ConstantsMiniGame1 c = new ConstantsMiniGame1();
        numberChoice++;
        if (gameObject.GetComponent<MovedElements>().isSelectElements == false && numberChoice<=2)
        {
            ColorSwitchElements colorSwitch = new ColorSwitchElements();
            gameObject.GetComponent<MovedElements>().isSelectElements = true;
            RemoveRotation();
            if (numberChoice == 1)
            {
                StartCoroutine(MoveToContainer(c.pos1, c.pos2, c.pos3, c.time1, c.time2, c.time3));
                // StartCoroutine(MoveElement(new Vector2(0f, -2.42f), 1f));
                RelatedElements.firstElement = gameObject;
                //colorSwitch.SelectColor(gameObject);
            }
            if (numberChoice == 2)
            {
                elEffect = GameObject.Find("Controllers").GetComponent<GenerateEffects>().GetElectricityEffect;
                //StartCoroutine(MoveElement(new Vector2(0f, -1f), 1f));
                StartCoroutine(MoveToContainer(c.pos1, c.pos2, c.pos3 + new Vector2(1f, 0f), c.time1, c.time2, c.time3));
                //colorSwitch.SelectColor(gameObject);
                Instantiate(elEffect, gameObject.transform);
                RelatedElements.firstElement = gameObject;
            }
        }

    }

    private void RemoveRotation()
    {
        gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
        gameObject.transform.rotation = ConstantsMiniGame1.rotationElementZero;
    }

   private IEnumerator MoveToContainer(Vector2 pos1,Vector2 pos2, Vector2 pos3,float time1, float time2, float time3)
    {
       
            yield return StartCoroutine(MoveElement(pos1, time1));
            yield return StartCoroutine(MoveElement(pos2, time2));
            yield return StartCoroutine(MoveElement(pos3, time3));
            yield return isElementStanding = true;
    }
    private IEnumerator MoveElement(Vector2 endPos, float time)
    {
        yield return StartCoroutine(ChangeOscillation(transform, gameObject.transform.position, endPos, time));
    }

    private IEnumerator Startfluctuations(Vector2 pointA, Vector2 pointB)
    {       
        while (gameObject.GetComponent<MovedElements>().isSelectElements == true)
        {
            yield return StartCoroutine(ChangeOscillation(transform, pointA, pointB, ConstantsMiniGame1.timeFluctuation));
            yield return StartCoroutine(ChangeOscillation(transform, pointB, pointA, ConstantsMiniGame1.timeFluctuation));

        }
    }
    IEnumerator ChangeOscillation(Transform thisTransform, Vector2 startPos, Vector2 endPos, float time)
    {
        float i = 0.0f;
        float rate = 1.0f / time;
        while (i < 1.0f)
        {
            i += Time.deltaTime * rate;
            thisTransform.position = Vector2.Lerp(startPos, endPos, i);
            yield return null;
        }
    }
    

}
