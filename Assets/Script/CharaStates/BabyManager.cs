using Unity.Burst.CompilerServices;
using UnityEngine;

public class BabyManager : MonoBehaviour
{
    Vector3 pos;
    public KeyCode key = KeyCode.LeftShift;
    public float yOffSet = 0.1f;
    public float distance = 1;
    public bool touching = false;

    private void Update()
    {
        SetPos();
    }

    void OnTriggerStay(Collider other)
    {
        if (other.GetComponent<BabyMove>())
        {
            touching = true;

            if (Input.GetKeyDown(key))
            {
                other.GetComponent<BabyMove>().follow = !other.GetComponent<BabyMove>().follow;
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        touching = false;
    }

    void SetPos()
    {
        if (pos != new Vector3(transform.position.x, transform.position.y, transform.position.z))
        {
            pos = new Vector3(transform.position.x, transform.position.y + yOffSet, transform.position.z);
        }
    }
}
