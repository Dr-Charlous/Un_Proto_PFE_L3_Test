using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class StateBabyController : MonoBehaviour
{
    public IState currentState;
    public StateBabyStay StateStay = new StateBabyStay();
    public StateBabyFollow StateFollow = new StateBabyFollow();
    public StateBabyRide StateRide = new StateBabyRide();
    public StateBabyAction StateAction = new StateBabyAction();
    public StateBabySelect StateSelect = new StateBabySelect();

    [Header("Components :")]
    public NavMeshAgent Agent;
    public LineRenderer Line;
    public List<Vector3> Point;

    public GameObject ObjectBaby;
    public GameObject BabyMesh;
    public NestCreation Nest;

    [Header("Parent follow :")]
    public Transform Parent;
    public Vector3 Target;
    public float Distance = 5;
    public bool ShowPath = true;

    private void Start()
    {
        ChangeState(StateFollow);
        Target = transform.position;
    }

    private void Update()
    {
        if (currentState != null)
        {
            currentState.UpdateState(this);
        }

        if (ShowPath)
        {
            DrawPath();
        }

        BodyFollow();

        FallingRotate();
    }

    public void ChangeState(IState newState)
    {
        if (currentState != null)
        {
            currentState.OnExit(this);
        }
        currentState = newState;
        currentState.OnEnter(this);
    }

    private void BodyFollow()
    {
        Vector3 Direction = Agent.velocity;

        if (Direction != Vector3.zero)
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

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, Distance);
    }
}

public interface IState
{
    public void OnEnter(StateBabyController controller);
    public void UpdateState(StateBabyController controller);
    public void OnExit(StateBabyController controller);
}