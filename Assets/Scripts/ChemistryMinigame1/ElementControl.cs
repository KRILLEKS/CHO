using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ElementControl : MonoBehaviour, IPointerClickHandler
{
    [SerializeField]
    GameObject Prefab;
    public Rigidbody2D rb;
    private Vector2 direction;
    private System.Random rnd;
    public string nameElements;
    private float timeContactElem;
    public bool isSelectElement = false;
    public bool isMovement = true;
    public bool isInContainer = false;

    //private float timeContactBar;

    void Start()
    {
        timeContactElem = 0;
        rnd = new System.Random();
        direction.x = (float)rnd.NextDouble() * (2 + 1) - 1;
        direction.y = (float)rnd.NextDouble() * (2 + 1) - 1;
    }


    void FixedUpdate()
    {
        if (isSelectElement == false)
            rb.MovePosition(rb.position + direction.normalized * 3f * Time.fixedDeltaTime);
        if (isInContainer==true)
        {
            Vector2 pointA = new Vector2(gameObject.transform.position.x, ConstantsMiniGame1.upPositionFluctuation);
            Vector2 pointB = new Vector2(gameObject.transform.position.x, ConstantsMiniGame1.downPositionFluctuation);
            StartCoroutine(Startfluctuations(pointA, pointB));
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



    //ScriptedMove

    public void OnPointerClick(PointerEventData eventData)
    {
        ConstantsMiniGame1 c = new ConstantsMiniGame1();      
        if (isSelectElement == false )
        {
            isSelectElement = true;
            RemoveRotation();
            SelectColor(gameObject);
            StartCoroutine(MoveToContainer(c.pos1, c.pos2, c.pos3, c.time1, c.time2, c.time3));
            //RelatedElements.firstElement = gameObject;
        }
    }
    public void RemoveRotation()
    {
        rb.bodyType = RigidbodyType2D.Static;
        gameObject.transform.rotation = ConstantsMiniGame1.rotationElementZero;
    }

    public IEnumerator MoveToContainer(Vector2 pos1, Vector2 pos2, Vector2 pos3, float time1, float time2, float time3)
    {
        yield return StartCoroutine(MoveElement(pos1, time1));
        yield return StartCoroutine(MoveElement(pos2, time2));
        yield return StartCoroutine(MoveElement(pos3+ new Vector2(ConstantsMiniGame1.numberChoice++,0f), time3));
        yield return isInContainer= true;
    }
    IEnumerator MoveElement(Vector2 endPos, float time)
    {
        yield return StartCoroutine(ChangeOscillation(transform, gameObject.transform.position, endPos, time));
    }

    public IEnumerator Startfluctuations(Vector2 pointA, Vector2 pointB)
    {
        while (isSelectElement == true)
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


    //colorswitch
    private void SelectColor(GameObject element)
    {
        StartCoroutine(GradualChangeBlack(element));
    }
    private void OriginColor(GameObject element)
    {
        element.GetComponent<Image>().color = Color.white;
    }

    private IEnumerator GradualChangeBlack(GameObject element)
    {
        float x = 1;
        while (x == 0)
        {
            element.GetComponent<Image>().color = new Color(x, x, x, 1);
            x -= 0.1f;
            yield return null;
        }

    }
}
