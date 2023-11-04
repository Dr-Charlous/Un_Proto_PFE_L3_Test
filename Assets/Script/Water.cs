using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : MonoBehaviour
{
    public BoxCollider collider;

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

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<CharaMove>() != null)
        {
            other.GetComponent<CharaMove>().IsInWater = false;
            other.GetComponent<CharaMove>().water = null;
        }
    }
}
