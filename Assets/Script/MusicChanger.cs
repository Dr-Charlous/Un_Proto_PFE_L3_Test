using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicChanger : MonoBehaviour
{
    [SerializeField] float _value;
    [SerializeField] float _value1;
    [SerializeField] float _speed;
    [SerializeField] bool _isInside;
    [SerializeField] bool _isValueReset;

    private void LateUpdate()
    {
        if (!_isValueReset)
        {
            if (_isInside)
            {
                _value += _speed * Time.deltaTime;
                _value1 -= _speed * Time.deltaTime;

                if (_value >= 1)
                {
                    _value = 1;
                }

                if (_value1 <= 0)
                {
                    _value1 = 0;
                }

                GameManager.Instance.MusicMain.volume = _value1;
                GameManager.Instance.MusicDanger.volume = _value;
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

                if (_value >= 1)
                {
                    _value1 = 1;
                }

                GameManager.Instance.MusicMain.volume = _value1;
                GameManager.Instance.MusicDanger.volume = _value;
            }
        }
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
}
