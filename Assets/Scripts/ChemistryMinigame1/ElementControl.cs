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
    public int indexInContainer = 0;
    public Mode status=Mode.Move;

    void Start()
    {
        rnd = new System.Random();
        direction.x = (float)rnd.NextDouble() * (2 + 1) - 1;
        direction.y = (float)rnd.NextDouble() * (2 + 1) - 1;
        Status(status);

    }


    void FixedUpdate()
    {
        if (status==Mode.Move)
            rb.MovePosition(rb.position + direction.normalized * 3f * Time.fixedDeltaTime);
    }
    public enum Mode 
    {
        Move,
        MoveInContainer,
        StaticFluctuations,

    }
    public void Status (Mode mode)
    {
        switch(mode)
        {
            case Mode.Move:
                rb.MovePosition(rb.position + direction.normalized * 3f * Time.fixedDeltaTime);
                break;
            case Mode.MoveInContainer:
                RemoveRotation();
                StartCoroutine(MoveToContainer(ConstantsMiniGame1.posTo1, ConstantsMiniGame1.posTo2, ConstantsMiniGame1.posTo3, ConstantsMiniGame1.timeTo1, ConstantsMiniGame1.timeTo2, ConstantsMiniGame1.timeTo3));
                status = Mode.StaticFluctuations;
                break;
            case Mode.StaticFluctuations:
              StartCoroutine(Fluc());
                break;

        }
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
                AddElemSelect();
                isSelectElement = true;
            }
            else
            {
                DeleteElemSelect();
                isSelectElement = false;
            }
        }
        else 
        {
            isInContainer = false;
        }
           

    }

    public void OffsetLeft()
    {
        for (int i = 1; i < DatabaseSubstances.numberChoice; i++)
        {
            foreach (var c in DatabaseSubstances.selectedElements[i])
            {
                if (c.GetComponent<ElementControl>().indexInContainer >= DatabaseSubstances.numbersInContainer - indexInContainer - 1)
                {
                    Debug.Log(c.name);
                    //StartCoroutine(c.GetComponent<ElementControl>().OffsetLeftElem(c.GetComponent<GameObject>().transform.position,1f));
                    c.GetComponent<ElementControl>().indexInContainer--;

                }

            }
        }
    }

//ScriptedMove

    //делает объекты статичными,что их не могли сбить другие объекты, а также отменяет вращение объекта
    public void RemoveRotation()
    {
        rb.bodyType = RigidbodyType2D.Static;
        gameObject.transform.rotation = ConstantsMiniGame1.rotationElementZero;
    }


    //делает колебания у объектов, когда они в контейнере
    //public IEnumerator Startfluctuations()
    //{

    //    //while (isSelectElement == true && isInContainer == true)
    //    if (isSelectElement == true && isInContainer == true)
    //    {

    //        yield return StartCoroutine(ChangeOscillation(transform, new Vector2(gameObject.transform.position.x, ConstantsMiniGame1.upPositionFluctuation), new Vector2(gameObject.transform.position.x, ConstantsMiniGame1.downPositionFluctuation), ConstantsMiniGame1.timeFluctuation));
    //        yield return StartCoroutine(ChangeOscillation(transform, new Vector2(gameObject.transform.position.x, ConstantsMiniGame1.downPositionFluctuation), new Vector2(gameObject.transform.position.x, ConstantsMiniGame1.upPositionFluctuation), ConstantsMiniGame1.timeFluctuation));
    //        //if (isInContainer == false)
    //        //{         
    //        //    yield return MoveFromContainer(ConstantsMiniGame1.posFrom1, ConstantsMiniGame1.posFrom2, ConstantsMiniGame1.posFrom3, ConstantsMiniGame1.timeFrom1, ConstantsMiniGame1.timeFrom2);

    //        //}
    //        //onOffsetLeft.Invoke();
    //        //DeleteElemSelect();
    //    }
        
    //} 
//Взаимодействие с базой выбранных элементов
    public void AddElemSelect()
    {
        DatabaseSubstances.selectedElements[DatabaseSubstances.numberChoice].Add(gameObject);
        DatabaseSubstances.numbersInContainer++;
        indexInContainer = DatabaseSubstances.numbersInContainer;
    }
    public void DeleteElemSelect()
    {
        for (int i = 1; i < DatabaseSubstances.numberChoice; i++)
        {
            DatabaseSubstances.selectedElements[i].Remove(gameObject);
            DatabaseSubstances.numbersInContainer--;
            indexInContainer = 0;
        }
        
    }

//перемещает элементы в контейнер 
    public IEnumerator MoveToContainer(Vector2 pos1, Vector2 pos2, Vector2 pos3, float time1, float time2, float time3)
    {
        if (isInContainer == false)
        {
            yield return StartCoroutine(MoveElement(pos1, time1));
            yield return StartCoroutine(MoveElement(pos2, time2));
            yield return StartCoroutine(MoveElement(pos3 + new Vector2(indexInContainer, 0f), time3));
            yield return isInContainer = true;
            yield return AcceptButton.isButtonAccept = false;
            Status(Mode.StaticFluctuations);
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
        DeleteElemSelect();
        status = Mode.Move;
    }
    public IEnumerator Fluc ()
    {
        if (isSelectElement == true && isInContainer == true)
        {
            while (isSelectElement == true && isInContainer == true)
            {
                yield return StartCoroutine(MoveElement(gameObject.transform.position + new Vector3(0f, -0.2f, 0f), 1f));
                yield return StartCoroutine(MoveElement(gameObject.transform.position - new Vector3(0f, -0.2f, 0f), 1f));
                if (isInContainer == false)
                {
                    
                    yield return MoveFromContainer(ConstantsMiniGame1.posFrom1, ConstantsMiniGame1.posFrom2, ConstantsMiniGame1.posFrom3, ConstantsMiniGame1.timeFrom1, ConstantsMiniGame1.timeFrom2);
                }
            }
        }
    }

    //public IEnumerator OffsetLeftElem(Vector2 pos,float time)
    //{
    //    yield return StartCoroutine(MoveElement(pos-new Vector2(1f,0f), time));
    //}

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
