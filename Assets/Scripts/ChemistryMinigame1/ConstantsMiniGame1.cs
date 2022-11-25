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

//MoveToContainer
    static public Vector2 posTo1= new Vector2 (0f, -1.56f);
    static public Vector2 posTo2 = new Vector2(0f, -4.3f);
    static public Vector2 posTo3 = new Vector2(-8.24f, -4.3f);
    static public float timeTo1 = 1.5f;
    static public float timeTo2 = 1f;
    static public float timeTo3 = 2f;
//MoveFromContainer
    static public Vector2 posFrom1 = new Vector2(0f, -6f);
    static public Vector2 posFrom2 = new Vector2(0f, 6f);
    static public Vector2 posFrom3 = new Vector2(0f, 3.42f);
    static public float timeFrom1 = 1.5f;
    static public float timeFrom2 = 1f;
    static public float timeFrom3 = 2f;
//EndLevel
    public const float openEndLevelWindowX = 0;
    public const float endLevelWindowY = 0;
    public const float endLevelWindowZ = 0;
    public const float durationEndLevelWindow = 2f;

}
