using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class BabyManager : MonoBehaviour
{
    public KeyCode Stay = KeyCode.F1;
    public KeyCode Follow = KeyCode.F2;
    public KeyCode Action = KeyCode.F3;
    public KeyCode Ride = KeyCode.F4;

    public bool touching = false;

    public List<GameObject> BabiesRef;
    public List<GameObject> Babies;

    void OnTriggerStay(Collider other)
    {
        if (other.GetComponent<BabyMove>())
        {
            touching = true;

            if (Input.GetKeyDown(Stay) && other.GetComponent<BabyMove>().State != BabyMove.state.Stay)
            {
                other.GetComponent<BabyMove>().State = BabyMove.state.Stay;
            } 
            else if (Input.GetKeyDown(Follow) && other.GetComponent<BabyMove>().State != BabyMove.state.Follow)
            {
                other.GetComponent<BabyMove>().State = BabyMove.state.Follow;
            }
            else if (Input.GetKeyDown(Action) && other.GetComponent<BabyMove>().State != BabyMove.state.Action)
            {
                other.GetComponent<BabyMove>().State = BabyMove.state.Action;
            }

            if (Input.GetKeyDown(Ride) && other.GetComponent<BabyMove>().State != BabyMove.state.Ride)
            {
                other.GetComponent<BabyMove>().State = BabyMove.state.Ride;

                var baby = Instantiate(other.GetComponent<BabyMove>().transform.parent.GetComponentInChildren<SphereCollider>().gameObject, transform.position, Quaternion.identity);

                baby.transform.position = other.GetComponent<BabyMove>().transform.parent.GetComponentInChildren<SphereCollider>().gameObject.transform.position;
                baby.transform.rotation = other.GetComponent<BabyMove>().transform.parent.GetComponentInChildren<SphereCollider>().gameObject.transform.rotation;
                baby.transform.localScale = other.GetComponent<BabyMove>().transform.parent.GetComponentInChildren<SphereCollider>().gameObject.transform.localScale;

                baby.transform.SetParent(transform);
                baby.GetComponentInChildren<BabyMove>().GetComponent<BoxCollider>().enabled = false;

                BabiesRef.Add(other.GetComponent<BabyMove>().transform.parent.gameObject);
                Babies.Add(baby);

                other.GetComponent<BabyMove>().transform.parent.gameObject.SetActive(false);
            }
            else
            {
                var baby = Instantiate(BabiesRef[0]);

                baby.transform.position = Babies[0].gameObject.transform.position;
                baby.transform.rotation = Babies[0].transform.rotation;
                baby.transform.localScale = Babies[0].transform.localScale;

                Babies[0] = null;
                Destroy(Babies[0]);
                Destroy(BabiesRef[0]);
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        touching = false;
    }
}
