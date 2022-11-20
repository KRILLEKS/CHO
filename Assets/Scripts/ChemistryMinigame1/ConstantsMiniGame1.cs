using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstantsMiniGame1 :MonoBehaviour 
{
    //OpenList
    public const float openListX = 5.5f;
    public const float closeListX = 12.5f;
    public const float listY = 0;
    public const float listZ = 0;
    public const float durationList = 1.2f;

    //SelectElement
    //Select
    public const float scaleSelectElementX = 0.35f;
    public const float scaleSelectElementY = 0.35f;
    //Base
    public const float scaleBaseElementX = 1f;
    public const float scaleBaseElementY = 1f;


    public static Quaternion rotationElementZero = new Quaternion(0, 0, 0, 0);

    //Fluctuation
    public const float upPositionFluctuation = -4.3f;
    public const float downPositionFluctuation = -4.55f;
    public const float timeFluctuation = 1.5f;

    //MoveSelectedElement
    public Vector2 pos1= new Vector2 (0f, -1.56f);
    public Vector2 pos2 = new Vector2(0f, -4.48f);
    public Vector2 pos3 = new Vector2(-8.24f, -4.48f);
    public float time1 = 1.5f;
    public float time2 = 1f;
    public float time3 = 4f;

    static public int numberChoice =0; 

}
