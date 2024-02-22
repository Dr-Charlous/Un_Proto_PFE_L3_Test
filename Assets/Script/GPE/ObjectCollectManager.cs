using UnityEngine;

public class ObjectCollectManager : MonoBehaviour
{
    [SerializeField] bool grab;
    [SerializeField] CharaMove chara;
    public Collider Collider;
    [SerializeField] GameObject objectGetModel;
    [SerializeField] GameObject objectGet;
    [SerializeField] bool isValid = false;

    private void Update()
    {
        if (chara.Collected)
        {
            if (Collider != null)
            {
                if (!grab)
                    Grab(Collider);
                else if (grab)
                    Release();
            }

            chara.Collected = false;
        }
    }

    private void OnTriggerStay(Collider collider)
    {
        if (collider.GetComponent<ObjectCollect>() != null)
        {
            this.Collider = collider;
            isValid = true;
        }
    }

    private void OnTriggerExit(Collider collider)
    {
        if (collider.GetComponent<ObjectCollect>() != null)
        {
            Release();
            this.Collider = null;
            isValid = false;
        }
    }

    void Grab(Collider collider)
    {
        if (collider.gameObject != objectGet && collider.GetComponent<ObjectCollect>() != null)
        {
            objectGetModel = collider.gameObject;
            objectGetModel.SetActive(false);

            objectGet = Instantiate(objectGetModel);
            objectGet.transform.SetParent(chara.transform);
            objectGet.SetActive(true);
            grab = true;
        }
    }

    void Release()
    {
        if (chara.Collected)
        {
            objectGetModel.transform.position = objectGet.transform.position;
            objectGetModel.transform.rotation = objectGet.transform.rotation;

            objectGetModel.SetActive(true);
            objectGetModel = null;

            Destroy(objectGet);
            grab = false;
        }
    }
}
