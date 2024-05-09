using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;

public class ObjectResonnance : MonoBehaviour
{
    public Transform BabyTarget;
    public Transform BabyPos;
    public float SpeedBabyTarget = 5;
    public bool IsResonating = false;
    public bool IsCoroutineFinish;
    public bool IsPlayerInside;

    [SerializeField] CharaMove _character;
    [SerializeField] Camera _cameraMove;
    [SerializeField] AudioSource _source;
    [SerializeField] AudioClip _clip;
    [SerializeField] Transform _destinationCamera;
    [SerializeField] float _speed = 5;

    Camera _mainCamera;
    Vector3 LastPosPlayer;
    float _speedCam = 0;

    private void Start()
    {
        _mainCamera = Camera.main;
        _cameraMove.gameObject.SetActive(false);
        IsCoroutineFinish = true;
        IsPlayerInside = false;
    }

    private void Update()
    {
        if (IsPlayerInside)
        {
            PlaySound(_source, _clip);
            IsResonating = true;
        }
        else
        {
            IsResonating = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<CharaMove>() != null && IsPlayerInside == false)
        {
            PlayerGetInside();
        }
    }

    void PlaySound(AudioSource source, AudioClip clip)
    {
        if (source.isPlaying)
        {
            return;
        }
        else
        {
            source.clip = clip;
            source.Play();
        }
    }

    void PlayerGetInside()
    {
        if (IsCoroutineFinish)
        {
            //Debug.Log("ObjectResonnance");

            StartCoroutine(CameraMove());

            BabyTarget.position = BabyPos.position;

            for (int i = 0; i < _character.GetComponentInChildren<BabyManager>().BabiesInScene.Count; i++)
            {
                StateBabyController Baby = _character.GetComponentInChildren<BabyManager>().BabiesInScene[0].GetComponentInChildren<StateBabyController>();

                if (!IsResonating)
                {
                    Baby.ChangeState(Baby.StateAction);
                    Baby.Target = BabyTarget;

                    _character.GetComponentInChildren<BabyManager>().ChangeOrder();
                }
            }

            _character.TrapResonnance = this;
            IsPlayerInside = true;
        }
    }

    public void PlayerGetOutside()
    {
        if (IsCoroutineFinish)
        {
            //Debug.Log("ObjectResonnance");

            StartCoroutine(CameraMove());

            for (int i = 0; i < _character.GetComponentInChildren<BabyManager>().BabiesInScene.Count; i++)
            {
                StateBabyController Baby = _character.GetComponentInChildren<BabyManager>().BabiesInScene[0].GetComponentInChildren<StateBabyController>();

                if (!IsResonating)
                {
                    Baby.ChangeState(Baby.StateStay);

                    _character.GetComponentInChildren<BabyManager>().ChangeOrder();
                }
            }

            _character.TrapResonnance = null;
            IsPlayerInside = false;
        }
    }

    public IEnumerator CameraMove()
    {
        if (IsCoroutineFinish == true)
        {
            IsCoroutineFinish = false;

            if (!_cameraMove.gameObject.activeSelf)
            {
                ChangeCam();
                ChangePlayerPos();
                _character.IsParalysed = true;

                _cameraMove.transform.position = _mainCamera.transform.position;
                _cameraMove.transform.rotation = _mainCamera.transform.rotation;

                _speedCam = (_destinationCamera.position - _cameraMove.transform.position).magnitude * _speed * Time.deltaTime;

                _cameraMove.transform.DOMove(_destinationCamera.position, _speedCam);
                _cameraMove.transform.DORotate(_destinationCamera.rotation.eulerAngles, _speedCam);

                yield return new WaitForSeconds(_speedCam);
            }
            else if (_cameraMove.gameObject.activeSelf)
            {
                _character.IsParalysed = false;
                ChangePlayerPos();

                _cameraMove.transform.position = _destinationCamera.position;
                _cameraMove.transform.rotation = _destinationCamera.rotation;


                _cameraMove.transform.DOMove(_mainCamera.transform.position, _speedCam);
                _cameraMove.transform.DORotate(_mainCamera.transform.rotation.eulerAngles, _speedCam);

                yield return new WaitForSeconds(_speedCam);

                _cameraMove.transform.DOMove(_mainCamera.transform.position, _speedCam);
                _cameraMove.transform.DORotate(_mainCamera.transform.rotation.eulerAngles, _speedCam);
                ChangeCam();
            }

            IsCoroutineFinish = true;
        }
    }

    void ChangeCam()
    {
        _cameraMove.gameObject.SetActive(!_cameraMove.gameObject.activeSelf);
        _mainCamera.gameObject.SetActive(!_mainCamera.gameObject.activeSelf);
    }

    public void ChangePlayerPos()
    {
        if (LastPosPlayer == Vector3.zero)
        {
            LastPosPlayer = _character.transform.position;

            float speed = (transform.position - _character.transform.position).magnitude * _speed * Time.deltaTime;

            _character.transform.DOMove(transform.position, speed);
        }
        else
        {
            float speed = (transform.position - _character.transform.position).magnitude * _speed * Time.deltaTime;

            _character.transform.DOMove(LastPosPlayer, speed);
            _character._rb.velocity = Vector3.zero;

            LastPosPlayer = Vector3.zero;
        }
    }
}
