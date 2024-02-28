using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishNavigator : MonoBehaviour
{
    [SerializeField] Transform[] _positions;
    [SerializeField] float _speed;
    [SerializeField] int _number;
    [SerializeField] bool _isMoving;
    float _time = 0;
    float _timer = 2;
    Transform _transform;
    Vector3 actualPos;


    private void Update()
    {
        _time += Time.deltaTime;

        if (_time >= _timer)
        {
            if ((actualPos - _transform.position).magnitude < 0.1f)
            {
                _isMoving = false;
            }
            else
            {
                 _isMoving = true;
            }

            Debug.Log((actualPos - _transform.position).magnitude);
            actualPos = _transform.position;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<CharaMove>() != null && !_isMoving)
        {
            GetNearestPosition(other.transform);
        }
    }

    void GetNearestPosition(Transform charaTransform)
    {
        _transform = charaTransform;
        float distance = 100000000;
        int number = 0;

        for (int i = 0; i < _positions.Length; i++)
        {
            float distancePoint = Vector3.Distance(_positions[i].position, transform.position);
            float distanceCharacter = Vector3.Distance(_positions[i].position, charaTransform.position);

            if (distancePoint < distance && distanceCharacter > distancePoint && i != _number)
            {
                distancePoint = distance;
                number = i;
            }
        }

        _number = number;

        transform.DOComplete();
        transform.DOMove(_positions[_number].position, _speed * Time.deltaTime);
        _isMoving = true;
    }
}
