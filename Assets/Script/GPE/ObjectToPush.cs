using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectToPush : MonoBehaviour
{
    [SerializeField] BabyPosCheckAction[] _checkBabies;
    [SerializeField] bool[] _isBabyActionned;
    [SerializeField] Transform _Destination;

    private void Update()
    {
        for (int i = 0; i < _checkBabies.Length; i++)
        {
            if (_checkBabies[i].IsBabyActionned)
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
        transform.DOMove(_Destination.position, 2);
        transform.DORotate(_Destination.rotation.eulerAngles, 2);
        this.enabled = false;
    }
}
