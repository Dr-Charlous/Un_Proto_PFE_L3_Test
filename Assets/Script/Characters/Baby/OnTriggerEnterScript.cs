using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnTriggerEnterScript : MonoBehaviour
{
    public bool isTrigger = false;
    public GameObject ObjectTouch;

    private void OnTriggerEnter(Collider other)
    {
        ObjectTouch = other.gameObject;
        isTrigger = true;
    }

    private void OnTriggerExit(Collider other)
    {
        ObjectTouch = null;
        isTrigger = false;
    }
}
