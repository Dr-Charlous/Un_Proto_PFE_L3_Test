using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.XR;

public class ObjectCollectController : MonoBehaviour
{
    [SerializeField] CharaMove _chara;
    [SerializeField] GameObject _objectToGrab;

    private void Start()
    {
        for (int i = 0; i < _chara.BabyManager.BabiesInScene.Count - 1; i++)
        {
            _chara.BabyManager.BabiesInScene[i].GetComponentInChildren<StateBabyController>().isGrab = false;
        }
    }

    private void Update()
    {
        if (_chara.CollectedBabies)
        {
            GrabCheck(0);
            _chara.CollectedBabies = false;
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

    void GrabCheck(int i)
    {
        if (!_chara.BabyManager.BabiesInScene[i].GetComponentInChildren<StateBabyController>().isGrab)
            GrabOrder(i);
        else if (_chara.BabyManager.BabiesInScene[i].GetComponentInChildren<StateBabyController>().isGrab)
            ReleaseOrder(i);
    }

    void GrabOrder(int i)
    {
        var target = _objectToGrab;
        var baby = _chara.BabyManager;

        if (target != null)
        {
            baby.BabiesInScene[i].GetComponentInChildren<StateBabyController>().TargetObject = target;
            baby.BabyCollect();
            _chara.BabyManager.BabiesInScene[baby.BabiesInScene.Count - 1].GetComponentInChildren<StateBabyController>().isGrab = true;
        }
    }

    void ReleaseOrder(int i)
    {
        var controller = _chara.BabyManager.BabiesInScene[i].GetComponentInChildren<StateBabyController>();

        if (controller.TargetObject != null)
        {
            controller.TargetObject.transform.SetParent(controller.ParentObject);
            controller.TargetObject.GetComponent<BoxCollider>().excludeLayers += LayerMask.GetMask("Player");
            controller.isTransporting = false;
            _chara.BabyManager.BabiesInScene[i].GetComponentInChildren<StateBabyController>().isGrab = false;
        }
    }
}
