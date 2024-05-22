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
    [HideInInspector] public ObjectResonnance TrapResonnance;

    public Animator Animator;
    [SerializeField] Rigidbody CharacterRb;

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
        else
        {
            _rb.velocity = Vector3.zero;

            if (TrapResonnance != null)
                TrapResonnance.BabyTarget.position += (transform.right * Position + transform.forward * Rotation) * TrapResonnance.SpeedBabyTarget * Time.deltaTime;
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
        _rb.velocity = _rb.velocity / _decreaseSpeed;

        Animator.SetFloat("Move", CharacterRb.velocity.magnitude, 0.1f, Time.deltaTime);
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