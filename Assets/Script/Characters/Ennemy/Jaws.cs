using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jaws : MonoBehaviour
{
    [SerializeField] EnnemyStateController _ennemyStateController;
    public bool IsBitting = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<StateBabyController>() != null || other.GetComponent<CharaMove>() != null)
        {
            Debug.Log("Miamiam");
            IsBitting = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<StateBabyController>() != null || other.GetComponent<CharaMove>() != null)
        {
            IsBitting = false;
        }
    }
}
