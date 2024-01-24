using System.Collections.Generic;
using UnityEngine;

public class BabyManager : MonoBehaviour
{
    [SerializeField] bool touching = false;

    [SerializeField] List<GameObject> BabiesRef;
    [SerializeField] Transform RespawnPoint;
    [SerializeField] BabyMove Baby;
    [SerializeField] int Babies = 0;

    void OnTriggerStay(Collider other)
    {
        if (other.GetComponent<BabyMove>())
        {
            Baby = other.GetComponent<BabyMove>();
            touching = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        touching = false;
    }

    public void GetBaby()
    {
        Baby.State = BabyMove.state.Ride;

        BabiesRef.Add(Baby.transform.parent.gameObject);
        Baby.transform.parent.gameObject.SetActive(false);
        Babies++;
    }

    public void BabyStay()
    {
        if (Babies > 0 && Baby.State == BabyMove.state.Ride)
        {
            Babies--;
            BabiesRef[0].SetActive(true);
            BabiesRef[0].transform.position = RespawnPoint.position;
            BabiesRef.RemoveAt(0);
        }

        Baby.State = BabyMove.state.Stay;
    }

    public void BabyFollow()
    {
        if (Babies > 0 && Baby.State == BabyMove.state.Ride)
        {
            Babies--;
            BabiesRef[0].SetActive(true);
            BabiesRef[0].transform.position = RespawnPoint.position;
            BabiesRef.RemoveAt(0);
        }

        Baby.State = BabyMove.state.Follow;
    }

    public void BabyAction()
    {
        if (Babies > 0 && Baby.State == BabyMove.state.Ride)
        {
            Babies--;
            BabiesRef[0].SetActive(true);
            BabiesRef[0].transform.position = RespawnPoint.position;
            BabiesRef.RemoveAt(0);
        }

        Baby.State = BabyMove.state.Action;
    }
}
