using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.XR;

public class ObjectCollectController : MonoBehaviour
{
    [SerializeField] CharaMove _chara;
    [SerializeField] GameObject _objectToGrab;
    public bool Grab = false;

    private void Update()
    {
        if (_chara.Collected)
        {
            GrabCheck();
            _chara.Collected = false;
        }
    }

    private void OnTriggerEnter(Collider collider)
    {
        ObjectCollect obj = collider.GetComponent<ObjectCollect>();

        if (obj != null)
        {
            obj.ChangeOutlineObject(obj.MaterialOutline, 1.1f);
            _objectToGrab = collider.gameObject;
        }
    }

    private void OnTriggerExit(Collider collider)
    {
        ObjectCollect obj = collider.GetComponent<ObjectCollect>();

        if (obj != null)
        {
            obj.ChangeOutlineObject(obj.MaterialOutline, 0f);
        }
    }

    void GrabCheck()
    {
        if (!Grab)
            GrabOrder();
        else if (Grab)
            ReleaseOrder();
    }

    void GrabOrder()
    {
        _chara.BabyManager.BabiesInScene[_chara.BabyManager.BabieNumberSelect].GetComponentInChildren<StateBabyController>().TargetObject = _objectToGrab;
        _chara.BabyManager.BabyCollect();
        Grab = true;
    }

    void ReleaseOrder()
    {
        var controller = _chara.BabyManager.BabiesInScene[_chara.BabyManager.BabieNumberSelect].GetComponentInChildren<StateBabyController>();

        controller.TargetObject.transform.SetParent(controller.ParentObject);
        controller.isTransporting = false;
        Grab = false;
    }
}
