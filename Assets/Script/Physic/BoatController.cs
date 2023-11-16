using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharaMove))]
public class BoatController : MonoBehaviour
{
    public Transform Motor;
    public float SteerPower = 500f;
    public float Power = 5f;
    public float MaxSpeed = 10f;

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
            MoveNRotate();
        }
        
    }

    void MoveNRotate()
    {
        //direction de base
        var forceDirection = transform.forward;
        var steer = 0f;

        //steer direction
        steer = chara.Rotation;

        //force rotation
        rb.AddForceAtPosition(steer * transform.right * SteerPower / 100f, Motor.position);

        //calcul verteurs
        var forward = Vector3.Scale(new Vector3(1, 0, 1), transform.forward);

        //devant et derriere
        var goalV = new Vector3();
        if (chara.Position > 0)
        {
            goalV = forward * MaxSpeed * Power * Time.deltaTime;
        }
        else if (chara.Position < 0)
        {
            goalV = forward * -MaxSpeed * Power * Time.deltaTime;
        }

        Debug.DrawLine(transform.position, transform.position + goalV);
        rb.AddForce((goalV - rb.velocity) * rb.mass * Time.fixedDeltaTime);
    }
}
