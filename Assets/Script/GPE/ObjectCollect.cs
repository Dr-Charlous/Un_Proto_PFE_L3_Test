using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectCollect : MonoBehaviour
{
    [SerializeField] Vector3 ObjRot;
    [SerializeField] Vector3 PlayerRot;
    [SerializeField] Vector3 DiffRot;

    private void Start()
    {
        ObjRot = transform.rotation.eulerAngles;
    }

    public Vector3 RotateObjGrab(Vector3 PlayerRot)
    {
        DiffRot = PlayerRot - ObjRot;

        DiffRot.y = 0;

        return DiffRot;
    }
}
