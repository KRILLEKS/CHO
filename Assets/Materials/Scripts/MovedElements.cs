using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovedElements : MonoBehaviour
{
    private GameObject obj;
    private Element element;
    public Rigidbody2D rb;
    private Vector2 direction;
    static System.Random rnd ;
    private float timeContactElem;
    private float timeContactBor;
    void Start()
    {
        timeContactBor = 0;
        timeContactElem = 0;
        rnd = new System.Random();
       // Debug.Log(listE.listElements[0].GetComponent<Element>().initialSpeed);
        rb.mass = this.gameObject.GetComponent<SettingElement>().mass;
        rb.collisionDetectionMode=CollisionDetectionMode2D.Continuous;
        direction.x = (float)rnd.NextDouble() * (2 + 1) - 1;
        direction.y = (float)rnd.NextDouble() * (2 + 1) - 1;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rb.MovePosition(rb.position + direction.normalized * this.gameObject.GetComponent<SettingElement>().initialspeed * Time.fixedDeltaTime);
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
           direction.x *= -1;
           direction.y *= -1;

    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        timeContactBor += Time.deltaTime;
        if (timeContactBor>2)
        {
            this.gameObject.transform.position = new Vector2(884, 611);
            timeContactBor = 0;
        }
        else
        direction.x *= -1;
        direction.y *= -1;

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Vector2 dir;
        //    dir.x = 100;
        //  dir.y = 100;
        // rb.AddForce(dir, ForceMode2D.Impulse);
        // direction.x *= -(float)RandomFloatNumbers();
        //  direction.y *= -(float)RandomFloatNumbers();


    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        timeContactElem += Time.deltaTime;
        if (timeContactElem > 2)
        {
            direction.x *= -1;
            direction.y *= -1;
            timeContactElem = 0;
        }

    }
    public double RandomFloatNumbers() 
    {
        double val = rnd.NextDouble() * (2 + 1) - 1;
        return val;
    }
}
