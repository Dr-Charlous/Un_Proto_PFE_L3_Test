using UnityEngine;

public class ObjectCollectManager : MonoBehaviour
{
    [SerializeField] bool grab;
    [SerializeField] CharaMove chara;
    [SerializeField] Collider collider;
    [SerializeField] GameObject objectGetModel;
    [SerializeField] GameObject objectGet;

    private void Start()
    {
        chara = GetComponentInParent<CharaMove>();
    }

    private void Update()
    {
        if (chara.Collected)
        {
            if (collider != null)
            {
                Grab(collider);
            }

            chara.Collected = false;
        }
    }

    private void OnTriggerStay(Collider collider)
    {
        if (collider.GetComponent<ObjectCollect>() != null)
        {
            this.collider = collider;
        }

    }

    void Grab(Collider collider)
    {
        if (!grab && collider.gameObject != objectGet && collider.GetComponent<ObjectCollect>() != null)
        {
            objectGetModel = collider.gameObject;
            objectGetModel.SetActive(false);

            objectGet = Instantiate(objectGetModel);
            objectGet.transform.SetParent(chara.transform);
            objectGet.SetActive(true);
            grab = true;
        }
        else if (chara.Collected && grab)
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
