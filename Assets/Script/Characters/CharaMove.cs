using DG.Tweening;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(InputManager))]

public class CharaMove : MonoBehaviour
{
    [Header("Input System :")]
    public float Position;
    public float Rotation;

    [SerializeField] float _acceleration = 10;
    [SerializeField] float _limitMaxSpeed = 5;
    [SerializeField] float _decreaseSpeed = 1.01f;
    [SerializeField] float _steering = 500f;

    [Header("Components/Values :")]
    [Range(0, 5)]
    public bool Collected;
    public bool CollectedBabies;
    public GameObject ParticuleSystem;
    public Rigidbody _rb;

    public bool IsParalysed = false;
    public BabyManager BabyManager;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (!IsParalysed)
        {
            Movement();
            Rotate();
        }
    }

    void Movement()
    {
        if (Position != 0)
        {
            if (_rb.velocity.magnitude < _limitMaxSpeed)
            {
                if (Position < 0)
                    _rb.AddRelativeForce(Vector3.forward * Position * _acceleration * Time.fixedDeltaTime);
                else
                    _rb.AddRelativeForce(Vector3.forward * Position * _acceleration / 10 * Time.fixedDeltaTime);
            }
        }
        _rb.velocity = _rb.velocity / _decreaseSpeed; // <-- This will gradually slow down the player when they're idle.
    }

    void Rotate()
    {
        if (Rotation != 0)
        {
            _rb.angularVelocity += Vector3.up * Rotation * _steering * Time.fixedDeltaTime;
        }
        else
        {
            _rb.angularVelocity = _rb.angularVelocity / _decreaseSpeed;
        }
    }
}