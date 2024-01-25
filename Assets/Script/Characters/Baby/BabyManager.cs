using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BabyManager : MonoBehaviour
{
    [SerializeField] List<GameObject> BabiesRef;
    [SerializeField] Transform RespawnPoint;
    [SerializeField] BabyMove Baby;

    void OnTriggerStay(Collider other)
    {
        var otherBaby = other.GetComponent<BabyMove>();

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
            Baby.State = BabyMove.state.Ride;

            BabiesRef.Add(Baby.transform.parent.gameObject);
            Baby.transform.parent.gameObject.SetActive(false);
            Baby = null;
        } else if (BabiesRef.Count > 0 && Baby == null)
        {
            BabiesRef[0].SetActive(true);
            BabiesRef[0].GetComponentInChildren<NavMeshAgent>().destination = RespawnPoint.position;
            BabiesRef[0].transform.position = RespawnPoint.position;
            BabiesRef.RemoveAt(0);
        }
    }

    public void BabyStay()
    {
        if (Baby != null)
            Baby.State = BabyMove.state.Stay;
    }

    public void BabyFollow()
    {
        if (Baby != null)
            Baby.State = BabyMove.state.Follow;
    }

    public void BabyAction()
    {
        if (Baby != null)
            Baby.State = BabyMove.state.Action;
    }
}
