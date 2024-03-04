using DG.Tweening;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEditor.Experimental.GraphView.GraphView;

[RequireComponent(typeof(InputManager))]

public class CharaMove : MonoBehaviour
{
    [Header("Input System :")]
    public float Position;
    public float Rotation;
    [SerializeField] float _speed = 10;
    [SerializeField] float _decreaseSpeed = 1.01f;
    [SerializeField] float _steering = 500f;

    [Header("Components/Values :")]
    [Range(0, 5)]
    public int Life = 5;
    public bool Collected;
    public GameObject Floaters;
    public GameObject ParticuleSystem;
    public Rigidbody _rb;
    public GameObject UI;

    [Header("Dash")]
    public bool IsDashing = false;
    public float DashForce = 5f;
    public float DashCooldown = 1f;

    //private UI _UIObject;
    public BabyManager BabyManager;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();

        UI.SetActive(false);
    }

    void Update()
    {
        Movement();
        Rotate();
        FallingRotate();
    }

    void Movement()
    {
        if (Position > 0)
        {
            _rb.AddRelativeForce(Vector3.forward * _speed * Time.fixedDeltaTime);
        }
        else if (Position < 0)
        {
            _rb.AddRelativeForce(Vector3.back * _speed * Time.fixedDeltaTime);
        }
        else
        {
            _rb.velocity = _rb.velocity / _decreaseSpeed; // <-- This will gradually slow down the player when they're idle.
        }
    }

    void Rotate()
    {
        if (Rotation > 0)
        {
            transform.rotation *= Quaternion.Euler(Vector3.up * _steering * Time.fixedDeltaTime);
        }
        else if (Rotation < 0)
        {
            transform.rotation *= Quaternion.Euler(Vector3.down * _steering * Time.fixedDeltaTime);
        }
        else
        {

        }
    }

    void FallingRotate()
    {
        if (_rb != null && (_rb.velocity.y < -2 || _rb.velocity.y > 2))
        {
            var rot = transform.eulerAngles;
            rot.x = 0;
            rot.z = 0;
            transform.eulerAngles = rot;
        }
    }

    public IEnumerator Dash()
    {
        IsDashing = true;
        var direction = transform.forward.normalized;
        _rb.AddForce(-direction * DashForce, ForceMode.Impulse);
        yield return new WaitForSeconds(DashCooldown);
        IsDashing = false;
    }
}