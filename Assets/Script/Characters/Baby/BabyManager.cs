using System.Collections.Generic;
using UnityEngine;

public class BabyManager : MonoBehaviour
{
    [HideInInspector] [SerializeField] List<GameObject> _babiesOnBack;
    [HideInInspector] [SerializeField] List<Transform> _parentOrigin;

    [SerializeField] Transform _parentCharacter;
    [SerializeField] Transform _respawnPoint;
    [SerializeField] NestCreation _nest;
    [SerializeField] float _distanceFromBaby = 1.5f;
    [SerializeField] float _babyOffsetOnBack = 1f;
    [SerializeField] float _distanceAssign = 10;
    public ScriptableDialogue DialogueBabyReccup;

    [Header("BabiesTag :")]
    public int BabieNumberOnBack = 1;
    public List<GameObject> BabiesInScene;

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

    public void BabyFollow()
    {
        StateBabyController Baby = BabiesInScene[0].GetComponentInChildren<StateBabyController>();
        bool isFollowing = false;

        //if (Vector3.Distance(new Vector3(Baby.Parent.position.x, 0, Baby.Parent.position.z), new Vector3(Baby.Target.position.x, 0, Baby.Target.position.z)) < Baby.Distance)
        //{
            if (Baby.currentState != Baby.StateStay)
            {
                isFollowing = false;
            }
            else
            {
                isFollowing = true;
            }

            for (int i = 0; i < BabiesInScene.Count; i++)
            {
                Baby = BabiesInScene[i].GetComponentInChildren<StateBabyController>();

                if (Baby.currentState != Baby.StateStay && !isFollowing)
                    Baby.ChangeState(Baby.StateStay);
                else if (Baby.currentState != Baby.StateFollow)
                    Baby.ChangeState(Baby.StateFollow);
            }
        //}
    }

    public void BabyAction()
    {
        RaycastHit hit;
        Physics.Raycast(transform.position, transform.TransformDirection(Vector3.back), out hit, _distanceAssign);

        if (hit.transform != null && hit.transform.GetComponentInParent<ObjectToPush>() != null)
        {
            //Debug.Log("ObjectToPush");

            ObjectToPush obj = hit.transform.GetComponentInParent<ObjectToPush>();

            for (int i = 0; i < obj.CheckBabies.Length; i++)
            {
                StateBabyController Baby = BabiesInScene[0].GetComponentInChildren<StateBabyController>();

                if (Baby.currentState != Baby.StateRide)
                {
                    Baby.ChangeState(Baby.StateAction);
                    Baby.Target = obj.CheckBabies[i].transform;

                    ChangeOrder();
                }
            }
        }
    }

    public void BabyCollect()
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
                StateBabyController Baby = BabiesInScene[i].GetComponentInChildren<StateBabyController>();

                if (Baby.currentState != Baby.StateRide)
                {
                    Baby.ChangeState(Baby.StateCollect);

                    ChangeOrder();
                    break;
                }
            }
        }
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
        Gizmos.DrawRay(transform.position, transform.TransformDirection(Vector3.back) * _distanceAssign);
    }
}
