using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput))]
public class InputManager : MonoBehaviour
{
    Controls _controls;

    [HideInInspector] public float Vertical;
    [HideInInspector] public float Horizontal;
    [HideInInspector] public bool Call;
    [HideInInspector] public bool Assign;

    private void OnEnable()
    {
        _controls.Diplocaulus.Enable();
        _controls.Diplocaulus.Move.performed += GetMoveInputs;

        _controls.Diplocaulus.CamMove.performed += GetCamMoveInputs;
        _controls.Diplocaulus.CamReset.started += GetCamResetInput;

        _controls.Diplocaulus.BabyFollow.started += GetBabyFollowInput;
        _controls.Diplocaulus.BabyFollow.canceled += BabyFollowOutput;

        _controls.Diplocaulus.BabyAction.started += GetBabyActionInput;
        _controls.Diplocaulus.BabyAction.canceled += BabyActionOutput;
    }

    private void OnDisable()
    {
        _controls.Diplocaulus.Disable();
        _controls.Diplocaulus.Move.performed -= GetMoveInputs;

        _controls.Diplocaulus.CamMove.performed -= GetCamMoveInputs;
        _controls.Diplocaulus.CamReset.started -= GetCamResetInput;

        _controls.Diplocaulus.BabyFollow.started -= GetBabyFollowInput;
        _controls.Diplocaulus.BabyFollow.canceled -= BabyFollowOutput;

        _controls.Diplocaulus.BabyAction.started -= GetBabyActionInput;
        _controls.Diplocaulus.BabyAction.canceled -= BabyActionOutput;
    }

    void GetMoveInputs(InputAction.CallbackContext move)
    {
        GameManager.Instance.Character.Position = -move.ReadValue<Vector2>().y;
        GameManager.Instance.Character.Rotation = move.ReadValue<Vector2>().x;
        
        Vertical = -move.ReadValue<Vector2>().y;
        Horizontal = move.ReadValue<Vector2>().x;
    }

    void GetCamMoveInputs(InputAction.CallbackContext move)
    {
        GameManager.Instance.CamManager.Rotation = move.ReadValue<Vector2>().x;
    }

    void GetCamResetInput(InputAction.CallbackContext reset)
    {
        GameManager.Instance.CamManager.Reset();
    }

    void GetBabyFollowInput(InputAction.CallbackContext baby)
    {
        if (GameManager.Instance.BabyManager.BabiesInScene.Count > 0 && !GameManager.Instance.Character.IsParalysed)
            GameManager.Instance.BabyManager.BabyFollow();
        else if (GameManager.Instance.Character.TrapResonnance != null)
        {
            GameManager.Instance.Character.TrapResonnance.PlayerGetOutside();
        }

        GameManager.Instance.Character.GetComponentInChildren<Animator>().SetTrigger("Call");

        Call = true;
    }

    void BabyFollowOutput(InputAction.CallbackContext baby)
    {
        Call = false;
    }
    
    void GetBabyActionInput(InputAction.CallbackContext baby)
    {
        if (!GameManager.Instance.Character.IsParalysed)
        {
            GameManager.Instance.BabyManager.BabyAction();

            GameManager.Instance.Character.InputCollectBabies = !GameManager.Instance.Character.InputCollectBabies;
        }

        GameManager.Instance.Character.GetComponentInChildren<Animator>().SetTrigger("Call");

        Assign = true;
    }

    void BabyActionOutput(InputAction.CallbackContext baby)
    {
        Assign = false;
    }
    private void Awake()
    {
        _controls = new Controls();
    }
}
