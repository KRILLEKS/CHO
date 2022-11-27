using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstantsMiniGame1 :MonoBehaviour 
{
//OpenList
    static public Vector3 posOpenList = new Vector3(5.5f, 0f, 0f);
    static public Vector3 posCloseList = new Vector3(12.5f, 0f, 0f);
    static public Vector3 posCloseListButton = new Vector3(10.02f, 4.02f, 0f);
    static public Vector3 posOpenListButton = new Vector3(7.98f, 4.02f, 0f);
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
    static public Vector2 posTo2 = new Vector2(0f, -3.52f);
    static public Vector2 posTo3 = new Vector2(0f, -4.3f);
    static public Vector2 posTo4 = new Vector2(-8.24f, -4.3f);

    static public float timeTo1 = 1.5f;
    static public float timeTo2 = 0.5f;
    static public float timeTo3 = 1f;
    static public float timeTo4 = 2f;
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
//AlertWindow
    static public Vector3 posOpenAlertWindow = new Vector3(5.94f, -2.18f, 0f);
    static public Vector3 posCloseAlertWindow = new Vector3(11.78f, -2.18f, 0f);
    static public Vector3 posCloseAlertButton = new Vector3(11.12f, -3.52f, 0f);
    static public Vector3 posOpenAlertButton = new Vector3(7.12f, -3.52f, 0f);
    static public float durationAlertWindow = 2f;
    //limit
    public const float limitX = 11f;
    public const float limitY = 8f;
}
