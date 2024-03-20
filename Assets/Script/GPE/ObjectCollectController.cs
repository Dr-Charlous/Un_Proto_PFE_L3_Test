using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectCollectController : MonoBehaviour
{
    [SerializeField] CharaMove chara;

    private void Update()
    {
        if (chara.Collected)
        {
            //GrabCheck();
            chara.Collected = false;
        }
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.GetComponent<ObjectCollect>() != null)
        {
            
        }
    }

    private void OnTriggerExit(Collider collider)
    {
        if (collider.GetComponent<ObjectCollect>() != null)
        {
            
        }
    }
}
