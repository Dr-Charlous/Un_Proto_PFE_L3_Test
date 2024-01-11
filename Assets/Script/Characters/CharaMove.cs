using DG.Tweening;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput))]
[RequireComponent(typeof(UI))]
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
    public bool Swimming = true;
    public bool UiIsActive = false;
    public Transform Body;
    public GameObject Floaters;
    public GameObject ParticuleSystem;
    public Rigidbody _rb;

    private UI _UIObject;
    private BoatController _BoatController;
    private Gravity _Gravity;

    [Header("Dash")]
    public bool IsDashing = false;
    public float DashForce = 5f;
    public float DashCooldown = 1f;

    [Header("Fishing :")]
    [Range(0f, 5f)]
    public int Fish = 0;
    public bool Fishinning = false;

    [Header("Collecting :")]
    public bool Collecting = false;
    Coroutine _Coroutine;

    #region Inputs
    private void OnEnable()
    {
        _controls.Diplocaulus.Enable();
        _controls.Diplocaulus.Move.performed += GetMoveInputs;
        _controls.Diplocaulus.Dash.started += GetDashInput;

        _controls.Diplocaulus.UI.started += GetUIInput;
        _controls.Diplocaulus.UI.canceled += GetUIInputCanceled;
        _controls.Diplocaulus.Fishing.started += GetFishingInput;
        _controls.Diplocaulus.Fishing.canceled += GetFishingInputCanceled;
        _controls.Diplocaulus.Collecting.started += GetCollectingInput;
        _controls.Diplocaulus.Collecting.canceled += GetCollectingInputCanceled;
        _controls.Diplocaulus.SwitchWater.started += GetSwitchInput;
    }

    private void OnDisable()
    {
        _controls.Diplocaulus.Disable();
        _controls.Diplocaulus.Move.performed -= GetMoveInputs;
        _controls.Diplocaulus.Dash.started -= GetDashInput;

        _controls.Diplocaulus.UI.started -= GetUIInput;
        _controls.Diplocaulus.UI.canceled -= GetUIInputCanceled;
        _controls.Diplocaulus.Fishing.started -= GetFishingInput;
        _controls.Diplocaulus.Fishing.canceled -= GetFishingInputCanceled;
        _controls.Diplocaulus.Collecting.started -= GetCollectingInput;
        _controls.Diplocaulus.Collecting.canceled -= GetCollectingInputCanceled;
        _controls.Diplocaulus.SwitchWater.started -= GetSwitchInput;
    }

    void GetMoveInputs(InputAction.CallbackContext move)
    {
        Position = -move.ReadValue<Vector2>().y;
        Rotation = move.ReadValue<Vector2>().x;
    }

    void GetDashInput(InputAction.CallbackContext dash)
    {
        if (IsDashing == false)
        {
            StartCoroutine(Dash());
        }
    }

    void GetUIInput(InputAction.CallbackContext ui)
    {
        UiIsActive = true;
    }

    void GetUIInputCanceled(InputAction.CallbackContext obj)
    {
        UiIsActive = false;
        _UIObject.HideUI();
    }

    void GetFishingInput(InputAction.CallbackContext fish)
    {
        Fishinning = true;
    }

    void GetFishingInputCanceled(InputAction.CallbackContext fish)
    {
        Fishinning = false;
    }

    void GetCollectingInput(InputAction.CallbackContext fish)
    {
        Collecting = true;
    }

    void GetCollectingInputCanceled(InputAction.CallbackContext fish)
    {
        Collecting = false;

    }
    void GetSwitchInput(InputAction.CallbackContext sw)
    {
        SwitchSwim();
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
        _UIObject = GetComponent<UI>();
        _BoatController = GetComponent<BoatController>();
        _Gravity = GetComponent<Gravity>();

        if (Swimming)
        {
            _Coroutine = StartCoroutine(WaterUp());
        }
        else if (Swimming == false)
        {
            _Coroutine = StartCoroutine(WaterDown());
        }
    }

    void Update()
    {
        if (UiIsActive == true)
        {
            _UIObject.ShowUI();
        }

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

    #region water
    void SwitchSwim()
    {
        Swimming = !Swimming;

        if (Swimming)
        {
            _Coroutine = StartCoroutine(WaterUp());
            ParticuleSystem.SetActive(true);
        }
        else if (Swimming == false)
        {
            _Coroutine = StartCoroutine(WaterDown());
            ParticuleSystem.SetActive(false);
        }
    }

    IEnumerator WaterUp()
    {
        _rb.AddForce(Vector3.up, ForceMode.Force);

        yield return new WaitForSeconds(0.1f);

        Floaters.SetActive(true);
        _Gravity.gravityScale = 1f;
        _rb.useGravity = true;

        var rot = transform.eulerAngles;
        rot.x = 0;
        rot.z = 0;
        transform.eulerAngles = rot;

        _Coroutine = null;
    }

    IEnumerator WaterDown()
    {
        Floaters.SetActive(false);
        _Gravity.gravityScale = 0.8f;
        _rb.useGravity = true;
        _rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;

        yield return new WaitForSeconds(5f);

        var rot = transform.eulerAngles;
        rot.x = 0;
        rot.z = 0;
        transform.eulerAngles = rot;

        _rb.constraints = RigidbodyConstraints.None;
        _Coroutine = null;
    }
    #endregion

    #region abilities
    IEnumerator Dash()
    {
        IsDashing = true;
        var direction = transform.forward.normalized;
        _rb.AddForce(-direction * DashForce, ForceMode.Impulse);
        yield return new WaitForSeconds(DashCooldown);
        IsDashing = false;
    }
    #endregion
}