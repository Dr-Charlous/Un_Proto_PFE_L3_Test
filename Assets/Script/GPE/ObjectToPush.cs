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
    public ObjectToPush[] CheckBlockers;
    public BabyPosCheckAction[] CheckBabies;
    public TriggerIsParetnHere CheckParent;

    [SerializeField] bool[] _isBabyActionned;
    [SerializeField] Transform _destination;
    [SerializeField] GameObject _mesh;
    [SerializeField] CharaMove _character;

    public float ValuePush;
    [SerializeField] float _speedPush;
    [SerializeField] int _decreasePushValue = 1;

    Transform _parent;
    bool _isActivated = false;
    Vector3 _initPos;
    Vector3 _endPos;
    Quaternion _initRot;
    Quaternion _endRot;

    private void Start()
    {
        _initPos = transform.position;
        _endPos = _destination.position;
        _initRot = transform.rotation;
        _endRot = _destination.rotation;
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

        if (ValuePush == 1)
        {
            for (int i = 0; i < _character.BabyManager.BabiesInScene.Count; i++)
            {
                _character.BabyManager.BabiesInScene[i].GetComponentInChildren<StateBabyController>().Charges--;
            }

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

        if (CheckParent != null && !CheckParent.isTrigger)
            isEveryOneHere = false;

        if (CheckBlockers.Length > 0)
        {
            for (int i = 0; i < CheckBlockers.Length; i++)
            {
                if (CheckBlockers[i].ValuePush != 1)
                    isEveryOneHere = false;
            }
        }

        if (isEveryOneHere && !_isActivated)
        {
            Action();
        }
    }

    void Action()
    {
        if (_character.BabyManager.BabiesInScene[0].GetComponentInChildren<StateBabyController>().Charges >= _decreasePushValue || CheckParent == null)
        {
            if (ValuePush < 1)
                ValuePush += _speedPush * Time.deltaTime;
            else
                ValuePush = 1;

            transform.position = Vector3.Lerp(_initPos, _endPos, ValuePush);
            transform.rotation = Quaternion.Lerp(_initRot, _endRot, ValuePush);
            //Vector3[] destinationPos = new Vector3[_destination.Length];
            //for (int i = 0; i < _destination.Length; i++)
            //{
            //    destinationPos[i] = _destination[i].position;
            //}

            //List<GameObject> baby = _character.BabyManager.BabiesInScene;

            //isFinish = transform.DOPath(destinationPos, 2);
            //transform.DORotate(_destination[_destination.Length - 1].rotation.eulerAngles, 2);

            //_isActivated = true;
        }
    }
}
