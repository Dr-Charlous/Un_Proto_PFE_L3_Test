using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectToPush : MonoBehaviour
{
    public BabyPosCheckAction[] CheckBabies;
    [SerializeField] bool[] _isBabyActionned;
    [SerializeField] Transform[] _destination;

    private void Update()
    {
        for (int i = 0; i < CheckBabies.Length; i++)
        {
            if (CheckBabies[i].IsBabyActionned)
            {
                _isBabyActionned[i] = true;
                CheckForAction();
            }
            else
                _isBabyActionned[i] = false;
        }
    }

    void CheckForAction()
    {
        bool isEveryOneHere = true;

        for (int i = 0; i < _isBabyActionned.Length; i++)
        {
            if (_isBabyActionned[i] == false)
                isEveryOneHere = false;
        }

        if (isEveryOneHere)
        {
            Action();
        }
    }

    void Action()
    {
        Vector3[] destinationPos = new Vector3[_destination.Length];
        for (int i = 0; i < _destination.Length; i++)
        {
            destinationPos[i] = _destination[i].position;
        }

        transform.DOPath(destinationPos, 2);
        transform.DORotate(_destination[_destination.Length-1].rotation.eulerAngles, 2);
        this.enabled = false;
    }
}
