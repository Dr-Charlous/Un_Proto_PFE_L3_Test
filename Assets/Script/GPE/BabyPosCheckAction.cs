using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BabyPosCheckAction : MonoBehaviour
{
    public bool IsBabyActionned;
    [SerializeField] StateBabyController _isBabyOccupied;

    private void OnTriggerStay(Collider other)
    {
        StateBabyController baby = other.transform.parent.GetComponentInChildren<StateBabyController>();
        CharaMove mama = other.GetComponent<CharaMove>();

        if (mama != null)
        {
            IsBabyActionned = false;
            _isBabyOccupied = null;
        }
        else if (baby != null)
        {
            if (baby.currentState == baby.StateAction)
            {
                //baby.IsParalysed = true;
                IsBabyActionned = true;
                _isBabyOccupied = baby;
            }
            else
            {
                //baby.IsParalysed = false;
                IsBabyActionned = false;
                _isBabyOccupied = null;
            }
        }
    }
}
