using DG.Tweening;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

//[RequireComponent(typeof(BoatController))]
public class BabyMove : MonoBehaviour
{
    public enum state
    {
        Stay,
        Follow,
        Action,
        Ride
    }

    public state State = state.Stay;

    [Header("Components :")]
    public NavMeshAgent Agent;
    public LineRenderer Line;
    public List<Vector3> Point;

    public GameObject ObjectBaby;

    [Header("Parent follow :")]
    public Transform Parent;
    public Vector3 Target;
    public float Distance = 5;
    public bool ShowPath = true;

    void Start()
    {
        Target = transform.position;
    }

    void Update()
    {
        if (State == state.Follow && Vector3.Distance(Parent.position, Target) > Distance)
        {
            Target = Parent.position;
            Agent.SetDestination(Target);
        }

        if (State == state.Ride)
        {
            
        }

        if (ShowPath)
        {
            DrawPath();
        }

        BodyFollow();

        FallingRotate();
    }

    private void BodyFollow()
    {
        Vector3 Direction = Agent.velocity;

        if (Direction !=  Vector3.zero)
        {
            Quaternion lookRotation = Quaternion.LookRotation(Direction);
            Vector3 rotation = Quaternion.Lerp(ObjectBaby.transform.rotation, lookRotation, Time.deltaTime * 1).eulerAngles;
            ObjectBaby.transform.rotation = Quaternion.Euler(0f, rotation.y, 0f);
        }

        ObjectBaby.transform.DOKill();
        ObjectBaby.transform.DOMove(transform.position, 1);
    }

    void FallingRotate()
    {
        if (Agent != null && (Agent.velocity.y < -10 || Agent.velocity.y > 10))
        {
            var rot = transform.eulerAngles;
            rot.x = 0;
            rot.z = 0;
            transform.eulerAngles = rot;
        }
    }

    void DrawPath()
    {
        if (Agent.path.corners.Length < 2) return;

        int i = 1;
        while (i < Agent.path.corners.Length)
        {
            Line.positionCount = Agent.path.corners.Length;
            Point = Agent.path.corners.ToList();
            for (int j = 0; j < Point.Count; j++)
            {
                Line.SetPosition(j, Point[j]);
            }

            i++;
        }
    }
}
