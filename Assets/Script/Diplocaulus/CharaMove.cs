using DG.Tweening;
using System.Collections;
using Unity.VisualScripting;
using UnityEditor.ShaderGraph;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput))]
[RequireComponent(typeof(UI))]
[RequireComponent(typeof(BoatController))]
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
    public Rigidbody _rb;

    private UI _UIObject;
    private BoatController _BoatController;

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
    }

    void Update()
    {
        if (UiIsActive == true)
        {
            _UIObject.ShowUI();
        }

        FallingRotate();
        UpWater();
    }

    void FallingRotate()
    {
        if (_rb != null && (_rb.velocity.y < -10 || _rb.velocity.y > 10))
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
    }

    void UpWater()
    {
        if (Swimming)
        {
            Floaters.SetActive(true);
            _rb.useGravity = false;
        }
        else
        {
            Floaters.SetActive(false);
            _rb.useGravity = true;
        }
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