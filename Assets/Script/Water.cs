using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : MonoBehaviour
{
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
