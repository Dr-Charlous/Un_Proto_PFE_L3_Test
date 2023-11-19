using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharaMove))]
public class BoatController : MonoBehaviour
{
    public Transform Motor;
    [Header("On Water (Swim)")]
    public float SteerPower = 500f;
    public float Power = 5f;
    public float MaxSpeed = 10f;

    [Header("In Water (Dive)")]
    public float SteerPowerWater = 500f;
    public float PowerWater = 5f;
    public float MaxSpeedWater = 10f;

    protected Rigidbody rb;
    protected Quaternion startRot;
    private CharaMove chara;

    private void Start()
    {
        chara = GetComponent<CharaMove>();
        startRot = Motor.localRotation;
        rb = chara._rb;
    }

    private void FixedUpdate()
    {
        if (chara.Fishinning == false)
        {
            if (chara.Swimming)
            {
                MoveNRotate(SteerPower, Power, MaxSpeed, chara.Swimming);
            }
            else
            {
                MoveNRotate(SteerPowerWater, PowerWater, MaxSpeedWater, chara.Swimming);
            }
        }
    }

    void MoveNRotate(float steering, float power, float speed, bool swim)
    {
        //direction de base
        var forceDirection = transform.forward;
        var steer = 0f;

        //steer direction
        steer = chara.Rotation;

        //force rotation
        rb.AddForceAtPosition(steer * transform.right * steering / 100f, Motor.position);

        //calcul verteurs
        var forward = Vector3.Scale(new Vector3(1, 0, 1), transform.forward);

        //devant et derriere
        var goalV = new Vector3();
        if (chara.Position > 0)
        {
            goalV = forward * speed * power * Time.deltaTime;
        }
        else if (chara.Position < 0)
        {
            goalV = forward * -speed * power * Time.deltaTime;
        }

        Debug.DrawLine(transform.position, transform.position + goalV);

        if (swim)
        {
            rb.AddForce((goalV - rb.velocity) * rb.mass * Time.fixedDeltaTime);
        }
        else
        {
            rb.velocity = (goalV - rb.velocity) * rb.mass * Time.fixedDeltaTime;
        }
    }
}
