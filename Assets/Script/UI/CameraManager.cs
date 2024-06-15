using DG.Tweening;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public Transform TemporaryPos;
    public Transform PlayerCamPivot;
    public Transform CamPivot;
    public bool IsGamepad;
    public bool IsCamOrbital;
    public float Rotation;
    public float SpeedGamepad;
    public float SpeedKeyboard;
    public float ActualSpeedRotate;
    public float ActualSpeed;

    Transform PreviousTemporaryPos;
    Transform ActualPos;
    bool _ismoved;
    float _valueTime;

    private void Start()
    {
        _ismoved = true;
        ActualPos = GameManager.Instance.CamPlayer;
        UpdateCam(GameManager.Instance.CamPlayer);
    }

    private void Update()
    {
        if (!_ismoved)
            Transition();

        if (ActualPos != null)
            UpdateCam(ActualPos);

        if (Rotation != 0)
        {
            PlayerCamPivot.rotation *= Quaternion.Euler(Vector3.up * Rotation * ActualSpeedRotate * Time.deltaTime);
        }

        if (IsCamOrbital)
        {
            if (IsGamepad)
                ActualSpeedRotate = SpeedGamepad;
            else
                ActualSpeedRotate = SpeedKeyboard;
        }
    }

    void UpdateCam(Transform transformCam)
    {
        transform.position = transformCam.position;
        transform.rotation = transformCam.rotation;
    }

    void CamGoTo(Transform transformCam, float value)
    {
        transform.position = Vector3.Lerp(transform.position, transformCam.position, value);
        transform.rotation = Quaternion.Lerp(transform.rotation, transformCam.rotation, value);
    }

    public void MoveCam(Transform pos)
    {
        ActualPos = null;

        if (_valueTime <= 1 && Vector3.Lerp(transform.position, pos.position, _valueTime) != pos.position)
        {
            _valueTime += Time.deltaTime * ActualSpeed;
            CamGoTo(transform, _valueTime / Vector3.Distance(transform.position, pos.position));

            if (_valueTime > 1)
                _valueTime = 1;
        }
        else
        {
            ActualPos = pos;
            _valueTime = 0;

            _ismoved = true;
        }
    }

    public void ChangeCam(Transform newTransform)
    {
        _ismoved = false;

        if (newTransform == null)
        {
            TemporaryPos = newTransform;
            PreviousTemporaryPos = newTransform;
        }
        else
        {
            if (TemporaryPos != null)
                PreviousTemporaryPos = TemporaryPos;
            else
                PreviousTemporaryPos = newTransform;

            TemporaryPos = newTransform;
        }
    }

    void Transition()
    {
        if (TemporaryPos != null)
        {
            if (TemporaryPos == PreviousTemporaryPos)
                GameManager.Instance.Character.IsParalysed = true;
            else if (TemporaryPos != PreviousTemporaryPos)
                PreviousTemporaryPos = TemporaryPos;

            MoveCam(TemporaryPos);
        }
        else
        {
            PreviousTemporaryPos = null;

            MoveCam(GameManager.Instance.CamPlayer);

            GameManager.Instance.Character.IsParalysed = false;
        }
    }

    public void Reset()
    {
        PlayerCamPivot.DOComplete();
        PlayerCamPivot.DOLocalRotate(CamPivot.rotation.eulerAngles - Vector3.up * 180, 10 * Time.deltaTime);
    }
}
