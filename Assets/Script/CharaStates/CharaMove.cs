using Unity.Burst.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class CharaMove : MonoBehaviour
{
    [Header("Values :")]
    public float moveSpeed = 5f;
    public float rotateSpeed = 0.5f;
    public float gravityScaleGround = 1f;
    public float gravityScaleWater = 0f;
    public bool Swimming = false;

    [Header("Water :")]
    public bool IsInWater = false;
    public Transform water = null;
    public LayerMask layerMaskGround;
    public LayerMask layerMaskWater;

    [Header("Components :")]
    public Transform Body;
    public Rigidbody _rb;
    RaycastHit hit;

    void Start()
    {
        if (GetComponent<Gravity>())
        {
            Body = GetComponent<Transform>();
            _rb = GetComponent<Rigidbody>();
        }
    }

    void Update()
    {
        Move(KeyCode.UpArrow, Vector3.back, moveSpeed);
        Move(KeyCode.DownArrow, Vector3.forward, moveSpeed);
        Rotate(KeyCode.LeftArrow, Vector3.down, rotateSpeed);
        Rotate(KeyCode.RightArrow, Vector3.up, rotateSpeed);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            SwitchSwim();
        }

        FallingRotate();
        UpWater();
    }

    void Move(KeyCode key, Vector3 transformMove, float moveSpeed)
    {
        if (Input.GetKey(key))
        {
            transform.Translate(transformMove * moveSpeed * Time.deltaTime);
        }
    }

    void Rotate(KeyCode key, Vector3 transformMove, float rotateSpeed)
    {
        if (Input.GetKey(key))
        {
            transform.Rotate(transformMove * rotateSpeed * Time.deltaTime);
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

    private void OnDrawGizmos()
    {
        Debug.DrawRay(new Vector3(transform.position.x, transform.position.y + 0.1f, transform.position.z), transform.TransformDirection(Vector3.back) * 1, Color.yellow);
    }
}