using UnityEngine;

public class ObjectCollectController : MonoBehaviour
{
    public GameObject ObjectToGrab;
    public Material MaterialOutline;

    private void Start()
    {
        for (int i = 0; i < GameManager.Instance.BabyManager.BabiesInScene.Count - 1; i++)
        {
            GameManager.Instance.BabyManager.BabiesInScene[i].GetComponentInChildren<StateBabyController>().isGoingToGrab = false;
            GameManager.Instance.BabyManager.BabiesInScene[i].GetComponentInChildren<StateBabyController>().isTransporting = false;
        }

        MaterialOutline.SetInt("_IsActive", 0);
    }

    private void Update()
    {
        if (GameManager.Instance.Character.InputCollectBabies)
        {
            GrabCheck(0);
            GameManager.Instance.Character.InputCollectBabies = false;
        }

        if (ObjectToGrab != null && !ObjectToGrab.gameObject.activeInHierarchy)
            ObjectToGrab = null;
    }

    private void OnTriggerStay(Collider collider)
    {
        ObjectCollect obj = collider.GetComponent<ObjectCollect>();

        if (obj != null)
        {
            if (obj.UiFollow != null)
                obj.UiFollow.ShowUi(true);

            ObjectToGrab = collider.gameObject;
        }

        if (obj != null || collider.GetComponent<ObjectToPush>() || collider.GetComponent<ObjectResonnance>())
            MaterialOutline.SetInt("_IsActive", 1);

        if (collider.GetComponentInChildren<UiFollowing>() != null)
        {
            collider.GetComponentInChildren<UiFollowing>().ShowUi(true);
        }
    }

    private void OnTriggerExit(Collider collider)
    {
        ObjectCollect obj = collider.GetComponent<ObjectCollect>();

        if (obj != null)
        {
            if (obj.UiFollow != null)
                obj.UiFollow.ShowUi(false);

            ObjectToGrab = null;
        }

        MaterialOutline.SetInt("_IsActive", 0);

        if (collider.GetComponentInChildren<UiFollowing>() != null)
        {
            collider.GetComponentInChildren<UiFollowing>().ShowUi(false);
        }
    }

    void GrabCheck(int i)
    {
        if (!GameManager.Instance.BabyManager.BabiesInScene[i].GetComponentInChildren<StateBabyController>().isGoingToGrab && !GameManager.Instance.BabyManager.BabiesInScene[i].GetComponentInChildren<StateBabyController>().isTransporting)
            GrabOrder(i);
    }

    void GrabOrder(int i)
    {
        var target = ObjectToGrab;
        var baby = GameManager.Instance.BabyManager;

        if (target != null && !target.GetComponent<ObjectCollect>().isCollected)
        {
            target.GetComponentInChildren<ObjectCollect>().UiFollow.gameObject.SetActive(false);

            baby.BabiesInScene[i].GetComponentInChildren<StateBabyController>().TargetObject = target;
            GameManager.Instance.BabyManager.BabiesInScene[i].GetComponentInChildren<StateBabyController>().isGoingToGrab = true;

            if (baby.BabyCollect())
            {
                target.GetComponentInChildren<ObjectCollect>().UiFollow.gameObject.SetActive(true);

                baby.BabiesInScene[i].GetComponentInChildren<StateBabyController>().TargetObject = null;
            }
        }
    }
}
