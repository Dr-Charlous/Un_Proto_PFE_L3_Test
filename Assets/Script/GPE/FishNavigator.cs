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

    float _time = 0;
    float _velocity = 0;
    Vector3 _positionFishTimer;

    private void Start()
    {
        _positionFishTimer = _position.transform.position;
    }

    private void Update()
    {
        _time += Time.deltaTime;
        if (_time >= 1)
        {
            _velocity = (_positionFishTimer - _position.transform.position).magnitude / Time.deltaTime;

            if (_velocity >= 0.1f)
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
        float minusDistance = 0;
        int number = 0;

        for (int i = 0; i < _position.Neighbours.Length; i++)
        {
            float distance = Vector3.Distance(_position.Neighbours[i].transform.position, _position.transform.position);
            float distancePlayer = Vector3.Distance(_position.Neighbours[i].transform.position, _chara.transform.position);

            float pourcentage = distancePlayer / distance * 100;
            Debug.Log($"{pourcentage} : {minusDistance}");
            if (minusDistance < pourcentage)
            {
                minusDistance = pourcentage;
                number = i;
            }
        }

        Debug.Log(number);

        _position = _position.Neighbours[number].GetComponent<FishPointNavigation>();
        transform.DOMove(_position.transform.position, _speed * Time.deltaTime);
        _isMoving = true;
    }
}
