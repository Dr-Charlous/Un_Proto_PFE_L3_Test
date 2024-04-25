using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Core.PathCore;
using DG.Tweening.Plugins.Options;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class ObjectToPush : MonoBehaviour
{
    public BabyPosCheckAction[] CheckBabies;

    [SerializeField] bool[] _isBabyActionned;
    [SerializeField] Transform[] _destination;
    [SerializeField] GameObject _mesh;
    [SerializeField] CharaMove _character;

    Transform _parent;
    TweenerCore<Vector3, Path, PathOptions> isFinish;
    bool _isActivated = false;

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

        _parent = transform.parent;

        if (isFinish != null && !isFinish.IsActive())
        {
            _mesh.transform.parent = _parent;
            Destroy(this.gameObject);
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

        if (isEveryOneHere && !_isActivated)
        {
            Action();
        }
    }

    void Action()
    {
        if (_character.BabyManager.BabiesInScene[0].GetComponentInChildren<StateBabyController>().Charges > 0)
        {
            Vector3[] destinationPos = new Vector3[_destination.Length];
            for (int i = 0; i < _destination.Length; i++)
            {
                destinationPos[i] = _destination[i].position;
            }

            List<GameObject> baby = _character.BabyManager.BabiesInScene;

            for (int i = 0; i < baby.Count; i++)
            {
                baby[i].GetComponentInChildren<StateBabyController>().Charges--;
            }

            isFinish = transform.DOPath(destinationPos, 2);
            transform.DORotate(_destination[_destination.Length - 1].rotation.eulerAngles, 2);

            _isActivated = true;
        }
    }
}
