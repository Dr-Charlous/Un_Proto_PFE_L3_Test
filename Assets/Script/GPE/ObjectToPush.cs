using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectToPush : MonoBehaviour
{
    [SerializeField] BabyPosCheckAction[] _checkBabies;
    [SerializeField] bool[] _isBabyActionned;

    private void Update()
    {
        for (int i = 0; i < _checkBabies.Length; i++)
        {
            if (_checkBabies[i].IsBabyActionned)
            {
                _isBabyActionned[i] = true;
                Action();
            }
            else
                _isBabyActionned[i] = false;
        }
    }

    void Action()
    {
        bool isEveryOneHere = true;

        for (int i = 0; i < _isBabyActionned.Length; i++)
        {
            if (_isBabyActionned[i] == false)
                isEveryOneHere = false;
        }

        if (isEveryOneHere)
        {
            Debug.Log("Action");
        }
    }
}
