using System.Collections.Generic;
using UnityEngine;

public class BabyManager : MonoBehaviour
{
    [SerializeField] bool touching = false;

    [SerializeField] List<GameObject> BabiesRef;
    [SerializeField] Transform RespawnPoint;
    [SerializeField] BabyMove Baby;

    void OnTriggerStay(Collider other)
    {
        var otherBaby = other.GetComponent<BabyMove>();

        if ( otherBaby != null)
        {
            Baby = otherBaby;
            touching = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        touching = false;
    }

    public void GetBaby()
    {
        if (Baby != null)
        {
            Baby.State = BabyMove.state.Ride;

            BabiesRef.Add(Baby.transform.parent.gameObject);
            Baby.transform.parent.gameObject.SetActive(false);
            Baby = null;
        }
    }

    public void BabyStay()
    {
        if (BabiesRef.Count > 0 && Baby.State == BabyMove.state.Ride)
        {
            BabiesRef[0].SetActive(true);
            BabiesRef[0].transform.position = RespawnPoint.position;
            BabiesRef.RemoveAt(0);
        }

        if (Baby != null)
            Baby.State = BabyMove.state.Stay;
    }

    public void BabyFollow()
    {
        if (BabiesRef.Count > 0 && Baby.State == BabyMove.state.Ride)
        {
            BabiesRef[0].SetActive(true);
            BabiesRef[0].transform.position = RespawnPoint.position;
            BabiesRef.RemoveAt(0);
        }

        if (Baby != null)
            Baby.State = BabyMove.state.Follow;
    }

    public void BabyAction()
    {
        if (BabiesRef.Count > 0 && Baby.State == BabyMove.state.Ride)
        {
            BabiesRef[0].SetActive(true);
            BabiesRef[0].transform.position = RespawnPoint.position;
            BabiesRef.RemoveAt(0);
        }

        if (Baby != null)
            Baby.State = BabyMove.state.Action;
    }
}
