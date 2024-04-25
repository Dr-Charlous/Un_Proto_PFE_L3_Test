using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput))]
public class InputManager : MonoBehaviour
{
    Controls _controls;
    [SerializeField] CharaMove _chara;
    [SerializeField] TasksManager _taskBoard;

    private void OnEnable()
    {
        _controls.Diplocaulus.Enable();
        _controls.Diplocaulus.Move.performed += GetMoveInputs;
        _controls.Diplocaulus.Collect.started += GetCollectInputs;
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
        _controls.Diplocaulus.BabyFollow.started -= GetBabyFollowInput;
        _controls.Diplocaulus.BabyAction.started -= GetBabyActionInput;
        _controls.Diplocaulus.BabyGet.started -= GetBabyGetInput;
        _controls.Diplocaulus.Quest.started -= GetUIInput;
    }

    void GetMoveInputs(InputAction.CallbackContext move)
    {
        _chara.Position = -move.ReadValue<Vector2>().y;
        _chara.Rotation = move.ReadValue<Vector2>().x;
    }

    void GetCollectInputs(InputAction.CallbackContext collect)
    {
        _chara.Collected = !_chara.Collected;
        _chara.CollectedBabies = !_chara.CollectedBabies;
    }

    void GetBabyFollowInput(InputAction.CallbackContext baby)
    {
        if (_chara.BabyManager.BabiesInScene.Count > 0)
            _chara.BabyManager.BabyFollow();
    }

    void GetBabyActionInput(InputAction.CallbackContext baby)
    {
        if (_chara.BabyManager.BabiesInScene.Count > 0)
            _chara.BabyManager.BabyAction();
    }

    void GetBabyGetInput(InputAction.CallbackContext baby)
    {
        if (_chara.BabyManager.BabiesInScene.Count > 0)
            _chara.BabyManager.CanWeGetBaby(_chara.BabyManager.BabieNumberOnBack);
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
