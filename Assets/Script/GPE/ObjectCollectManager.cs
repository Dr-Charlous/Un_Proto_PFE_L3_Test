using UnityEngine;

public class ObjectCollectManager : MonoBehaviour
{
    [SerializeField] CharaMove chara;
    [SerializeField] Transform parentCharacter;
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

    private void OnTriggerEnter(Collider collider)
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
        if (!grab && objectCollectCollider != null)
            Grab(objectCollectCollider);
        else if (grab)
            Release();
    }

    void Grab(Collider collider)
    {
        ObjectCollect objectInMouth = collider.GetComponent<ObjectCollect>();
        Transform objectTransform = collider.transform;

        if (objectInMouth != null)
        {
            objectGet = objectInMouth.gameObject;
            parentOrigin = objectTransform.parent;
            objectTransform.SetParent(parentCharacter);
            grab = true;
        }
    }

    void Release()
    {
        if (chara.Collected)
        {
            objectGet.transform.SetParent(parentOrigin);
            grab = false;
        }
    }
}
