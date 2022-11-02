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
    public const float scaleSelectElementZ = 0f;
    //Base
    public const float scaleBaseElementX = 1f;
    public const float scaleBaseElementY = 1f;
    public const float scaleBaseElementZ = 0f;

    public static Quaternion rotationElementZero = new Quaternion(0, 0, 0, 0);

    //Fluctuation
    public const float upPositionFluctuation = -4.3f;
    public const float downPositionFluctuation = -4.55f;
    public const float zPositionFluctuation = 0f;
    public const float timeFluctuation = 1.5f;




}
