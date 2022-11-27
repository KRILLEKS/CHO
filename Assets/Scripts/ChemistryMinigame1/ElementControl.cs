using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;
using System.Linq;
using System.Threading.Tasks;


public class ElementControl : MonoBehaviour, IPointerClickHandler
{
    [SerializeField]
    GameObject Prefab;
    [SerializeField]
    GameObject elecEffect;
    [SerializeField]
    GameObject elecEffectSphere;
    [SerializeField]
    GameObject elecEffectR;
    public Rigidbody2D rb;
    private Vector2 direction;
    private System.Random rnd;
    public string nameElements;
    public int indexInContainer = 0;
    private int numberGroupLink;
    private int numberChoiceLink;
    public Mode status=Mode.MoveUnselect;
    private GameObject linkElement;
    private GameObject elSphere;
    private GameObject el;
    private GameObject elR;
    private Task[] tasks;
    public enum Mode
    {
        MoveUnselect,
        MoveInContainer,
        StaticFluctuations,
        MoveSelect,
        MoveFromContainer,
        MoveLeftOffset

    }
    public async void PerformStatus(Mode mode)
    {
        switch (mode)
        {
            case Mode.MoveSelect:
                AddElemSelect();
                break;
            case Mode.MoveInContainer:
                await MoveToContainer(ConstantsMiniGame1.posTo1, ConstantsMiniGame1.posTo2, ConstantsMiniGame1.posTo3, ConstantsMiniGame1.posTo4, ConstantsMiniGame1.timeTo1, ConstantsMiniGame1.timeTo2, ConstantsMiniGame1.timeTo3, ConstantsMiniGame1.timeTo4);
                break;
            case Mode.StaticFluctuations:
                await Fluctuation();
                break;
            case Mode.MoveFromContainer:
                await MoveFromContainer(ConstantsMiniGame1.posFrom1, ConstantsMiniGame1.posFrom2, ConstantsMiniGame1.posFrom3, ConstantsMiniGame1.timeFrom1, ConstantsMiniGame1.timeFrom2);
                break;
            case Mode.MoveLeftOffset:
                await OffsetLeftElem(gameObject.transform.position, 1f);
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
        if(gameObject.transform.position.x<-ConstantsMiniGame1.limitX || gameObject.transform.position.x > ConstantsMiniGame1.limitX || gameObject.transform.position.y < -ConstantsMiniGame1.limitY || gameObject.transform.position.y> ConstantsMiniGame1.limitY)
        {
            gameObject.transform.position = ConstantsMiniGame1.posFrom2;
            MoveElement(ConstantsMiniGame1.posFrom3, ConstantsMiniGame1.timeFrom2);
        }
        
        if(linkElement!=null)
            FollowingElec(linkElement);
        if (status==Mode.MoveUnselect|| status == Mode.MoveSelect)
            rb.MovePosition(rb.position + direction.normalized * 3f * Time.fixedDeltaTime);
    } 



//ScriptedMove 
    private void  RemoveRotation()
    {
        rb.bodyType = RigidbodyType2D.Static;
        gameObject.transform.rotation = ConstantsMiniGame1.rotationElementZero;
    }
    private void AddElemSelect()
    {
        DatabaseSubstances.instance.selectedElements[DatabaseSubstances.instance.numberChoice].Add(gameObject);
        DatabaseSubstances.instance.numbersInContainer++;
        numberGroupLink = DatabaseSubstances.instance.numberChoice;
        indexInContainer = DatabaseSubstances.instance.numbersInContainer;
        numberChoiceLink = DatabaseSubstances.instance.selectedElements[numberGroupLink].Count;
        SetupLinkElements(DatabaseSubstances.instance.numberChoice, DatabaseSubstances.instance.selectedElements);
        elSphere=Instantiate(elecEffectSphere, gameObject.transform);
    }
    private void DeleteElemSelect()
    {
        DatabaseSubstances.instance.numbersInContainer--;
        for (int i = 1; i <= DatabaseSubstances.instance.numberChoice; i++)
        {
            DatabaseSubstances.instance.selectedElements[i].Remove(gameObject);
        }
        IndexElementsReduce(DatabaseSubstances.instance.numberChoice, DatabaseSubstances.instance.selectedElements);
        DeleteLinkElements(DatabaseSubstances.instance.numberChoice, DatabaseSubstances.instance.selectedElements);
        Destroy(elSphere);
    }
    private void SetupLinkElements (int numberChoice, Dictionary<int, List<GameObject>> selectedElements)
    {
        for (int i = 1; i <= numberChoice; i++)
        {
            foreach (var c in selectedElements[i])
            {
                if (indexInContainer - c.GetComponent<ElementControl>().indexInContainer == 1 && numberGroupLink == c.GetComponent<ElementControl>().numberGroupLink)
                {
                    c.GetComponent<ElementControl>().linkElement = gameObject;
                }
            }
        }
    }
    private void DeleteLinkElements(int numberChoice, Dictionary<int, List<GameObject>> selectedElements)
    {
        for (int i = 1; i <= numberChoice; i++)
        {
            foreach (var c in selectedElements[i])
            {
                if (indexInContainer - c.GetComponent<ElementControl>().indexInContainer == 1 && numberGroupLink == c.GetComponent<ElementControl>().numberGroupLink)
                {
                    c.GetComponent<ElementControl>().linkElement = linkElement;

                }
            }
        }
        NumberChoiceLinkReduce(DatabaseSubstances.instance.numberChoice, DatabaseSubstances.instance.selectedElements);
        linkElement = null;
    }
    private void NumberChoiceLinkReduce(int numberChoice, Dictionary<int, List<GameObject>> selectedElements)
    {
        for (int i = 1; i <= DatabaseSubstances.instance.numberChoice; i++)
        {
            foreach (var c in DatabaseSubstances.instance.selectedElements[i])
            {
                if (c.GetComponent<ElementControl>().indexInContainer > indexInContainer && numberGroupLink == c.GetComponent<ElementControl>().numberGroupLink)
                {
                    c.GetComponent<ElementControl>().numberChoiceLink--;
                }
            } 
        }
    }
    private void IndexElementsReduce(int numberChoice, Dictionary<int, List<GameObject>> selectedElements)
    {
        for (int i = 1; i <= numberChoice; i++)
        {
            foreach (var c in selectedElements[i])
            {
                if (c.GetComponent<ElementControl>().indexInContainer > indexInContainer && c.GetComponent<ElementControl>().status == Mode.MoveSelect)
                {
                    c.GetComponent<ElementControl>().indexInContainer--;
                }
            }
        }
    }
    private void FollowingElec(GameObject linkElement)
    {
        Vector2 dirBetweenElements;
        int distance = (int)Math.Sqrt(Math.Pow((double)(linkElement.transform.position.x - (double)gameObject.transform.position.x),2)+ Math.Pow((double)(linkElement.transform.position.y - (double)gameObject.transform.position.y), 2));
        dirBetweenElements = gameObject.transform.position - linkElement.transform.position;
        var angle = Mathf.Atan2(dirBetweenElements.y, dirBetweenElements.x) * Mathf.Rad2Deg;
        linkElement.transform.rotation = Quaternion.Euler(0, 0, angle);
        if(distance<2)
        {
            linkElement.transform.position = Vector2.MoveTowards(linkElement.transform.position, transform.position, 2f * Time.deltaTime);
        }
        else
            linkElement.transform.position = Vector2.MoveTowards(linkElement.transform.position,transform.position, 3f*Time.deltaTime);
    }
    private async Task OffsetLeftELements()
    {
        if (numberChoiceLink == DatabaseSubstances.instance.selectedElements[numberGroupLink].Count&& DatabaseSubstances.instance.selectedElements[numberGroupLink].Count!=1)
        {
            Destroy(DatabaseSubstances.instance.selectedElements[numberGroupLink][numberChoiceLink - 2].GetComponent<ElementControl>().el);
        }
        DeleteElemSelect();
        for (int i = 1; i <= DatabaseSubstances.instance.numberChoice; i++)
        {
            foreach (var c in DatabaseSubstances.instance.selectedElements[i])
            {
                if (c.GetComponent<ElementControl>().indexInContainer > indexInContainer && c.GetComponent<ElementControl>().status != Mode.MoveSelect)
                {
                    
                    c.GetComponent<ElementControl>().status = Mode.MoveLeftOffset;
                    c.GetComponent<ElementControl>().indexInContainer--;
                }
            }
            await Task.Yield();
        }
    }
    // корутины перемещения
    private async Task MoveToContainer(Vector2 pos1, Vector2 pos2, Vector2 pos3, Vector2 pos4, float time1, float time2, float time3,float time4)
    {
        linkElement = null;
        RemoveRotation();
        await MoveElement(pos1, time1);
        await MoveElement(pos2, time2);
        ReduceAlpha();
        await MoveElement(pos3, time3);
        Destroy(elSphere);
        await MoveElement(pos4 + new Vector2(indexInContainer, 0f), time4);
        status = Mode.StaticFluctuations;
        AcceptButton.isButtonAccept = false;
        if (numberChoiceLink != DatabaseSubstances.instance.selectedElements[numberGroupLink].Count)
        {
            el = Instantiate(elecEffect, gameObject.transform);
        }
        PerformStatus(status);
    }
    private async Task MoveFromContainer(Vector2 pos1, Vector2 pos2, Vector2 pos3, float time1, float time2)
    {
        Destroy(el);
        await  MoveElement(pos1+new Vector2(gameObject.transform.position.x,0f), time1);
        gameObject.transform.position = pos2;
        IncreaseAlpha();
        await MoveElement(pos3, time2);
        rb.bodyType = RigidbodyType2D.Dynamic;
        status = Mode.MoveUnselect;
        numberChoiceLink = 0;
    }

    private async Task Fluctuation ()
    {
       if(status!=Mode.MoveInContainer||status!=Mode.MoveLeftOffset)
       {
            while (status!=Mode.MoveFromContainer && status != Mode.MoveLeftOffset)
            {
                await MoveElement(gameObject.transform.position + new Vector3(0f, -0.2f, 0f), 1f);
                await MoveElement(gameObject.transform.position - new Vector3(0f, -0.2f, 0f), 1f);
            }
            if (status == Mode.MoveFromContainer)
            {
                PerformStatus(status);
                await OffsetLeftELements ();
            } 
            else
                PerformStatus(status);
       }
    }
    private async Task OffsetLeftElem(Vector2 pos,float time)
    {
        await MoveElement(pos - new Vector2(1f, 0f), time);
        status = Mode.StaticFluctuations;
        PerformStatus(status);
    }
    private async Task MoveElement(Vector2 endPos, float time)
    {
        await Moving(transform, gameObject.transform.position, endPos, time);
    }
    private async Task Moving(Transform thisTransform, Vector2 startPos, Vector2 endPos, float time)
    {
        float i = 0.0f;
        float rate = 1.0f / time;
        while (i < 1.0f)
        {
            i += Time.deltaTime * rate;
            thisTransform.position = Vector2.Lerp(startPos, endPos, i);
            await Task.Yield();
        }
    }

   // //colorswitch
   private void ReduceAlpha()
    {
         gameObject.GetComponent<Image>().color = new Color(gameObject.GetComponent<Image>().color.r, gameObject.GetComponent<Image>().color.g, gameObject.GetComponent<Image>().color.b, 0.5f);
    }
   private void IncreaseAlpha()
    {
        gameObject.GetComponent<Image>().color = new Color(gameObject.GetComponent<Image>().color.r, gameObject.GetComponent<Image>().color.g, gameObject.GetComponent<Image>().color.b, 1f);
}

    
}
