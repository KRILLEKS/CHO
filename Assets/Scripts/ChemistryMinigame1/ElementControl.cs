using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;
using System.Linq;

public class ElementControl : MonoBehaviour, IPointerClickHandler
{
    [SerializeField]
    GameObject Prefab;
    public Rigidbody2D rb;
    private Vector2 direction;
    private System.Random rnd;
    public string nameElements;
    public bool isSelectElement = false;
    public bool isInContainer = false;
    public int numberInContainer = 0;

    void Start()
    {
        rnd = new System.Random();
        direction.x = (float)rnd.NextDouble() * (2 + 1) - 1;
        direction.y = (float)rnd.NextDouble() * (2 + 1) - 1;
        
    }


    void FixedUpdate()
    {
        if (isInContainer == false)
            rb.MovePosition(rb.position + direction.normalized * 3f * Time.fixedDeltaTime);
        StartCoroutine(Startfluctuations());
    
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.gameObject.name)
        {
            case "Up":
            case "PipeDownUp":
            case "PipeUpUp":
                direction.y *= -1;
                break;
            case "Down":
                direction.y *= -1;
                break;
            case "PipeDownLeft":
            case "PipeUpLeft":
            case "Left":
                direction.x *= -1;
                break;
            case "PipeUpRight":
            case "PipeDownRight":
            case "Right":
                direction.x *= -1;
                break;
        }
    }


    public void OnPointerClick(PointerEventData eventData)
    {
        
        if (isInContainer == false)
        {
            if (isSelectElement == false)
            {
                DatabaseSubstances.selectedElements[DatabaseSubstances.numberChoice].Add(gameObject);
                DatabaseSubstances.numberInContainer++;
                numberInContainer = DatabaseSubstances.numberInContainer;
                SelectColor(gameObject);
                isSelectElement = true;
            }
            else
            {
                DatabaseSubstances.selectedElements[DatabaseSubstances.numberChoice].Remove(gameObject);
                DatabaseSubstances.numberInContainer--;
                isSelectElement = false;
                OriginColor(gameObject);
            }
        }
        else 
        {
            isInContainer = false;
        }
           

    }

//ScriptedMove

    //делает объекты статичными,что их не могли сбить другие объекты, а также отменяет вращение объекта
    public void RemoveRotation()
    {
        rb.bodyType = RigidbodyType2D.Static;
        gameObject.transform.rotation = ConstantsMiniGame1.rotationElementZero;
    }
    //перемещает элементы в контейнер 
    public IEnumerator MoveToContainer(Vector2 pos1, Vector2 pos2, Vector2 pos3, float time1, float time2, float time3)
    {
        yield return StartCoroutine(MoveElement(pos1, time1));
        yield return StartCoroutine(MoveElement(pos2, time2));
        yield return StartCoroutine(MoveElement(pos3+ new Vector2(numberInContainer, 0f), time3));
        yield return isInContainer= true;
        yield return AcceptButton.isButtonAccept = false;
    }

    //делает колебания у объектов, когда они в контейнере
    public IEnumerator Startfluctuations()
    {
        Vector2 pointA = new Vector2(gameObject.transform.position.x, ConstantsMiniGame1.upPositionFluctuation);
        Vector2 pointB = new Vector2(gameObject.transform.position.x, ConstantsMiniGame1.downPositionFluctuation);
        while (isSelectElement == true && isInContainer == true)
        {
            yield return StartCoroutine(ChangeOscillation(transform, pointA, pointB, ConstantsMiniGame1.timeFluctuation));
            yield return StartCoroutine(ChangeOscillation(transform, pointB, pointA, ConstantsMiniGame1.timeFluctuation));
            if (isInContainer == false)
            {
                yield return MoveFromContainer(ConstantsMiniGame1.posFrom1, ConstantsMiniGame1.posFrom2, ConstantsMiniGame1.posFrom3, ConstantsMiniGame1.timeFrom1, ConstantsMiniGame1.timeFrom2);
            }
        }
    }
    //убираем элемент из контейнера
    public IEnumerator MoveFromContainer(Vector2 pos1, Vector2 pos2, Vector2 pos3, float time1, float time2)
    {
        yield return StartCoroutine(MoveElement(pos1+new Vector2(gameObject.transform.position.x,0f), time1));
        gameObject.transform.position = pos2;
        yield return StartCoroutine(MoveElement(pos3, time2));
        yield return isSelectElement == false;
        yield return rb.bodyType = RigidbodyType2D.Dynamic;

    }

    IEnumerator MoveElement(Vector2 endPos, float time)
    {
        yield return StartCoroutine(ChangeOscillation(transform, gameObject.transform.position, endPos, time));
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


//colorswitch
    private void SelectColor(GameObject element)
    {
        StartCoroutine(GradualChangeBlack(element));
    }
    private void OriginColor(GameObject element)
    {
        StartCoroutine(GradualChangeOrigin(element));
    }

    private IEnumerator GradualChangeBlack(GameObject element)
    {
       
        float x = 1;
        while (x > 0)
        {
            element.GetComponent<Image>().color = new Color(x, x, x, 1);
            x -= 0.01f;
            yield return null;
        }

    }
    private IEnumerator GradualChangeOrigin(GameObject element)
    {
        float x = 0;
        while (x < 1)
        {
            element.GetComponent<Image>().color = new Color(x, x, x, 1);
            x += 0.01f;
            yield return null;
        }

    }
}
