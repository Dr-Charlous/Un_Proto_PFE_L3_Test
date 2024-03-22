using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.XR;

public class ObjectCollectController : MonoBehaviour
{
    [SerializeField] CharaMove _chara;
    [SerializeField] GameObject _objectToGrab;
    public bool[] Grab = new bool[4];

    private void Start()
    {
        for (int i = 0; i < _chara.BabyManager.BabiesInScene.Length - 1; i++)
        {
            Grab[i] = false;
        }
    }

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
        if (!Grab[_chara.BabyManager.BabieNumberSelect])
            GrabOrder();
        else if (Grab[_chara.BabyManager.BabieNumberSelect])
            ReleaseOrder();
    }

    void GrabOrder()
    {
        var target = _objectToGrab;
        var baby = _chara.BabyManager;

        if (target != null)
        {
            baby.BabiesInScene[_chara.BabyManager.BabieNumberSelect].GetComponentInChildren<StateBabyController>().TargetObject = target;
            baby.BabyCollect();
            Grab[_chara.BabyManager.BabieNumberSelect] = true;
        }
    }

    void ReleaseOrder()
    {
        var controller = _chara.BabyManager.BabiesInScene[_chara.BabyManager.BabieNumberSelect].GetComponentInChildren<StateBabyController>();

        if (controller.TargetObject != null)
        {
            controller.TargetObject.transform.SetParent(controller.ParentObject);
            controller.isTransporting = false;
            Grab[_chara.BabyManager.BabieNumberSelect] = false;
        }
    }
}
