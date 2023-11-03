using UnityEngine;
using static UnityEditor.PlayerSettings;

public class CharaMove : MonoBehaviour
{
    [Header("Values :")]
    public float moveSpeed = 5f;
    public float rotateSpeed = 0.5f;
    public float gravityScaleGround = 1f;
    public float gravityScaleWater = 0f;

    [Header("Water :")]
    public bool IsInWater = false;
    public Transform water = null;
    public LayerMask layerMaskGround;
    public LayerMask layerMaskWater;

    [Header("Components :")]
    public Transform Body;
    public Rigidbody _rb;
    public Gravity gravity;

    void Start()
    {
        if (GetComponent<Gravity>())
        {
            Body = GetComponent<Transform>();
            _rb = GetComponent<Rigidbody>();
            gravity = GetComponent<Gravity>();
        }
    }

    void Update()
    {
        Move(KeyCode.UpArrow, Vector3.back, moveSpeed);
        Move(KeyCode.DownArrow, Vector3.forward, moveSpeed);
        Rotate(KeyCode.LeftArrow, Vector3.down, rotateSpeed);
        Rotate(KeyCode.RightArrow, Vector3.up, rotateSpeed);

        TouchingGround();
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

    void TouchingGround()
    {
        if (_rb.velocity.y < -10)
        {
            var rot = transform.eulerAngles;
            rot.x = 0;
            rot.z = 0;
            transform.eulerAngles = rot;
        }

        Debug.Log(_rb.velocity.y);
    }
}