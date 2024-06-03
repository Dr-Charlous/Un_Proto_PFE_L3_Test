using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput))]
public class InputManager : MonoBehaviour
{
    Controls _controls;

    private void OnEnable()
    {
        _controls.Diplocaulus.Enable();
        _controls.Diplocaulus.Move.performed += GetMoveInputs;

        _controls.Diplocaulus.CamMove.performed += GetCamMoveInputs;
        _controls.Diplocaulus.CamReset.started += GetCamResetInput;

        _controls.Diplocaulus.BabyFollow.started += GetBabyFollowInput;
        _controls.Diplocaulus.BabyAction.started += GetBabyActionInput;
    }

    private void OnDisable()
    {
        _controls.Diplocaulus.Disable();
        _controls.Diplocaulus.Move.performed -= GetMoveInputs;

        _controls.Diplocaulus.CamMove.performed -= GetCamMoveInputs;
        _controls.Diplocaulus.CamReset.started -= GetCamResetInput;

        _controls.Diplocaulus.BabyFollow.started -= GetBabyFollowInput;
        _controls.Diplocaulus.BabyAction.started -= GetBabyActionInput;
    }

    void GetMoveInputs(InputAction.CallbackContext move)
    {
        GameManager.Instance.Character.Position = -move.ReadValue<Vector2>().y;
        GameManager.Instance.Character.Rotation = move.ReadValue<Vector2>().x;
    }
    
    void GetCamMoveInputs(InputAction.CallbackContext move)
    {
        GameManager.Instance.CamManager.Rotation = move.ReadValue<Vector2>().x;
    }
    
    void GetCamResetInput(InputAction.CallbackContext reset)
    {
        GameManager.Instance.CamManager.Reset();
    }

    void GetCollectInputs(InputAction.CallbackContext collect)
    {
        //if (!_chara.IsParalysed)
        //{
        //    _chara.Collected = !_chara.Collected;
        //    _chara.CollectedBabies = !_chara.CollectedBabies;
        //}
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
    }

    void GetBabyActionInput(InputAction.CallbackContext baby)
    {
        if (!GameManager.Instance.Character.IsParalysed)
        {
            GameManager.Instance.BabyManager.BabyAction();

            GameManager.Instance.Character.Collected = !GameManager.Instance.Character.Collected;
            GameManager.Instance.Character.CollectedBabies = !GameManager.Instance.Character.CollectedBabies;
        }

        GameManager.Instance.Character.GetComponentInChildren<Animator>().SetTrigger("Call");
    }

    void GetBabyGetInput(InputAction.CallbackContext baby)
    {
        //if (_chara.BabyManager.BabiesInScene.Count > 0)
        //_chara.BabyManager.CanWeGetBaby(_chara.BabyManager.BabieNumberOnBack);
    }

    private void GetUIInput(InputAction.CallbackContext ui)
    {
        //if (_chara.UI.activeInHierarchy)
        //{
        //    _taskBoard.HideTasks();
        //    _chara.UI.SetActive(false);
        //}
        //else
        //{
        //    _chara.UI.SetActive(true);
        //    _taskBoard.ShowTasks();
        //}
    }

    private void Awake()
    {
        _controls = new Controls();
    }
}
