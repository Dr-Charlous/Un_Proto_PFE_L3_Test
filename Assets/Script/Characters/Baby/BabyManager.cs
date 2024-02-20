using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BabyManager : MonoBehaviour
{
    [SerializeField] List<GameObject> babiesRef;
    [SerializeField] List<GameObject> babiesView;
    [SerializeField] GameObject babyPrefab;
    [SerializeField] Transform respawnPoint;
    public float DistanceFromBaby = 1.5f;
    public CharaMove Chara;

    public void GetBaby(int babyLimit)
    {
        var Baby = Chara.Babies[Chara.BabieNumber].GetComponentInChildren<StateBabyController>();

        if (Baby.currentState != Baby.StateRide && babiesRef.Count < babyLimit && Vector3.Distance(Baby.transform.position, transform.position) <= DistanceFromBaby)
        {
            Baby.ChangeState(Baby.StateRide);

            babiesRef.Add(Baby.transform.parent.gameObject);
            Baby.transform.parent.gameObject.SetActive(false);

            var baby = Instantiate(babyPrefab, transform);
            babiesView.Add(baby);

            baby.transform.rotation = Baby.transform.rotation;
            baby.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);

            for (int i = 0; i < babiesView.Count; i++)
            {
                babiesView[i].transform.localPosition = Vector3.up * (i / 10f - transform.position.y);
            }
        }
        else if (babiesRef.Count > 0 && Baby.currentState == Baby.StateRide)
        {
            babiesRef[0].SetActive(true);
            babiesRef[0].GetComponentInChildren<NavMeshAgent>().destination = respawnPoint.position;

            babiesRef[0].GetComponentInChildren<NavMeshAgent>().transform.localPosition = Vector3.zero;

            babiesRef[0].GetComponentInChildren<Gravity>().transform.localPosition = Vector3.zero;
            babiesRef[0].GetComponentInChildren<StateBabyController>().ChangeState(babiesRef[0].GetComponentInChildren<StateBabyController>().StateAction);

            babiesRef[0].transform.position = respawnPoint.position;
            babiesRef.RemoveAt(0);

            Destroy(babiesView[0]);
            babiesView.RemoveAt(0);


            for (int i = 0; i < babiesView.Count; i++)
            {
                babiesView[i].transform.localPosition = Vector3.up * (i / 10f - transform.position.y);
            }
        }
    }

    public void BabyFollow()
    {
        var Baby = Chara.Babies[Chara.BabieNumber].GetComponentInChildren<StateBabyController>();

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
        var Baby = Chara.Babies[Chara.BabieNumber].GetComponentInChildren<StateBabyController>();

        if (Baby.currentState != Baby.StateRide)
            Baby.ChangeState(Baby.StateAction);
    }
}
