using DG.Tweening;
using System.Collections;
using Unity.VisualScripting;
using UnityEditor.ShaderGraph;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(InputSystem))]
[RequireComponent(typeof(UI))]
public class CharaMove : MonoBehaviour
{
    [Header("Input System :")]
    Controls _controls;
    Vector3 _position;
    Vector3 _rotation;

    [Header("Components/Values :")]
    public float MoveSpeed = 1f;
    public float RotateSpeed = 100f;
    [Range(0, 5)]
    public int Life = 5;
    public bool Swimming = false;
    public Transform Body;
    public Rigidbody _rb;
    private UI _UIObject;

    [Header("Dash")]
    public bool IsDashing = false;
    public float DashForce = 5f;
    public float DashCooldown = 1f;

    [Header("Fishing :")]
    [Range(0f, 5f)]
    public int Fish = 0;
    public bool Fishinning = false;


    [Header("Water :")]
    public Transform water = null;

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
    }

    void GetMoveInputs(InputAction.CallbackContext move)
    {
        _position = new Vector3(0, 0, -move.ReadValue<Vector2>().y);
        _rotation = new Vector3(0, move.ReadValue<Vector2>().x, 0);
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
        _UIObject.ShowUI();
    }

    void GetUIInputCanceled(InputAction.CallbackContext obj)
    {
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
    }

    void Update()
    {
        if (Fishinning == false)
        {
            MoveNRotate(_position, MoveSpeed, _rotation, RotateSpeed);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            SwitchSwim();
        }

        FallingRotate();
        UpWater();
    }

    #region MoveNRotate
    void MoveNRotate(Vector3 transformMove, float moveSpeed, Vector3 transformRot, float rotateSpeed)
    {
        transform.Translate(transformMove * moveSpeed * Time.deltaTime);

        if (transformMove.z != 0)
        {
            transform.Rotate(transformRot * rotateSpeed * Time.deltaTime);
        }
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
    #endregion

    #region water
    void SwitchSwim()
    {
        Swimming = !Swimming;
    }

    void UpWater()
    {
        if (water != null)
        {
            if (Swimming && water.GetComponent<Water>().collider.enabled == false)
            {
                var rot = transform.eulerAngles;
                rot.x = 0;
                rot.z = 0;
                transform.eulerAngles = rot;

                transform.position = new Vector3(transform.position.x, water.position.y - 0.1f, transform.position.z);

                _rb.velocity = Vector3.zero;
                Destroy(_rb);
                _rb = null;

                water.GetComponent<Water>().collider.enabled = true;

                _rb = transform.AddComponent<Rigidbody>();
            }
            else if (Swimming == false)
            {
                water.GetComponent<Water>().collider.enabled = false;
            }
        }

        if (_rb == null)
            _rb = transform.AddComponent<Rigidbody>();
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