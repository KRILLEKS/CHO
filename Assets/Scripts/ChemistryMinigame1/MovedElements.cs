using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MovedElements : MonoBehaviour
{
   public Rigidbody2D rb;
   private Vector2 direction;
   private System.Random rnd;
   private float timeContactElem;
   public  bool isSelectElements = false;
   //private float timeContactBar;

   void Start()
   {
      //timeContactBar = 0;
      timeContactElem = 0;
      rnd = new System.Random();
      direction.x = (float) rnd.NextDouble() * (2 + 1) - 1; 
      direction.y = (float) rnd.NextDouble() * (2 + 1) - 1;
   }


   void FixedUpdate()
   {
        if(isSelectElements==false)
      rb.MovePosition(rb.position + direction.normalized * 3f * Time.fixedDeltaTime);
   }

   private void OnTriggerEnter2D(Collider2D collision)
   {
        switch (collision.gameObject.name)
        {
            case "Up":
            case "PipeUp":
                direction.y *= -1;
                break;
            case "Down":
                direction.y *= -1;
                break;
            case "PipeLeft":
            case "Left":
                direction.x *= -1;
                break;
            case "PipeRight":
            case "Right":
                direction.x *= -1;
                break;
        }
    }

   //private void OnTriggerStay2D(Collider2D collision)
   //{
   //   timeContactBar += Time.deltaTime;
   //   if (timeContactBar > 2)
   //   {
   //      this.gameObject.transform.position = new Vector2(884, 611);
   //      timeContactBar = 0;
   //   }

    //}

    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //   // Vector2 dir;
    //   //    dir.x = 100;
    //   //  dir.y = 100;
    //   // rb.AddForce(dir, ForceMode2D.Impulse);
    //   // direction.x *= -(float)RandomFloatNumbers();
    //   //  direction.y *= -(float)RandomFloatNumbers();
    //}

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
    
}
    