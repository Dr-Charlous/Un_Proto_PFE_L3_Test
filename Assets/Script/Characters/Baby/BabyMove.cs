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
    public NavMeshAgent agent;
    public LineRenderer line;
    public List<Vector3> point;

    public GameObject ObjectBaby;

    [Header("Parent follow :")]
    public Transform Parent;
    public Vector3 target;
    public float distance = 5;
    public bool showPath = true;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        target = transform.position;
    }

    void Update()
    {
        if (State == state.Follow && Vector3.Distance(Parent.position, target) > distance)
        {
            target = Parent.position;
            agent.SetDestination(target);
        }

        if (State == state.Ride)
        {
            
        }

        if (showPath)
        {
            DrawPath();
        }

        BodyFollow();

        FallingRotate();
    }

    private void BodyFollow()
    {
        Vector3 Direction = agent.velocity;

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
        if (agent != null && (agent.velocity.y < -10 || agent.velocity.y > 10))
        {
            var rot = transform.eulerAngles;
            rot.x = 0;
            rot.z = 0;
            transform.eulerAngles = rot;
        }
    }

    void DrawPath()
    {
        if (agent.path.corners.Length < 2) return;

        int i = 1;
        while (i < agent.path.corners.Length)
        {
            line.positionCount = agent.path.corners.Length;
            point = agent.path.corners.ToList();
            for (int j = 0; j < point.Count; j++)
            {
                line.SetPosition(j, point[j]);
            }

            i++;
        }
    }
}
