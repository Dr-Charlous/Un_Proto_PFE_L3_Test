using UnityEngine;

//[RequireComponent(typeof(CharaMove))]
public class BoatController : MonoBehaviour
{
    public Transform Motor;
    [Header("On Water (Swim)")]
    public float SteerPower = 500f;
    public float Power = 5f;
    public float MaxSpeed = 10f;

    protected Rigidbody _rb;
    protected Quaternion _startRot;
    CharaMove _chara;

    private void Start()
    {
        _chara = GetComponent<CharaMove>();
        _startRot = Motor.localRotation;
        _rb = _chara._rb;
    }

    private void FixedUpdate()
    {
        MoveNRotate(SteerPower, Power, MaxSpeed);
    }

    void MoveNRotate(float steering, float power, float speed)
    {
        //direction de base
        var forceDirection = transform.forward;
        var steer = 0f;

        //steer direction
        steer = _chara.Rotation;


        //calcul verteurs
        var forward = Vector3.Scale(new Vector3(1, 0, 1), transform.forward);

        //devant et derriere
        var goalV = new Vector3();
        if (_chara.Position > 0)
        {
            goalV = forward * speed * power * Time.deltaTime;
        }
        else if (_chara.Position < 0)
        {
            goalV = forward * -speed * power * Time.deltaTime;
        }

        Debug.DrawLine(transform.position, transform.position + goalV);


        _rb.AddForce((goalV - _rb.velocity) * _rb.mass * Time.fixedDeltaTime);

        //force rotation
        _rb.AddForceAtPosition(-steer * transform.right * steering / 100f, Motor.position);

    }
}
