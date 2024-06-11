using UnityEngine;
using static Unity.VisualScripting.Member;

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

    [SerializeField] AudioSource _source;

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
                TrapResonnance.BabyTarget.GetComponent<Rigidbody>().velocity = (TrapResonnance.BabyTarget.transform.right * Position + TrapResonnance.BabyTarget.transform.forward * Rotation) * TrapResonnance.SpeedBabyTarget * Time.deltaTime;
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
            }
        }

        if (Rotation != 0)
        {
            if (Rb.velocity.magnitude < LimitMaxSpeed)
            {
                Rb.AddRelativeForce(Vector3.left * Rotation * Acceleration * Time.fixedDeltaTime);
            }
        }

        Rb.velocity = Rb.velocity / DecreaseSpeed;

        if (_source != null)
            _source.volume = Rb.velocity.magnitude / LimitMaxSpeed;

        Animator.SetFloat("Move", Rb.velocity.magnitude, 0.1f, Time.deltaTime);
    }

    void Rotate()
    {
        //if (Rotation > 0)
        //{
        //    //Rb.angularVelocity += Vector3.up * Rotation * Steering * Time.fixedDeltaTime;
        //    Rb.AddRelativeForce(Vector3.right * Position * Acceleration * Time.fixedDeltaTime);
        //}
        //else if(Rotation < 0)
        //{
        //    //Rb.angularVelocity = Rb.angularVelocity / DecreaseSpeed;
        //    Rb.AddRelativeForce(Vector3.left * Position * Acceleration * Time.fixedDeltaTime);
        //}
    }
}