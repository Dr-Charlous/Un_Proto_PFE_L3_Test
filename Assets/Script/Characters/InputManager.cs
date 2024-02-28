using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput))]
public class InputManager : MonoBehaviour
{
    Controls _controls;
    [SerializeField] CharaMove Chara;

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
        var babies = Chara.BabyManager;
        if (babies.Babies.Length > 0)
        {
            babies.ChangeOutlineBaby(babies.BabieNumber, 0);

            if (int.Parse(input.action.ReadValueAsObject().ToString()) > 0 && babies.BabieNumber < babies.Babies.Length - 1)
            {
                babies.BabieNumber++;
            }
            else if (int.Parse(input.action.ReadValueAsObject().ToString()) < 0 && babies.BabieNumber > 0)
            {
                babies.BabieNumber--;
            }

            babies.ChangeOutlineBaby(babies.BabieNumber, 1.1f);
        }
    }

    private void GetKidsInputGamePad(InputAction.CallbackContext input)
    {
        var babies = Chara.BabyManager;

        if (babies.Babies.Length > 0)
        {
            babies.ChangeOutlineBaby(babies.BabieNumber, 0);

            if (input.action.ReadValue<Vector2>().x == 1 && babies.Babies.Length > 1)
            {
                babies.BabieNumber = 1;
            }
            else if (input.action.ReadValue<Vector2>().x == -1 && babies.Babies.Length > 3)
            {
                babies.BabieNumber = 3;
            }
            else if (input.action.ReadValue<Vector2>().y == 1 && babies.Babies.Length > 2)
            {
                babies.BabieNumber = 2;
            }
            else if (input.action.ReadValue<Vector2>().y == -1)
            {
                babies.BabieNumber = 0;
            }

            babies.ChangeOutlineBaby(babies.BabieNumber, 1.1f);
        }
    }

    void GetBabyFollowInput(InputAction.CallbackContext baby)
    {
        if (Chara.BabyManager.Babies.Length > 0)
            Chara.BabyManager.BabyFollow();
    }

    void GetBabyActionInput(InputAction.CallbackContext baby)
    {
        if (Chara.BabyManager.Babies.Length > 0)
            Chara.BabyManager.BabyAction();
    }

    void GetBabyGetInput(InputAction.CallbackContext baby)
    {
        if (Chara.BabyManager.Babies.Length > 0)
            Chara.BabyManager.CanWeGetBaby(Chara.BabyManager.BabieNumberOnBack);
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

        var babies = Chara.BabyManager;

        for (int i = 0; i < babies.Babies.Length; i++)
        {
            babies.ChangeOutlineBaby(i, 0);
        }

        if (babies.Babies.Length > 0)
            babies.ChangeOutlineBaby(babies.BabieNumber, 1.1f);
    }
}
