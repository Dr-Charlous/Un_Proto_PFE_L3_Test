using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static Unity.VisualScripting.Member;

[RequireComponent(typeof(PlayerInput))]
public class InputManager : MonoBehaviour
{
    Controls _controls;

    [HideInInspector] public float Vertical;
    [HideInInspector] public float Horizontal;
    [HideInInspector] public bool Call;
    [HideInInspector] public bool Assign;
    [SerializeField] bool InGame;
    [SerializeField] AudioSource _source;

    public bool IsGamepad;

    private void OnEnable()
    {
        _controls.Diplocaulus.Enable();
        _controls.Diplocaulus.Move.performed += GetMoveInputs;

        _controls.Diplocaulus.CamMove.performed += GetCamMoveInputs;
        _controls.Diplocaulus.CamReset.started += GetCamResetInput;

        _controls.Diplocaulus.BabyAction.started += GetBabyActionInput;
        _controls.Diplocaulus.BabyAction.canceled += BabyActionOutput;
    }

    private void OnDisable()
    {
        _controls.Diplocaulus.Disable();
        _controls.Diplocaulus.Move.performed -= GetMoveInputs;

        _controls.Diplocaulus.CamMove.performed -= GetCamMoveInputs;
        _controls.Diplocaulus.CamReset.started -= GetCamResetInput;

        _controls.Diplocaulus.BabyAction.started -= GetBabyActionInput;
        _controls.Diplocaulus.BabyAction.canceled -= BabyActionOutput;
    }

    bool VerifyDevice(InputAction.CallbackContext input)
    {
        if (input.action.activeControl.device.name == "Keyboard" || input.action.activeControl.device.name == "Mouse")
            IsGamepad = false;
        else
            IsGamepad = true;

        return IsGamepad;
    }

    void GetMoveInputs(InputAction.CallbackContext move)
    {
        GameManager.Instance.Character.Position = -move.ReadValue<Vector2>().y;
        GameManager.Instance.Character.Rotation = move.ReadValue<Vector2>().x;

        Vertical = -move.ReadValue<Vector2>().y;
        Horizontal = move.ReadValue<Vector2>().x;

        VerifyDevice(move);
    }

    void GetCamMoveInputs(InputAction.CallbackContext move)
    {
        GameManager.Instance.CamManager.Rotation = move.ReadValue<Vector2>().x;

        GameManager.Instance.CamManager.IsGamepad = VerifyDevice(move);
    }

    void GetCamResetInput(InputAction.CallbackContext reset)
    {
        GameManager.Instance.CamManager.Reset();

        if (_source != null && !_source.isPlaying)
            _source.Play();

        VerifyDevice(reset);
    }

    void GetBabyActionInput(InputAction.CallbackContext baby)
    {
        if (!GameManager.Instance.Character.IsParalysed)
        {
            GameManager.Instance.BabyManager.BabyAction();

            GameManager.Instance.Character.InputCollectBabies = true;
        }

        GameManager.Instance.Character.Animator.SetTrigger("Call");

        Assign = true;

        VerifyDevice(baby);
    }

    void BabyActionOutput(InputAction.CallbackContext baby)
    {
        Assign = false;

        VerifyDevice(baby);
    }

    private void Awake()
    {
        _controls = new Controls();
        IsGamepad = false;
    }

    private void Update()
    {
        if (IsGamepad || InGame)
            Cursor.visible = false;
        else
            Cursor.visible = true;
    }
}
