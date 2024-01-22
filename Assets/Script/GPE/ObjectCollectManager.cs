using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectCollectManager : MonoBehaviour
{
    [SerializeField] bool grab;
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
        }
    }

    private void OnTriggerStay(Collider collider)
    {
        if (collider.GetComponent<ObjectCollect>() == null)
            return;

        if (chara.Collected)
        {
            if (grab)
            {
                objectGet = null;
                grab = false;
            }
            else
            {
                objectGet = collider.gameObject;
                grab = true;
            }
        }
    }
}
