using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateObjectRight : MonoBehaviour
{
    [SerializeField]
    GameObject objectPercentCircle;
    void FixedUpdate()
    {
        gameObject.transform.Rotate(0, 0, -0.2f);
    }
}
