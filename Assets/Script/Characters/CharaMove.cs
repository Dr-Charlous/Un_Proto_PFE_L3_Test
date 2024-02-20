using System;
using System.Collections;
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


    [Header("Babies :")]
    public int BabieNumber = 0;
    public int BabieNumberOnBack = 1;
    public GameObject[] Babies;

    [Header("Dash")]
    public bool IsDashing = false;
    public float DashForce = 5f;
    public float DashCooldown = 1f;

    //private UI _UIObject;
    Transform _body;
    BoatController _BoatController;
    Gravity _Gravity;
    BabyManager _BabyManager;

    #region Inputs
    private void OnEnable()
    {
        _controls.Diplocaulus.Enable();
        _controls.Diplocaulus.Move.performed += GetMoveInputs;
        _controls.Diplocaulus.Collect.started += GetCollectInputs;
        _controls.Diplocaulus.Dash.started += GetDashInput;
        _controls.Diplocaulus.KidsGamePad.performed += GetKidsInputGamePad;
        _controls.Diplocaulus.KidsKeyBoard.started += GetKidsInputMouse;
        _controls.Diplocaulus.BabyFollow.started += GetBabyFollowInput;
        _controls.Diplocaulus.BabyAction.started += GetBabyActionInput;
        _controls.Diplocaulus.BabyGet.started += GetBabyGetInput;
    }

    private void OnDisable()
    {
        _controls.Diplocaulus.Disable();
        _controls.Diplocaulus.Move.performed -= GetMoveInputs;
        _controls.Diplocaulus.Collect.started -= GetCollectInputs;
        _controls.Diplocaulus.Dash.started -= GetDashInput;
        _controls.Diplocaulus.KidsGamePad.performed -= GetKidsInputGamePad;
        _controls.Diplocaulus.KidsKeyBoard.started -= GetKidsInputMouse;
        _controls.Diplocaulus.BabyFollow.started -= GetBabyFollowInput;
        _controls.Diplocaulus.BabyAction.started -= GetBabyActionInput;
        _controls.Diplocaulus.BabyGet.started -= GetBabyGetInput;
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

    private void GetKidsInputMouse(InputAction.CallbackContext input)
    {
        if (int.Parse(input.action.ReadValueAsObject().ToString()) > 0 && BabieNumber < Babies.Length)
        {
            BabieNumber++;
        }
        else if (int.Parse(input.action.ReadValueAsObject().ToString()) < 0 && BabieNumber > 1)
        {
            BabieNumber--;
        }
    }

    private void GetKidsInputGamePad(InputAction.CallbackContext input)
    {
        if (input.action.ReadValue<Vector2>().x == 1 && Babies.Length > 1)
        {
            BabieNumber = 1;
        }
        else if (input.action.ReadValue<Vector2>().x == -1 && Babies.Length > 3)
        {
            BabieNumber = 3;
        }
        else if(input.action.ReadValue<Vector2>().y == 1 && Babies.Length > 2)
        {
            BabieNumber = 2;
        }
        else if (input.action.ReadValue<Vector2>().y == -1)
        {
            BabieNumber = 0;
        }
    }

    void GetBabyFollowInput(InputAction.CallbackContext baby)
    {
        _BabyManager.BabyFollow();
    }

    void GetBabyActionInput(InputAction.CallbackContext baby)
    {
        _BabyManager.BabyAction();
    }

    void GetBabyGetInput(InputAction.CallbackContext baby)
    {
        _BabyManager.GetBaby(BabieNumberOnBack);
    }

    private void Awake()
    {
        _controls = new Controls();
        
        _rb = GetComponent<Rigidbody>();
        _BoatController = GetComponent<BoatController>();
        _Gravity = GetComponent<Gravity>();
        _BabyManager = GetComponent<BabyManager>();
    }
    #endregion

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