using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public Transform TemporaryPos;
    public float Speed;

    float _valueTime;

    private void Start()
    {
        UpdateCam(GameManager.Instance.CamPlayer);
    }

    private void Update()
    {
        if (TemporaryPos != null)
        {
            if (_valueTime <= 1 && Vector3.Lerp(transform.position, TemporaryPos.position, _valueTime) != TemporaryPos.position)
            {
                _valueTime += Time.deltaTime * Speed;
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
                _valueTime -= Time.deltaTime * Speed;
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
}
