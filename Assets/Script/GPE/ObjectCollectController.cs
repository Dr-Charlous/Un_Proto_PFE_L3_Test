using UnityEngine;

public class ObjectCollectController : MonoBehaviour
{
    public GameObject ObjectToGrab;

    private void Start()
    {
        for (int i = 0; i < GameManager.Instance.BabyManager.BabiesInScene.Count - 1; i++)
        {
            GameManager.Instance.BabyManager.BabiesInScene[i].GetComponentInChildren<StateBabyController>().isGrab = false;
        }
    }

    private void Update()
    {
        if (GameManager.Instance.Character.CollectedBabies)
        {
            GrabCheck(0);
            GameManager.Instance.Character.CollectedBabies = false;
        }
    }

    private void OnTriggerStay(Collider collider)
    {
        ObjectCollect obj = collider.GetComponent<ObjectCollect>();

        if (obj != null)
        {
            //obj.ChangeOutlineObject(obj.MaterialOutline, 1.1f);
            obj.UiFollow.ShowUi(true);
            ObjectToGrab = collider.gameObject;
        }

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
            //obj.ChangeOutlineObject(obj.MaterialOutline, 0f);
            obj.UiFollow.ShowUi(false);
            ObjectToGrab = null;
        }

        if (collider.GetComponentInChildren<UiFollowing>() != null)
        {
            collider.GetComponentInChildren<UiFollowing>().ShowUi(false);
        }
    }

    void GrabCheck(int i)
    {
        if (!GameManager.Instance.BabyManager.BabiesInScene[i].GetComponentInChildren<StateBabyController>().isGrab)
            GrabOrder(i);
    }

    void GrabOrder(int i)
    {
        var target = ObjectToGrab;
        var baby = GameManager.Instance.BabyManager;

        if (target != null)
        {
            target.GetComponentInChildren<ObjectCollect>().UiFollow.gameObject.SetActive(false);

            baby.BabiesInScene[i].GetComponentInChildren<StateBabyController>().TargetObject = target;
            baby.BabyCollect();
            GameManager.Instance.BabyManager.BabiesInScene[baby.BabiesInScene.Count - 1].GetComponentInChildren<StateBabyController>().isGrab = true;
        }
    }
}
