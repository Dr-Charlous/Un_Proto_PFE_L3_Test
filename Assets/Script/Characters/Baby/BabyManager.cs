using DG.Tweening;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class BabyManager : MonoBehaviour
{
    [HideInInspector][SerializeField] List<GameObject> babiesOnBack;
    [HideInInspector] [SerializeField] List<Transform> ParentOrigin;

    [SerializeField] Transform ParentCharacter;
    [SerializeField] Transform respawnPoint;
    [SerializeField] float DistanceFromBaby = 1.5f;

    [Header("Babies :")]
    public int BabieNumberSelect = 0;
    public int BabieNumberOnBack = 1;
    public GameObject[] BabiesInScene;
    public Material[] BabiesMaterial;

    public void ChangeOutlineBaby(int number, float scale)
    {
        BabiesMaterial[number].SetFloat("_Scale", scale);
    }

    public void CanWeGetBaby(int babyLimit)
    {
        StateBabyController Baby = BabiesInScene[BabieNumberSelect].GetComponentInChildren<StateBabyController>();

        if (Baby != null)
        {
            if (Baby.currentState != Baby.StateRide && babiesOnBack.Count < babyLimit && Vector3.Distance(Baby.transform.position, ParentCharacter.position) <= DistanceFromBaby)
            {
                GrabBaby(Baby);
            }
            else if (babiesOnBack.Count > 0 && Baby.currentState == Baby.StateRide)
            {
                Debug.Log("Release");
                ReleaseBaby();
            }
        }
    }

    void GrabBaby(StateBabyController baby)
    {
        Transform babyTransform = baby.BabyMesh.transform;

        babiesOnBack.Add(babyTransform.gameObject);
        ParentOrigin.Add(babyTransform.parent);
        baby.GetComponent<NavMeshAgent>().Stop();

        baby.ChangeState(baby.StateRide);

        babyTransform.SetParent(ParentCharacter);
        baby.transform.parent.gameObject.SetActive(false);

        for (int i = 0; i < babiesOnBack.Count; i++)
        {
            babiesOnBack[i].transform.localPosition = Vector3.up * (i / 10f - ParentCharacter.position.y);
        }
    }

    void ReleaseBaby()
    {
        Transform babyTransform = babiesOnBack[0].transform;
        babyTransform.SetParent(ParentOrigin[0]);
        ParentOrigin[0].parent.gameObject.SetActive(true);

        StateBabyController babyController = ParentOrigin[0].transform.parent.GetComponentInChildren<StateBabyController>();

        babyTransform.localPosition = Vector3.zero;
        babyTransform.localRotation = Quaternion.Euler(Vector3.zero);

        babyController.transform.parent.transform.position = respawnPoint.position;
        babyController.transform.localPosition = Vector3.zero;
        babyTransform.parent.localPosition = Vector3.zero;

        babyController.ChangeState(babyController.StateAction);

        ParentOrigin.RemoveAt(0);
        babiesOnBack.RemoveAt(0);


        for (int i = 0; i < babiesOnBack.Count; i++)
        {
            babiesOnBack[i].transform.localPosition = Vector3.up * (i / 10f - ParentCharacter.position.y);
        }
    }

    public void BabyFollow()
    {
        StateBabyController Baby = BabiesInScene[BabieNumberSelect].GetComponentInChildren<StateBabyController>();

        if (Baby.currentState != Baby.StateRide)
        {
            if (Baby.currentState == Baby.StateFollow)
                Baby.ChangeState(Baby.StateStay);
            else
                Baby.ChangeState(Baby.StateFollow);
        }
    }

    public void BabyAction()
    {
        StateBabyController Baby = BabiesInScene[BabieNumberSelect].GetComponentInChildren<StateBabyController>();

        if (Baby.currentState != Baby.StateRide)
            Baby.ChangeState(Baby.StateAction);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, DistanceFromBaby);
    }
}
