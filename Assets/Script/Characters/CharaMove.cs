using UnityEngine;

[RequireComponent(typeof(InputManager))]

public class CharaMove : MonoBehaviour
{
    [Header("Input System :")]
    public float Position;
    public float Rotation;

    [Header("")]
    public float Acceleration;
    public float LimitMaxSpeed;
    public float DecreaseSpeed;
    public float Steering;

    [Header("Components/Values :")]
    public bool IsParalysed = false;
    [HideInInspector] public bool InputCollectBabies;
    public ObjectResonnance TrapResonnance;
    public Rigidbody Rb;
    public Animator Animator;

    int _valueRotateBack;

    private void Awake()
    {
        Rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (!IsParalysed)
        {
            Movement();
            Rotate();
        }
        else
        {
            Rb.velocity = Vector3.zero;

            if (TrapResonnance != null)
                TrapResonnance.BabyTarget.position += (TrapResonnance.BabyTarget.transform.right * Position + TrapResonnance.BabyTarget.transform.forward * Rotation) * TrapResonnance.SpeedBabyTarget * Time.deltaTime;
        }
    }

    void Movement()
    {
        if (Position != 0)
        {
            if (Rb.velocity.magnitude < LimitMaxSpeed)
            {
                if (Position < 0)
                    Rb.AddRelativeForce(Vector3.forward * Position * Acceleration * Time.fixedDeltaTime);
                else
                    Rb.AddRelativeForce(Vector3.forward * Position * Acceleration / 2 * Time.fixedDeltaTime);

                if (_valueRotateBack == -1 && Position > 0 && Rotation == 0)
                {
                    _valueRotateBack = Random.Range(0, 2);

                    if (_valueRotateBack == 0)
                        Rotation = -1;
                    else
                        Rotation = 1;
                }
                else if (_valueRotateBack != Rotation && Position > 0)
                    _valueRotateBack = (int)Rotation;
            }
        }
        else
            _valueRotateBack = -1;

        Rb.velocity = Rb.velocity / DecreaseSpeed;

        Animator.SetFloat("Move", Rb.velocity.magnitude, 0.1f, Time.deltaTime);
    }

    void Rotate()
    {
        if (Rotation != 0)
        {
            Rb.angularVelocity += Vector3.up * Rotation * Steering * Time.fixedDeltaTime;
        }
        else
        {
            Rb.angularVelocity = Rb.angularVelocity / DecreaseSpeed;
        }
    }
}