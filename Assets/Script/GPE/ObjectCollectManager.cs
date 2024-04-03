using UnityEngine;

public class ObjectCollectManager : MonoBehaviour
{
    [SerializeField] CharaMove chara;
    [SerializeField] Transform parentCharacter;
    [SerializeField] GameObject ObjectFish;
    Transform parentOrigin;
    GameObject objectGet;
    Collider objectCollectCollider;
    bool grab;

    private void Update()
    {
        if (chara.Collected)
        {
            GrabCheck();
            chara.Collected = false;
        }
    }

    private void OnTriggerStay(Collider collider)
    {
        if (collider.GetComponent<ObjectCollect>() != null)
        {
            objectCollectCollider = collider;
        }
    }

    private void OnTriggerExit(Collider collider)
    {
        if (collider.GetComponent<ObjectCollect>() != null)
        {
            Release();
            objectCollectCollider = null;
        }
    }

    void GrabCheck()
    {
        if (grab == false && objectCollectCollider != null)
            Grab(objectCollectCollider);
        else if (grab)
            Release();
    }

    void Grab(Collider collider)
    {
        ObjectCollect objectInMouth = collider.GetComponent<ObjectCollect>();
        Transform objectTransform = collider.transform;

        if (objectInMouth != null && objectInMouth.gameObject == ObjectFish)
        {
            objectGet = objectInMouth.gameObject;
            parentOrigin = objectTransform.parent;
            objectTransform.SetParent(parentCharacter);
            grab = true;
        }
    }

    void Release()
    {
        objectGet.transform.parent = parentOrigin;
        grab = false;
    }
}
