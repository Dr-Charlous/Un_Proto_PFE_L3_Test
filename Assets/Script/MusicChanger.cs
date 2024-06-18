using UnityEngine;

public class MusicChanger : MonoBehaviour
{
    [SerializeField] float _speed;
    [SerializeField] float _volumeMax;

    float _value;
    float _value1;
    bool _isInside;
    bool _isValueReset;

    private void Start()
    {
        _value1 = _volumeMax;
        _value = 0;
    }

    private void LateUpdate()
    {
        if (!_isValueReset)
        {
            if (_isInside)
            {
                _value += _speed * Time.deltaTime;
                _value1 -= _speed * Time.deltaTime;

                if (_value >= _volumeMax)
                {
                    _value = _volumeMax;
                }

                if (_value1 <= 0)
                {
                    _value1 = 0;
                }
            }
            else
            {
                _value -= _speed * Time.deltaTime;
                _value1 += _speed * Time.deltaTime;

                if (_value <= 0)
                {
                    _value = 0;
                    _isValueReset = true;
                }

                if (_value >= _volumeMax)
                {
                    _value1 = _volumeMax;
                }
            }
        }

        GameManager.Instance.MusicMain.volume = _value1;
        GameManager.Instance.MusicDanger.volume = _value;
    }

    private void OnTriggerEnter(Collider other)
    {
        CamController chara = other.GetComponent<CamController>();

        if (chara != null)
        {
            _isInside = true;
            _isValueReset = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        CamController chara = other.GetComponent<CamController>();

        if (chara != null)
        {
            _isInside = false;
        }
    }

    public void KillMusic()
    {
        _value -= _speed * Time.deltaTime;
        _value1 -= _speed * Time.deltaTime;

        if (_value <= 0)
        {
            _value = 0;
        }
        if (_value1 <= 0)
        {
            _value1 = 0;
        }
    }
}
