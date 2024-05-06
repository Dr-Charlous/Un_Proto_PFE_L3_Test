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
    public TriggerIsParetnHere CheckParent;

    [SerializeField] bool[] _isBabyActionned;
    [SerializeField] Transform[] _destination;
    [SerializeField] GameObject _mesh;
    [SerializeField] CharaMove _character;

    [SerializeField] float _valuePush;
    [SerializeField] float _speedPush;

    Transform _parent;
    bool _isActivated = false;
    Vector3 _initPos;
    Vector3 _endPos;
    Quaternion _initRot;
    Quaternion _endRot;

    private void Start()
    {
        _initPos = transform.position;
        _endPos = _destination[0].position;
        _initRot = transform.rotation;
        _endRot = _destination[0].rotation;
    }

    private void Update()
    {
        for (int i = 0; i < CheckBabies.Length; i++)
        {
            if (CheckBabies[i].IsBabyActionned)
            {
                _isBabyActionned[i] = true;
            }
            else
                _isBabyActionned[i] = false;
        }

        CheckForAction();

        _parent = transform.parent;

        if (_valuePush == 1)
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

        if (CheckParent != null)
            isEveryOneHere = false;

        if (isEveryOneHere && !_isActivated)
        {
            Action();
        }
    }

    void Action()
    {
        //if (_character.BabyManager.BabiesInScene[0].GetComponentInChildren<StateBabyController>().Charges > 0)
        //{
        if (_valuePush < 1)
            _valuePush += _speedPush * Time.deltaTime;
        else
            _valuePush = 1;

        transform.position = Vector3.Lerp(_initPos, _endPos, _valuePush);
        transform.rotation = Quaternion.Lerp(_initRot, _endRot, _valuePush);
        //Vector3[] destinationPos = new Vector3[_destination.Length];
        //for (int i = 0; i < _destination.Length; i++)
        //{
        //    destinationPos[i] = _destination[i].position;
        //}

        //List<GameObject> baby = _character.BabyManager.BabiesInScene;

        //for (int i = 0; i < baby.Count; i++)
        //{
        //    baby[i].GetComponentInChildren<StateBabyController>().Charges--;
        //}

        //isFinish = transform.DOPath(destinationPos, 2);
        //transform.DORotate(_destination[_destination.Length - 1].rotation.eulerAngles, 2);

        //_isActivated = true;
        //}
    }
}
