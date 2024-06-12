using UnityEngine;

public class BabyPosCheckAction : MonoBehaviour
{
    public bool IsBabyActionned;
    [SerializeField] StateBabyController _isBabyOccupied;

    private void OnTriggerStay(Collider other)
    {
        StateBabyController baby = other.transform.parent.GetComponentInChildren<StateBabyController>();
        CamController mama = other.GetComponent<CamController>();

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
        }
    }

    private void OnTriggerExit(Collider other)
    {
        StateBabyController baby = other.transform.parent.GetComponentInChildren<StateBabyController>();

        if (baby != null)
        {
            if (baby == _isBabyOccupied)
            {
                //baby.IsParalysed = false;
                IsBabyActionned = false;
                _isBabyOccupied = null;
            }
        }
    }
}
