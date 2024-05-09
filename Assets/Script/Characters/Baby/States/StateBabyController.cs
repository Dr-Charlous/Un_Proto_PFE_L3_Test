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
    public StateBabyCollect StateCollect = new StateBabyCollect();
    //public StateBabyAnim StateAnim = new StateBabyAnim();

    [Header("Components :")]
    public NavMeshAgent Agent;
    public LineRenderer Line;
    public List<Vector3> Point;

    [Header("Babies stuffs :")]
    public GameObject ObjectBaby;
    public GameObject BabyMesh;
    public NestCreation Nest;
    public bool IsParalysed = false;
    public int Charges = 1;

    [Header("ParentTag follow :")]
    public Transform Parent;
    public Transform TargetParent;
    public Transform Target;
    public float Distance = 5;
    public bool ShowPath = true;

    [Header("Collect object")]
    public bool isTransporting = false;
    public bool isGrab = false;
    public GameObject TargetObject;
    public Transform ParentObject;
    public Transform ParentCollect;
    public OnTriggerEnterScript OnTriggerEnterScript;

    private void Start()
    {
        ChangeState(StateFollow);
    }

    private void Update()
    {
        if (!IsParalysed)
        {
            if (currentState != null)
            {
                currentState.UpdateState(this);
            }

            if (ShowPath)
            {
                DrawPath();
            }

            if (OnTriggerEnterScript.isTrigger && OnTriggerEnterScript.ObjectTouch.GetComponent<ObjectCollect>() != null
            && isTransporting == false
            && Parent.GetComponent<CharaMove>().TrapResonnance.IsPlayerInside)
            {
                TargetObject = OnTriggerEnterScript.ObjectTouch;
                GetObj();
            }

            if (Target == null)
                Target = transform.parent;
        }
        BodyFollow();
    }

    public void ChangeState(IState newState)
    {
        if (currentState != null)
        {
            currentState.OnExit(this);
        }
        IsParalysed = false;
        currentState = newState;
        currentState.OnEnter(this);
    }

    private void BodyFollow()
    {
        Vector3 Direction = Agent.velocity;

        if (Direction != Vector3.zero)
        {
            Quaternion lookRotation = Quaternion.LookRotation(Direction);
            Vector3 rotation = Quaternion.Lerp(ObjectBaby.transform.rotation, lookRotation, Time.deltaTime * 10).eulerAngles;
            ObjectBaby.transform.rotation = Quaternion.Euler(0f, rotation.y, 0f);
        }

        ObjectBaby.transform.DOKill();
        ObjectBaby.transform.DOMove(transform.position, 0.5f);
        //ObjectBaby.transform.position = transform.position;
    }

    public void GetObj()
    {
        TargetObject.transform.SetParent(ParentCollect);
        TargetObject.GetComponent<BoxCollider>().excludeLayers -= LayerMask.GetMask("Player");
        isTransporting = true;
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