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
    [SerializeField]
    GameObject elecEffect;
    [SerializeField]
    GameObject elecEffectSphere;
    public Rigidbody2D rb;
    private Vector2 direction;
    private System.Random rnd;
    public string nameElements;
    public int indexInContainer = 0;
    private int numberChoiceLink;
    public Mode status=Mode.MoveUnselect;
    private GameObject linkElement;
    private GameObject elSphere;
    private GameObject el;
    public enum Mode
    {
        MoveUnselect,
        MoveInContainer,
        StaticFluctuations,
        MoveSelect,
        MoveFromContainer,
        MoveLeftOffset

    }
    public void PerformStatus(Mode mode)
    {
        switch (mode)
        {
            case Mode.MoveSelect:
                AddElemSelect();
                break;
            case Mode.MoveInContainer:
                StartCoroutine(MoveToContainer(ConstantsMiniGame1.posTo1, ConstantsMiniGame1.posTo2, ConstantsMiniGame1.posTo3, ConstantsMiniGame1.timeTo1, ConstantsMiniGame1.timeTo2, ConstantsMiniGame1.timeTo3));
                break;
            case Mode.StaticFluctuations:
                StartCoroutine(Fluctuation());
                break;
            case Mode.MoveFromContainer:
                StartCoroutine(MoveFromContainer(ConstantsMiniGame1.posFrom1, ConstantsMiniGame1.posFrom2, ConstantsMiniGame1.posFrom3, ConstantsMiniGame1.timeFrom1, ConstantsMiniGame1.timeFrom2));
                break;
            case Mode.MoveLeftOffset:
                StartCoroutine(OffsetLeftElem(gameObject.transform.position, 1f));
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
        if (status != Mode.StaticFluctuations)
        {
            if (status == Mode.MoveUnselect)
            {
                status = Mode.MoveSelect;
                PerformStatus(status);
            }
            else
            {
                DeleteElemSelect();
                status = Mode.MoveUnselect;
            }
        }
        else
        {
            status = Mode.MoveFromContainer;
        }
    }
    private void Start()
    {
        rnd = new System.Random();
        direction.x = (float)rnd.NextDouble() * (2 + 1) - 1;
        direction.y = (float)rnd.NextDouble() * (2 + 1) - 1;
    }
    private void FixedUpdate()
    {
        if(linkElement!=null)
            FollowingElec(linkElement);
        if (status==Mode.MoveUnselect|| status == Mode.MoveSelect)
            rb.MovePosition(rb.position + direction.normalized * 3f * Time.fixedDeltaTime);
    } 



//ScriptedMove
    private void RemoveLink(GameObject c)
    {
        if (numberChoiceLink == c.GetComponent<ElementControl>().numberChoiceLink && (indexInContainer - c.GetComponent<ElementControl>().indexInContainer == 1 || indexInContainer - c.GetComponent<ElementControl>().indexInContainer == 0))
        {
            if (indexInContainer - c.GetComponent<ElementControl>().indexInContainer == 0)
            {
                Debug.Log(c.GetComponent<ElementControl>().linkElement);
                c.GetComponent<ElementControl>().linkElement = linkElement;
                Debug.Log(c.GetComponent<ElementControl>().linkElement);
                linkElement = null;
            }
            if (c.GetComponent<ElementControl>().linkElement == null)
            {
                c.GetComponent<ElementControl>().RemoveRotation();
                Destroy(c.GetComponent<ElementControl>().el);
            }
        }
    }
    private void  RemoveRotation()
    {
        rb.bodyType = RigidbodyType2D.Static;
        gameObject.transform.rotation = ConstantsMiniGame1.rotationElementZero;
    }

    private void AddElemSelect()
    {
        DatabaseSubstances.selectedElements[DatabaseSubstances.numberChoice].Add(gameObject);
        DatabaseSubstances.numbersInContainer++;
        numberChoiceLink = DatabaseSubstances.numberChoice;
        indexInContainer = DatabaseSubstances.numbersInContainer;
        for (int i = 1; i <= DatabaseSubstances.numberChoice; i++)
        {
            foreach (var c in DatabaseSubstances.selectedElements[i])
            {
                if (indexInContainer - c.GetComponent<ElementControl>().indexInContainer == 1 &&numberChoiceLink== c.GetComponent<ElementControl>().numberChoiceLink)
                {
                    linkElement = c.gameObject;
                }
            }
        }
        elSphere=Instantiate(elecEffectSphere, gameObject.transform);
    }
    private void DeleteElemSelect()
    {
        DatabaseSubstances.numbersInContainer--;
        for (int i = 1; i <= DatabaseSubstances.numberChoice; i++)
        {
            DatabaseSubstances.selectedElements[i].Remove(gameObject);
        }

        for (int i = 1; i <= DatabaseSubstances.numberChoice; i++)
        {
            foreach (var c in DatabaseSubstances.selectedElements[i])
            {
                if (c.GetComponent<ElementControl>().indexInContainer > indexInContainer && c.GetComponent<ElementControl>().status==Mode.MoveSelect)
                {
                    c.GetComponent<ElementControl>().indexInContainer--;
                }
            }
        }
        //numberChoiceLink = default(int);
        Destroy(elSphere);
    }
    private void FollowingElec(GameObject linkElement)
    {
        //if (linkElement.GetComponent<ElementControl>().numberChoiceLink == numberChoiceLink)
        // {
        Vector2 dirBetweenElements;
        dirBetweenElements = gameObject.transform.position - linkElement.transform.position;
        var angle = Mathf.Atan2(dirBetweenElements.y, dirBetweenElements.x) * Mathf.Rad2Deg;
        gameObject.transform.rotation = Quaternion.Euler(0, 0, angle);
        linkElement.transform.rotation = Quaternion.Euler(0, 0, angle);
        // }
    }
    private void OffsetLeftELements()
    {
        for (int i = 1; i <= DatabaseSubstances.numberChoice; i++)
        {
            foreach (var c in DatabaseSubstances.selectedElements[i])
            {
                if (c.GetComponent<ElementControl>().indexInContainer > indexInContainer && c.GetComponent<ElementControl>().status != Mode.MoveSelect)
                {
                    c.GetComponent<ElementControl>().status = Mode.MoveLeftOffset;
                    c.GetComponent<ElementControl>().indexInContainer--;
                }
                RemoveLink(c);
            }
        }
    }
    // корутины перемещения
    private IEnumerator MoveToContainer(Vector2 pos1, Vector2 pos2, Vector2 pos3, float time1, float time2, float time3)
    {
        if(DatabaseSubstances.selectedElements[DatabaseSubstances.numberChoice].Count==1)
        RemoveRotation();
        yield return StartCoroutine(MoveElement(pos1, time1+indexInContainer));
        yield return StartCoroutine(MoveElement(pos2, time2));
        Destroy(elSphere);
        yield return StartCoroutine(MoveElement(pos3 + new Vector2(indexInContainer, 0f), time3));
        yield return status = Mode.StaticFluctuations;
        yield return AcceptButton.isButtonAccept = false;
        for (int i = 1; i <= DatabaseSubstances.numberChoice; i++)
        {
            foreach (var c in DatabaseSubstances.selectedElements[i])
            {
                if (indexInContainer - c.GetComponent<ElementControl>().indexInContainer == 1 && numberChoiceLink==c.GetComponent<ElementControl>().numberChoiceLink)
                {
                    linkElement = c.gameObject;
                    el=Instantiate(elecEffect, gameObject.transform);
                }
            }
        }
        PerformStatus(status);
    }
    private IEnumerator MoveFromContainer(Vector2 pos1, Vector2 pos2, Vector2 pos3, float time1, float time2)
    {
        DeleteElemSelect();
        Destroy(el);
        yield return StartCoroutine(MoveElement(pos1+new Vector2(gameObject.transform.position.x,0f), time1));
        gameObject.transform.position = pos2;
        yield return StartCoroutine(MoveElement(pos3, time2));
        yield return rb.bodyType = RigidbodyType2D.Dynamic;
        status = Mode.MoveUnselect;
    }
    private IEnumerator Fluctuation ()
    {
       if(status!=Mode.MoveInContainer||status!=Mode.MoveLeftOffset)
       {
            while (status!=Mode.MoveFromContainer && status != Mode.MoveLeftOffset)
            {
                yield return StartCoroutine(MoveElement(gameObject.transform.position + new Vector3(0f, -0.2f, 0f), 1f));
                yield return StartCoroutine(MoveElement(gameObject.transform.position - new Vector3(0f, -0.2f, 0f), 1f));
            }
            if (status == Mode.MoveFromContainer)
            {
                PerformStatus(status);
                OffsetLeftELements();
            } 
            else
                PerformStatus(status);
       }
    }
    private IEnumerator OffsetLeftElem(Vector2 pos,float time)
    {
        yield return StartCoroutine(MoveElement(pos-new Vector2(1f,0f), time));
        yield return status = Mode.StaticFluctuations;
        PerformStatus(status);
    }
    private IEnumerator MoveElement(Vector2 endPos, float time)
    {
        yield return StartCoroutine(Moving(transform, gameObject.transform.position, endPos, time));
    }
    private IEnumerator Moving(Transform thisTransform, Vector2 startPos, Vector2 endPos, float time)
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

   // //colorswitch
    //private void SelectColor(GameObject element)
    //{
    //    StartCoroutine(GradualChangeBlack(element));
    //}
    //private void OriginColor(GameObject element)
    //{
    //    StartCoroutine(GradualChangeOrigin(element));
    //}

    //private IEnumerator GradualChangeBlack(GameObject element)
    //{
    //    float x = 1;
    //    while (x > 0)
    //    {
    //        element.GetComponent<Image>().color = new Color(x, x, x, 1);
    //        x -= 0.01f;
    //        yield return null;
    //    }
    //}
    //private IEnumerator GradualChangeOrigin(GameObject element)
    //{
    //    float x = 0;
    //    while (x < 1)
    //    {
    //        element.GetComponent<Image>().color = new Color(x, x, x, 1);
    //        x += 0.01f;
    //        yield return null;
    //    }
    //}
}
