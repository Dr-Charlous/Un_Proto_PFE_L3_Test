using DG.Tweening;
using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput))]
[RequireComponent(typeof(BoatController))]
[RequireComponent(typeof(Gravity))]

public class CharaMove : MonoBehaviour
{
    [Header("Input System :")]
    Controls _controls;
    public float Position;
    public float Rotation;

    [Header("Components/Values :")]
    [Range(0, 5)]
    public int Life = 5;
    public bool Collected;
    public GameObject Floaters;
    public GameObject ParticuleSystem;
    public Rigidbody _rb;

    [Header("Dash")]
    public bool IsDashing = false;
    public float DashForce = 5f;
    public float DashCooldown = 1f;

    //private UI _UIObject;
    private Transform Body;
    private BoatController _BoatController;
    private Gravity _Gravity;

    #region Inputs
    private void OnEnable()
    {
        _controls.Diplocaulus.Enable();
        _controls.Diplocaulus.Move.performed += GetMoveInputs;
        _controls.Diplocaulus.Collect.started += GetCollectInputs;
        _controls.Diplocaulus.Dash.started += GetDashInput;
    }

    private void OnDisable()
    {
        _controls.Diplocaulus.Disable();
        _controls.Diplocaulus.Move.performed -= GetMoveInputs;
        _controls.Diplocaulus.Collect.started -= GetCollectInputs;
        _controls.Diplocaulus.Dash.started -= GetDashInput;
    }

    void GetMoveInputs(InputAction.CallbackContext move)
    {
        Position = -move.ReadValue<Vector2>().y;
        Rotation = move.ReadValue<Vector2>().x;
    }
    
    void GetCollectInputs(InputAction.CallbackContext collect)
    {
        Collected = !Collected;
    }

    void GetDashInput(InputAction.CallbackContext dash)
    {
        if (IsDashing == false)
        {
            StartCoroutine(Dash());
        }
    }

    private void Awake()
    {
        _controls = new Controls();
    }
    #endregion


    void Start()
    {
        Body = GetComponent<Transform>();
        _rb = GetComponent<Rigidbody>();
        _BoatController = GetComponent<BoatController>();
        _Gravity = GetComponent<Gravity>();
    }

    void Update()
    {
        FallingRotate();
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

    IEnumerator Dash()
    {
        IsDashing = true;
        var direction = transform.forward.normalized;
        _rb.AddForce(-direction * DashForce, ForceMode.Impulse);
        yield return new WaitForSeconds(DashCooldown);
        IsDashing = false;
    }
}