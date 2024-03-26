using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BabyPosCheckAction : MonoBehaviour
{
    public bool IsBabyActionned;
    [SerializeField] StateBabyController _isBabyOccupied;

    private void OnTriggerStay(Collider other)
    {
        StateBabyController baby = null;
        baby = other.transform.parent.GetComponentInChildren<StateBabyController>();

        if (baby != null && baby != _isBabyOccupied)
        {
            if (baby.currentState == baby.StateAction)
            {
                baby.IsParalysed = true;
                IsBabyActionned = true;
                _isBabyOccupied = baby;
            }
            else if (baby == null)
            {
                IsBabyActionned = false;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        StateBabyController baby = null;
        baby = other.transform.parent.GetComponentInChildren<StateBabyController>();

        if (baby != null)
        {
            _isBabyOccupied = null;
            baby.IsParalysed = false;
            IsBabyActionned = false;
        }
    }
}
