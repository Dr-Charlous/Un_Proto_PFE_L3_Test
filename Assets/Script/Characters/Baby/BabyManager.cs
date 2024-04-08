using DG.Tweening;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem.XR;

public class BabyManager : MonoBehaviour
{
    [HideInInspector][SerializeField] List<GameObject> _babiesOnBack;
    [HideInInspector][SerializeField] List<Transform> _parentOrigin;

    [SerializeField] Transform _parentCharacter;
    [SerializeField] Transform _respawnPoint;
    [SerializeField] NestCreation _nest;
    [SerializeField] float _distanceFromBaby = 1.5f;
    [SerializeField] float _babyOffsetOnBack = 1f;

    [Header("Babies :")]
    public int BabieNumberOnBack = 1;
    public List<GameObject> BabiesInScene;

    private void Update()
    {
        if (_nest.IsCreated && !_nest.IsFeed)
        {
            for (int j = 0; j < _babiesOnBack.Count; j++)
            {
                var baby = _babiesOnBack[j].GetComponent<StateBabyController>();

                ReleaseBaby();
            }
        }
    }

    //public void ChangeOutlineBaby(int number, float scale)
    //{
    //    BabiesMaterial[number].SetFloat("_Scale", scale);
    //}

    public void CanWeGetBaby(int babyLimit)
    {
        float distance = 0;
        int j = -1;
        for (int i = 0; i < BabiesInScene.Count; i++)
        {
            float actualDistance = (transform.position - BabiesInScene[i].transform.position).magnitude;

            if (actualDistance < distance && BabiesInScene[i].GetComponentInChildren<StateBabyController>().currentState != BabiesInScene[i].GetComponentInChildren<StateBabyController>().StateRide)
            {
                distance = actualDistance;
                j = i;
            }
        }

        if (j != -1)
        {
            StateBabyController Baby = BabiesInScene[j].GetComponentInChildren<StateBabyController>();

            if (Baby != null)
            {
                if (Baby.currentState != Baby.StateRide && _babiesOnBack.Count < babyLimit && Vector3.Distance(Baby.transform.position, _parentCharacter.position) <= _distanceFromBaby)
                {
                    GrabBaby(Baby);
                }
                else if (_babiesOnBack.Count > 0 && Baby.currentState == Baby.StateRide)
                {
                    ReleaseBaby();
                }
            }
        }
    }

    void GrabBaby(StateBabyController baby)
    {
        Transform babyTransform = baby.BabyMesh.transform;

        _babiesOnBack.Add(babyTransform.gameObject);
        _parentOrigin.Add(babyTransform.parent);
        baby.GetComponent<NavMeshAgent>().isStopped = true;

        baby.ChangeState(baby.StateRide);

        babyTransform.SetParent(_parentCharacter);
        baby.transform.parent.gameObject.SetActive(false);

        BabyOnBackUp(_babyOffsetOnBack);
    }

    void ReleaseBaby()
    {
        Transform babyTransform = _babiesOnBack[0].transform;
        babyTransform.SetParent(_parentOrigin[0]);
        _parentOrigin[0].parent.gameObject.SetActive(true);

        StateBabyController babyController = _parentOrigin[0].transform.parent.GetComponentInChildren<StateBabyController>();

        babyTransform.localPosition = Vector3.zero;
        babyTransform.localRotation = Quaternion.Euler(Vector3.zero);

        babyController.transform.parent.transform.position = _respawnPoint.position;
        babyController.transform.localPosition = Vector3.zero;
        babyTransform.parent.localPosition = Vector3.zero;

        babyController.ChangeState(babyController.StateAction);

        _parentOrigin.RemoveAt(0);
        _babiesOnBack.RemoveAt(0);

        BabyOnBackUp(_babyOffsetOnBack);
    }

    void BabyOnBackUp(float offset)
    {
        for (int i = 0; i < _babiesOnBack.Count; i++)
        {
            _babiesOnBack[i].transform.localPosition = Vector3.up * (i / 10f + offset);
        }
    }

    public void BabyFollow()
    {
        StateBabyController Baby = BabiesInScene[0].GetComponentInChildren<StateBabyController>();
        bool isFollowing = false;

        if (Baby.currentState != Baby.StateRide)
        {
            if (Baby.currentState == Baby.StateFollow)
            {
                Baby.ChangeState(Baby.StateStay);
                isFollowing = false;
            }
            else
            {
                Baby.ChangeState(Baby.StateFollow);
                isFollowing = true;
            }
        }

        for (int i = 1; i < BabiesInScene.Count; i++)
        {
            Baby = BabiesInScene[i].GetComponentInChildren<StateBabyController>();

            if (Baby.currentState != Baby.StateRide)
            {
                if (Baby.currentState != Baby.StateStay && !isFollowing)
                    Baby.ChangeState(Baby.StateStay);
                else if (Baby.currentState != Baby.StateFollow)
                    Baby.ChangeState(Baby.StateFollow);
            }
        }
    }

    public void BabyAction()
    {
        for (int i = 0; i < BabiesInScene.Count; i++)
        {
            StateBabyController Baby = BabiesInScene[i].GetComponentInChildren<StateBabyController>();

            if (Baby.currentState != Baby.StateRide)
            {
                Baby.ChangeState(Baby.StateAction);

                GameObject obj = BabiesInScene[0];
                BabiesInScene.Remove(BabiesInScene[0]);
                BabiesInScene.Add(obj);
                break;
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

                    GameObject obj = BabiesInScene[0];
                    BabiesInScene.Remove(BabiesInScene[0]);
                    BabiesInScene.Add(obj);
                    break;
                }
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, _distanceFromBaby);
    }
}
