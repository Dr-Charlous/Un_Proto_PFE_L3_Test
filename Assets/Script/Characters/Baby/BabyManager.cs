using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BabyManager : MonoBehaviour
{
    [SerializeField] List<GameObject> BabiesRef;
    [SerializeField] List<GameObject> BabiesView;
    [SerializeField] GameObject BabyPrefab;
    [SerializeField] Transform RespawnPoint;
    [SerializeField] StateBabyController Baby;

    void OnTriggerStay(Collider other)
    {
        var otherBaby = other.GetComponent<StateBabyController>();

        if ( otherBaby != null)
        {
            Baby = otherBaby;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (Baby != null)
        {
            Baby = null;
        }
    }

    public void GetBaby()
    {
        if (Baby != null)
        {
            Baby.ChangeState(Baby.StateRide);

            BabiesRef.Add(Baby.transform.parent.gameObject);
            Baby.transform.parent.gameObject.SetActive(false);

            var baby = Instantiate(BabyPrefab, transform);
            BabiesView.Add(baby);

            baby.transform.rotation = Baby.transform.rotation;
            baby.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);

            Baby = null;

            for (int i = 0; i < BabiesView.Count; i++)
            {
                BabiesView[i].transform.localPosition = Vector3.up * (i/10f - transform.position.y);
            }
        } 
        else if (BabiesRef.Count > 0 && Baby == null)
        {
            BabiesRef[0].SetActive(true);
            BabiesRef[0].GetComponentInChildren<NavMeshAgent>().destination = RespawnPoint.position;

            BabiesRef[0].GetComponentInChildren<NavMeshAgent>().transform.localPosition = Vector3.zero;
            BabiesRef[0].GetComponentInChildren<Gravity>().transform.localPosition = Vector3.zero;

            BabiesRef[0].transform.position = RespawnPoint.position;
            BabiesRef.RemoveAt(0);

            Destroy(BabiesView[0]);
            BabiesView.RemoveAt(0);

            for (int i = 0; i < BabiesView.Count; i++)
            {
                BabiesView[i].transform.localPosition = Vector3.up * (i / 10f - transform.position.y);
            }
        }
    }

    public void BabyStay()
    {
        if (Baby != null)
            Baby.ChangeState(Baby.StateStay);
    }

    public void BabyFollow()
    {
        if (Baby != null)
            Baby.ChangeState(Baby.StateFollow);
    }

    public void BabyAction()
    {
        if (Baby != null)
            Baby.ChangeState(Baby.StateAction);
    }
}
