using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput))]
public class InputManager : MonoBehaviour
{
    Controls _controls;
    [SerializeField] CharaMove Chara;

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
        _controls.Diplocaulus.Quest.started += GetUIInput;
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
        _controls.Diplocaulus.Quest.started -= GetUIInput;
    }

    void GetMoveInputs(InputAction.CallbackContext move)
    {
        Chara.Position = -move.ReadValue<Vector2>().y;
        Chara.Rotation = move.ReadValue<Vector2>().x;
    }

    void GetCollectInputs(InputAction.CallbackContext collect)
    {
        Chara.Collected = !Chara.Collected;
    }

    void GetDashInput(InputAction.CallbackContext dash)
    {
        if (Chara.IsDashing == false)
        {
            StartCoroutine(Chara.Dash());
        }
    }

    private void GetKidsInputMouse(InputAction.CallbackContext input)
    {
        if (int.Parse(input.action.ReadValueAsObject().ToString()) > 0 && Chara.BabieNumber < Chara.Babies.Length)
        {
            Chara.BabieNumber++;
        }
        else if (int.Parse(input.action.ReadValueAsObject().ToString()) < 0 && Chara.BabieNumber > 1)
        {
            Chara.BabieNumber--;
        }
    }

    private void GetKidsInputGamePad(InputAction.CallbackContext input)
    {
        if (input.action.ReadValue<Vector2>().x == 1 && Chara.Babies.Length > 1)
        {
            Chara.BabieNumber = 1;
        }
        else if (input.action.ReadValue<Vector2>().x == -1 && Chara.Babies.Length > 3)
        {
            Chara.BabieNumber = 3;
        }
        else if (input.action.ReadValue<Vector2>().y == 1 && Chara.Babies.Length > 2)
        {
            Chara.BabieNumber = 2;
        }
        else if (input.action.ReadValue<Vector2>().y == -1)
        {
            Chara.BabieNumber = 0;
        }
    }

    void GetBabyFollowInput(InputAction.CallbackContext baby)
    {
        Chara._BabyManager.BabyFollow();
    }

    void GetBabyActionInput(InputAction.CallbackContext baby)
    {
        Chara._BabyManager.BabyAction();
    }

    void GetBabyGetInput(InputAction.CallbackContext baby)
    {
        Chara._BabyManager.GetBaby(Chara.BabieNumberOnBack);
    }

    private void GetUIInput(InputAction.CallbackContext ui)
    {
        if (Chara.UI.activeInHierarchy)
            Chara.UI.SetActive(false);
        else
            Chara.UI.SetActive(true);
    }

    private void Awake()
    {
        _controls = new Controls();
    }
    #endregion   
}
