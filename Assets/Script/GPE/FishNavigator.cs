using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.VFX;

public class FishNavigator : MonoBehaviour
{
    [SerializeField] FishPointNavigation[] _positions;
    [SerializeField] FishPointNavigation _position;
    [SerializeField] GameObject _fish;
    [SerializeField] float _speed;
    [SerializeField] bool _isMoving;

    [Header("Visul Effect : ")]
    [SerializeField] VisualEffect _effect;
    [SerializeField] float _offSetFishEffect;

    TweenerCore<Vector3, Vector3, VectorOptions> _doMoving;

    float _time = 0;
    float _velocity = 0;
    Vector3 _positionFishTimer;

    private void Start()
    {
        _positionFishTimer = _position.transform.position;
        _fish.SetActive(false);
        _doMoving = transform.DOMove(_position.transform.position, 0);
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

        if (transform.position != _position.transform.position && _isMoving == false && _doMoving.IsComplete())
        {
            _doMoving = transform.DOMove(_position.transform.position, _speed * Time.deltaTime);
            if (_position.Neighbours.Length == 0)
                GetFish();
        }

        Vector3 direction = _position.transform.position - transform.position;
        _effect.SetVector3("PosEffect", direction.normalized * _offSetFishEffect);
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.GetComponent<CamController>() != null && !_isMoving)
            GetNearetPoint();
    }

    void GetNearetPoint()
    {
        if (_position.Neighbours.Length > 0)
        {
            float minusDistance = 0;
            int number = 0;

            for (int i = 0; i < _position.Neighbours.Length; i++)
            {
                float distance = Vector3.Distance(_position.Neighbours[i].transform.position, _position.transform.position);
                float distancePlayer = Vector3.Distance(_position.Neighbours[i].transform.position, GameManager.Instance.Character.transform.position);

                float pourcentage = distancePlayer / distance * 100;
                if (minusDistance < pourcentage)
                {
                    minusDistance = pourcentage;
                    number = i;
                }
            }

            _position = _position.Neighbours[number].GetComponent<FishPointNavigation>();
            transform.DOMove(_position.transform.position, _speed * Time.deltaTime);
            _isMoving = true;
        }
        else
        {
            GetFish();
        }
    }

    void GetFish()
    {
        _fish.SetActive(true);
        gameObject.SetActive(false);
    }
}
