using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public Animator Animator;
    public Transform TemporaryPos;
    public Transform PlayerCamPivot;
    public Transform CamPivot;
    public Transform CamPivot2;
    public Transform ActualPos;
    public bool IsGamepad;
    public float Rotation;
    public float SpeedGamepad;
    public float SpeedKeyboard;
    public float ActualSpeedRotate;
    public float ActualSpeed;

    Coroutine _corroutine;
    List<Transform> _waitingPos = new();
    List<float> _timesWaiting = new();
    bool _moving;

    private void Start()
    {
        _moving = false;
        ActualPos = GameManager.Instance.CamPlayer;

        CamPivot2.position = ActualPos.position;
        CamPivot2.rotation = ActualPos.rotation;
    }

    private void Update()
    {
        if (IsGamepad)
            ActualSpeedRotate = SpeedGamepad;
        else
            ActualSpeedRotate = SpeedKeyboard;

        if (Rotation != 0 && !GameManager.Instance.Character.IsParalysed)
        {
            PlayerCamPivot.rotation *= Quaternion.Euler(Vector3.up * Rotation * ActualSpeedRotate * Time.deltaTime);
        }

        if (!_moving)
            UpdateCam(ActualPos);
    }

    public void ChangeCam(Transform transformPos, float time, bool isShake)
    {
        _waitingPos.Add(transformPos);
        _timesWaiting.Add(time);

        if (_corroutine == null)
            _corroutine = StartCoroutine(Transition(isShake));
    }

    void UpdateCam(Transform transformCam)
    {
        CamPivot2.position = transformCam.position;
        CamPivot2.rotation = transformCam.rotation;
    }

    IEnumerator Transition(bool isShake)
    {
        if (isShake)
            Animator.SetBool("Shake", true);

        _moving = true;
        ActualPos = _waitingPos[0];
        GameManager.Instance.Character.IsParalysed = _moving;

        CamPivot2.DOKill();
        CamPivot2.DOMove(_waitingPos[0].position, _timesWaiting[0]);
        CamPivot2.DORotate(_waitingPos[0].rotation.eulerAngles, _timesWaiting[0]);

        yield return new WaitForSeconds(_timesWaiting[0]);

        _waitingPos.RemoveAt(0);
        _timesWaiting.RemoveAt(0);

        if (isShake)
            Animator.SetBool("Shake", false);

        if (_waitingPos.Count > 0 && _timesWaiting.Count > 0)
            _corroutine = StartCoroutine(Transition(isShake));
        else
        {
            _moving = false;
            GameManager.Instance.Character.IsParalysed = _moving;
            _corroutine = null;
        }
    }

    #region comm
    //void CamGoTo(Transform transformCam, float value)
    //{
    //    transform.position = Vector3.Lerp(transform.position, transformCam.position, value);
    //    transform.rotation = Quaternion.Lerp(transform.rotation, transformCam.rotation, value);
    //}

    //public void MoveCam(Transform pos)
    //{
    //    ActualPos = null;

    //    if (_valueTime <= 1 && Vector3.Lerp(transform.position, pos.position, _valueTime) != pos.position)
    //    {
    //        _valueTime += Time.deltaTime * ActualSpeed;
    //        CamGoTo(transform, _valueTime / Vector3.Distance(transform.position, pos.position));

    //        if (_valueTime > 1)
    //            _valueTime = 1;
    //    }
    //    else
    //    {
    //        ActualPos = pos;
    //        _valueTime = 0;

    //        _ismoved = true;
    //    }
    //}

    //public void ChangeCam(Transform newTransform)
    //{
    //    if (newTransform == null)
    //    {
    //        TemporaryPos = newTransform;
    //        PreviousTemporaryPos = newTransform;
    //    }
    //    else
    //    {
    //        if (TemporaryPos != null)
    //            PreviousTemporaryPos = TemporaryPos;
    //        else
    //            PreviousTemporaryPos = newTransform;

    //        TemporaryPos = newTransform;
    //    }
    //}

    //void Transition()
    //{
    //    if (TemporaryPos != null)
    //    {
    //        if (TemporaryPos == PreviousTemporaryPos)
    //            GameManager.Instance.Character.IsParalysed = true;
    //        else if (TemporaryPos != PreviousTemporaryPos)
    //            PreviousTemporaryPos = TemporaryPos;

    //        MoveCam(TemporaryPos);
    //    }
    //    else
    //    {
    //        PreviousTemporaryPos = null;

    //        MoveCam(GameManager.Instance.CamPlayer);

    //        GameManager.Instance.Character.IsParalysed = false;
    //    }
    //}
    #endregion

    public void Reset()
    {
        PlayerCamPivot.DOComplete();
        PlayerCamPivot.DOLocalRotate(CamPivot.rotation.eulerAngles - Vector3.up, 10 * Time.deltaTime);
    }
}
