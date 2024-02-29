using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class FishNavigator : MonoBehaviour
{
    [SerializeField] FishPointNavigation[] _positions;
    [SerializeField] FishPointNavigation _position;
    [SerializeField] Rigidbody _rb;
    [SerializeField] CharaMove _chara;
    [SerializeField] float _speed;
    [SerializeField] bool _isMoving;

    int _number;
    int _numberLast;
    float _time = 0;
    Vector3 _positionFishTimer;

    private void Start()
    {
        _positionFishTimer = transform.position;
    }

    private void Update()
    {
        _time += Time.deltaTime;
        if (_time >= 0.2f)
        {
            float velocity = (_positionFishTimer - _position.transform.position).magnitude;
            Debug.Log(velocity);

            if (velocity != 0)
                _isMoving = true;
            else
                _isMoving = false;

            _time = 0;

            _positionFishTimer = _position.transform.position;
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.GetComponent<CharaMove>() != null && !_isMoving)
            GetNearetPoint();
    }

    void GetNearetPoint()
    {
        float minusDistance = 1000;
        int number = 0;

        for (int i = 0; i < _position.Neighbours.Length; i++)
        {
            float distance = Vector3.Distance(_position.Neighbours[i].transform.position, _position.transform.position);
            float distancePlayer = Vector3.Distance(_position.Neighbours[i].transform.position, _chara.transform.position);

            if ((distance < minusDistance || distance < distancePlayer) && i != _number && i != _numberLast)
            {
                minusDistance = distance;
                number = i;
            }
        }

        _numberLast = _number;
        _number = number;

        _position = _position.Neighbours[_number].GetComponent<FishPointNavigation>();
        transform.DOMove(_position.transform.position, _speed * Time.deltaTime);
    }
}
