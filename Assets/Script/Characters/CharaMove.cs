using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(BoatController))]
[RequireComponent(typeof(Gravity))]
[RequireComponent(typeof(InputManager))]

public class CharaMove : MonoBehaviour
{
    [Header("Input System :")]
    public float Position;
    public float Rotation;

    [Header("Components/Values :")]
    [Range(0, 5)]
    public int Life = 5;
    public bool Collected;
    public GameObject Floaters;
    public GameObject ParticuleSystem;
    public Rigidbody _rb;
    public GameObject UI;

    [Header("Dash")]
    public bool IsDashing = false;
    public float DashForce = 5f;
    public float DashCooldown = 1f;

    //private UI _UIObject;
    public BabyManager BabyManager;
    BoatController _BoatController;
    Gravity _Gravity;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _BoatController = GetComponent<BoatController>();
        _Gravity = GetComponent<Gravity>();

        UI.SetActive(false);
    }

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

    public IEnumerator Dash()
    {
        IsDashing = true;
        var direction = transform.forward.normalized;
        _rb.AddForce(-direction * DashForce, ForceMode.Impulse);
        yield return new WaitForSeconds(DashCooldown);
        IsDashing = false;
    }
}