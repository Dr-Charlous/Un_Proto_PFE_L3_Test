using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnTriggerEnterScript : MonoBehaviour
{
    public bool isTrigger = false;
    public GameObject ObjectTouch;
    public GameObject ObjectStay;
    public GameObject ObjectLastExit;
    public bool isEnterMode = false;
    public bool isStayMode = false;

    private void OnTriggerEnter(Collider other)
    {
        if (isEnterMode)
        {
            ObjectTouch = other.gameObject;
            isTrigger = true;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (isStayMode)
            ObjectStay = other.gameObject;
    }

    private void OnTriggerExit(Collider other)
    {
        if (isStayMode && other.gameObject == ObjectStay)
            ObjectLastExit = ObjectStay;
        else if (isEnterMode && other.gameObject == ObjectTouch)
            ObjectLastExit = ObjectTouch;

        ObjectStay = null;
        ObjectTouch = null;
        isTrigger = false;
    }
}
