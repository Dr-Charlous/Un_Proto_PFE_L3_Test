using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectCollectManager : MonoBehaviour
{
    [SerializeField] bool grab;
    [SerializeField] Vector3 objectGetDiffRot;
    [SerializeField] CharaMove chara;
    [SerializeField] GameObject objectGet;

    private void Start()
    {
        chara = GetComponentInParent<CharaMove>();
    }

    private void Update()
    {
        if (objectGet != null)
        {
            objectGet.transform.position = transform.position;

            objectGetDiffRot = objectGet.GetComponent<ObjectCollect>().RotateObjGrab(transform.rotation.eulerAngles);
            objectGet.transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles + objectGetDiffRot);
        }
    }

    private void OnTriggerStay(Collider collider)
    {
        if (collider.GetComponent<ObjectCollect>() == null)
        {
            chara.Collected = false;
            return;
        }

        if (chara.Collected)
        {
            if (grab)
            {
                objectGet = null;
                grab = false;
            }
            else if (collider.gameObject != objectGet)
            {
                objectGet = collider.gameObject;
                grab = true;
            }

            chara.Collected = false;
        }
    }
}
