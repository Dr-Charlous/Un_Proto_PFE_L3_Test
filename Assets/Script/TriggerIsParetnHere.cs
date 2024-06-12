using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerIsParetnHere : MonoBehaviour
{
    public bool isTrigger = false;

    private void OnTriggerStay(Collider other)
    {
        if (other.GetComponent<CamController>() != null)
            isTrigger = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<CamController>() != null)
            isTrigger = false;
    }
}
