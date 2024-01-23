using Unity.Burst.CompilerServices;
using UnityEngine;

public class BabyManager : MonoBehaviour
{
    public KeyCode key = KeyCode.LeftShift;
    public bool touching = false;
    public bool touch = false;

    void OnTriggerStay(Collider other)
    {
        if (other.GetComponent<BabyMove>())
        {
            touching = true;

            if (Input.GetKeyDown(key) && touch == false)
            {
                other.GetComponent<BabyMove>().follow = !other.GetComponent<BabyMove>().follow;
                touch = true;
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        touching = false;
        touch = false;
    }
}
