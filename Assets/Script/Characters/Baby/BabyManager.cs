using System.Collections.Generic;
using UnityEngine;

public class BabyManager : MonoBehaviour
{
    public float DistanceAssign = 10;
    public List<GameObject> BabiesInScene;

    [SerializeField] float _distanceFromBaby = 1.5f;

    #region comm
    //private void Update()
    //{
    //    if (_nest.IsCreated && !_nest.IsFeed)
    //    {
    //        for (int j = 0; j < _babiesOnBack.Count; j++)
    //        {
    //            var baby = _babiesOnBack[j].GetComponent<StateBabyController>();

    //            ReleaseBaby();
    //        }
    //    }
    //}

    //public void ChangeOutlineBaby(int number, float scale)
    //{
    //    BabiesMaterial[number].SetFloat("_Scale", scale);
    //}

    //public void CanWeGetBaby(int babyLimit)
    //{
    //    float distance = 0;
    //    int j = -1;
    //    for (int i = 0; i < BabiesInScene.Count; i++)
    //    {
    //        float actualDistance = (transform.position - BabiesInScene[i].transform.position).magnitude;

    //        if (actualDistance < distance && BabiesInScene[i].GetComponentInChildren<StateBabyController>().currentState != BabiesInScene[i].GetComponentInChildren<StateBabyController>().StateRide)
    //        {
    //            distance = actualDistance;
    //            j = i;
    //        }
    //    }

    //    if (j != -1)
    //    {
    //        StateBabyController Baby = BabiesInScene[j].GetComponentInChildren<StateBabyController>();

    //        if (Baby != null)
    //        {
    //            if (Baby.currentState != Baby.StateRide && _babiesOnBack.Count < babyLimit && Vector3.Distance(Baby.transform.position, _parentCharacter.position) <= _distanceFromBaby)
    //            {
    //                GrabBaby(Baby);
    //            }
    //            else if (_babiesOnBack.Count > 0 && Baby.currentState == Baby.StateRide)
    //            {
    //                ReleaseBaby();
    //            }
    //        }
    //    }
    //}

    //void GrabBaby(StateBabyController baby)
    //{
    //    Transform babyTransform = baby.BabyMesh.transform;

    //    _babiesOnBack.Add(babyTransform.gameObject);
    //    _parentOrigin.Add(babyTransform.parent);
    //    baby.GetComponent<NavMeshAgent>().isStopped = true;

    //    baby.ChangeState(baby.StateRide);

    //    babyTransform.SetParent(_parentCharacter);
    //    baby.transform.parent.gameObject.SetActive(false);

    //    BabyOnBackUp(_babyOffsetOnBack);
    //}

    //void ReleaseBaby()
    //{
    //    Transform babyTransform = _babiesOnBack[0].transform;
    //    babyTransform.SetParent(_parentOrigin[0]);
    //    _parentOrigin[0].parent.gameObject.SetActive(true);

    //    StateBabyController babyController = _parentOrigin[0].transform.parent.GetComponentInChildren<StateBabyController>();

    //    babyTransform.localPosition = Vector3.zero;
    //    babyTransform.localRotation = Quaternion.Euler(Vector3.zero);

    //    babyController.transform.parent.transform.position = _respawnPoint.position;
    //    babyController.transform.localPosition = Vector3.zero;
    //    babyTransform.parent.localPosition = Vector3.zero;

    //    babyController.ChangeState(babyController.StateAction);

    //    _parentOrigin.RemoveAt(0);
    //    _babiesOnBack.RemoveAt(0);

    //    BabyOnBackUp(_babyOffsetOnBack);
    //}

    //void BabyOnBackUp(float offset)
    //{
    //    for (int i = 0; i < _babiesOnBack.Count; i++)
    //    {
    //        _babiesOnBack[i].transform.localPosition = Vector3.up * (i / 10f + offset);
    //    }
    //}
    #endregion

    public void BabyAction()
    {
        RaycastHit hit;
        Physics.Raycast(GameManager.Instance.Character.transform.position, GameManager.Instance.Character.transform.TransformDirection(Vector3.back), out hit, DistanceAssign, LayerMask.GetMask("Ground"));

        if (hit.transform != null && hit.transform.GetComponentInParent<ObjectToPush>() != null)
        {
            //Debug.Log("ObjectToPush");

            ObjectToPush obj = hit.transform.GetComponentInParent<ObjectToPush>();

            for (int i = 0; i < obj.CheckBabies.Length; i++)
            {
                StateBabyController Baby = BabiesInScene[0].GetComponentInChildren<StateBabyController>();

                Baby.ChangeState(Baby.StateAction);
                Baby.Target = obj.CheckBabies[i].transform;

                ChangeOrder();
            }
        }
    }

    public bool BabyCollect()
    {
        bool isCollecting = false;

        for (int i = 0; i < BabiesInScene.Count; i++)
        {
            StateBabyController Baby = BabiesInScene[i].GetComponentInChildren<StateBabyController>();

            if (Baby.currentState == Baby.StateCollect)
            {
                isCollecting = true;
            }
        }

        if (!isCollecting)
        {
            for (int i = 0; i < BabiesInScene.Count; i++)
            {
                StateBabyController Baby = BabiesInScene[0].GetComponentInChildren<StateBabyController>();

                Baby.ChangeState(Baby.StateCollect);

                ChangeOrder();
                break;
            }
        }

        return isCollecting;
    }

    public void ChangeOrder()
    {
        GameObject baby = BabiesInScene[0];
        BabiesInScene.Remove(BabiesInScene[0]);
        BabiesInScene.Add(baby);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, _distanceFromBaby);
        Gizmos.color = Color.cyan;
        if (Application.isPlaying)
            Gizmos.DrawRay(GameManager.Instance.Character.transform.position, GameManager.Instance.Character.transform.TransformDirection(Vector3.back) * DistanceAssign);
    }
}
