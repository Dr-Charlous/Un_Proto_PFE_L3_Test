using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ObjectResonnance : MonoBehaviour
{
    public bool IsResonating = false;
    public Transform BabyPos;

    [SerializeField] CharaMove _character;
    [SerializeField] Camera _cameraMove;
    [SerializeField] AudioSource _source;
    [SerializeField] AudioClip _clip;
    [SerializeField] Transform _destinationCamera;
    [SerializeField] float _speed = 5;

    private Camera _mainCamera;

    private void Start()
    {
        _mainCamera = Camera.main;
        _cameraMove.gameObject.SetActive(false);
    }

    private void OnTriggerStay(Collider other)
    {
        StateBabyController babyController = other.transform.parent.GetComponentInChildren<StateBabyController>();

        if (babyController != null && babyController.currentState == babyController.StateAction)
        {
            babyController.IsParalysed = true;
            babyController.gameObject.transform.position = transform.position;
            PlaySound(_source, _clip);
            IsResonating = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform.parent.GetComponentInChildren<StateBabyController>() != null)
        {
            _source.Stop();
            IsResonating = false;
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

    public void CameraMove()
    {
        if (!_cameraMove.gameObject.activeSelf)
        {
            _cameraMove.gameObject.SetActive(true);
            _mainCamera.gameObject.SetActive(false);
            _character.IsParalysed = true;

            _cameraMove.transform.position = _mainCamera.transform.position;
            _cameraMove.transform.rotation = _mainCamera.transform.rotation;

            float speed = (_destinationCamera.position - _cameraMove.transform.position).magnitude * _speed * Time.deltaTime;

            _cameraMove.transform.DOMove(_destinationCamera.position, speed);
            _cameraMove.transform.DORotate(_destinationCamera.rotation.eulerAngles, speed);
        }
        else if (_cameraMove.gameObject.activeSelf)
        {
            _cameraMove.gameObject.SetActive(false);
            _mainCamera.gameObject.SetActive(true);
            _character.IsParalysed = false;

            _cameraMove.transform.position = _destinationCamera.position;
            _cameraMove.transform.rotation = _destinationCamera.rotation;

            float speed = (_mainCamera.transform.position - _cameraMove.transform.position).magnitude * _speed * Time.deltaTime;

            _cameraMove.transform.DOMove(_mainCamera.transform.position, speed);
            _cameraMove.transform.DORotate(_mainCamera.transform.rotation.eulerAngles, speed);
        }
    }
}
