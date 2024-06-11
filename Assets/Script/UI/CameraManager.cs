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

    float _valueTime;

    private void Start()
    {
        UpdateCam(GameManager.Instance.CamPlayer);
    }

    private void Update()
    {
        Transition();

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

    void Transition()
    {
        if (TemporaryPos != null)
        {
            if (_valueTime <= 1 && Vector3.Lerp(transform.position, TemporaryPos.position, _valueTime) != TemporaryPos.position)
            {
                _valueTime += Time.deltaTime * ActualSpeed;
                CamGoTo(TemporaryPos, _valueTime / Vector3.Distance(transform.position, TemporaryPos.position));

                if (_valueTime > 1)
                    _valueTime = 1;
            }
            else
            {
                UpdateCam(TemporaryPos);
            }
            GameManager.Instance.Character.IsParalysed = true;
        }
        else
        {
            if (_valueTime >= 0 && Vector3.Lerp(transform.position, GameManager.Instance.CamPlayer.position, (1 - _valueTime)) != GameManager.Instance.CamPlayer.position)
            {
                _valueTime -= Time.deltaTime * ActualSpeed;
                CamGoTo(GameManager.Instance.CamPlayer, (1 - _valueTime) / Vector3.Distance(transform.position, GameManager.Instance.CamPlayer.position));

                if (_valueTime < 0)
                    _valueTime = 0;
            }
            else
            {
                UpdateCam(GameManager.Instance.CamPlayer);
                GameManager.Instance.Character.IsParalysed = false;
            }
        }
    }

    public void Reset()
    {
        PlayerCamPivot.DOComplete();
        PlayerCamPivot.DOLocalRotate(CamPivot.rotation.eulerAngles - Vector3.up * 180, ActualSpeed * Time.deltaTime);
    }
}
