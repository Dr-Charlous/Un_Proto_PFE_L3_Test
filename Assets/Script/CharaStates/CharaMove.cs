using DG.Tweening;
using System.Collections;
using Unity.VisualScripting;
using UnityEditor.ShaderGraph;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharaMove : MonoBehaviour
{
    [Header("Values :")]
    public float MoveSpeed = 1f;
    public float RotateSpeed = 100f;
    public bool Swimming = false;

    [Header("Dash")]
    public bool IsDashing = false;
    public float DashForce = 5f;
    public float DashCooldown = 1f;

    [Header("Water :")]
    public bool IsInWater = false;
    public Transform water = null;

    [Header("Components :")]
    public Transform Body;
    public Rigidbody _rb;

    [Header("Input System :")]
    Controls _controls;
    Vector3 _position;
    Vector3 _rotation;

    #region Inputs
    private void OnEnable()
    {
        _controls.Diplocaulus.Enable();
        _controls.Diplocaulus.Move.performed += GetMoveInputs;
        _controls.Diplocaulus.Dash.started += GetDashInput;
    }

    private void OnDisable()
    {
        _controls.Diplocaulus.Disable();
        _controls.Diplocaulus.Move.performed -= GetMoveInputs;
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

    private void Awake()
    {
        _controls = new Controls();
    }
    #endregion


    void Start()
    {
        Body = GetComponent<Transform>();
        _rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        MoveNRotate(_position, MoveSpeed, _rotation, RotateSpeed);

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
            if (IsInWater && Swimming && water.GetComponent<Water>().collider.enabled == false)
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

    IEnumerator Dash()
    {
        IsDashing = true;
        var direction = transform.forward.normalized;
        _rb.AddForce(-direction * DashForce, ForceMode.Impulse);
        yield return new WaitForSeconds(DashCooldown);
        IsDashing = false;
    }
}