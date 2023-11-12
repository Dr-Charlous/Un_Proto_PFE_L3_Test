using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : MonoBehaviour
{
    public BoxCollider collider;
    public float depthBeforeSubmerged = 1f;
    public float displaycementAmount = 3f;
    public bool Floating = false;

    private void Start()
    {
        collider = GetComponent<BoxCollider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<CharaMove>() != null)
        {
            other.GetComponent<CharaMove>().IsInWater = true;
            other.GetComponent<CharaMove>().water = transform;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.GetComponent<Rigidbody>() != null && other.transform.position.y < 0 && Floating == true)
        {
            float displacementMultiplier = Mathf.Clamp01(-other.transform.position.y / depthBeforeSubmerged) * displaycementAmount;
            other.GetComponent<Rigidbody>().AddForce(new Vector3(0f, Mathf.Abs(Physics.gravity.y) * displacementMultiplier, 0f), ForceMode.Acceleration);


            other.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<CharaMove>() != null)
        {
            other.GetComponent<CharaMove>().IsInWater = false;
            other.GetComponent<CharaMove>().water = null;
        }
    }
}
