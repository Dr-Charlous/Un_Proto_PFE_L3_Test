using System.Collections;
using UnityEngine;

[RequireComponent(typeof(InputManager))]

public class CamController : MonoBehaviour
{
    [HideInInspector] public float Position;
    [HideInInspector] public float Rotation;

    [Header("")]
    public float Acceleration;
    public float LimitMaxSpeed;
    public float DecreaseSpeed;
    public float Steering;

    [HideInInspector] public bool IsParalysed = false;
    [HideInInspector] public bool InputCollectBabies;
    [Header("Components/Values :")]
    public ObjectResonnance TrapResonnance;
    public Rigidbody Rb;
    public Animator Animator;

    [SerializeField] AudioSource _source;

    private void Start()
    {
        StartCoroutine(WaitCam(2));
    }

    void Update()
    {
        Rb.angularVelocity = Vector3.zero;

        if (!IsParalysed)
        {
            Movement();
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
        if (Position != 0 || Rotation != 0)
        {
            Vector3 direction = new Vector3 (-Rotation, 0, Position).normalized;

            if (Rb.velocity.magnitude < LimitMaxSpeed)
            {
                Rb.AddRelativeForce(direction * Acceleration * Time.fixedDeltaTime);
            }
        }

        Rb.velocity = Rb.velocity / DecreaseSpeed;

        if (_source != null)
            _source.volume = Rb.velocity.magnitude / LimitMaxSpeed;

        Animator.SetFloat("Move", Rb.velocity.magnitude, 0.1f, Time.deltaTime);
    }

    public IEnumerator WaitCam(float time)
    {
        IsParalysed = true; 
        yield return new WaitForSeconds(time);
        IsParalysed = false;
    }
}