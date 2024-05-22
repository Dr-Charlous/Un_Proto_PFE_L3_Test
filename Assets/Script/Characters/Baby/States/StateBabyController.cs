using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem.XR;
using UnityEngine.TextCore.Text;

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
    public Animator Animator;
    public ScriptableDialogue Dialogue;
    public UiTextDialogueSpeaker Speaker;

    [Header("Babies stuffs :")]
    public GameObject ObjectBaby;
    public GameObject BabyMesh;
    public bool IsParalysed = false;
    public int Charges = 1;

    [Header("ParentTag follow :")]
    public Transform Parent;
    public Transform TargetParent;
    public Transform Target;
    public float Distance = 5;
    public bool ShowPath = true;
    public bool IsInNest = false;

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
        IsInNest = false;

        Parent = GameManager.Instance.Character.transform;
        Speaker = Parent.GetComponentInChildren<UiTextDialogueSpeaker>();
    }

    private void Update()
    {
        if (!IsParalysed)
        {
            if (GameManager.Instance.Nest == null || !GameManager.Instance.Nest.IsCreated || (GameManager.Instance.Nest.IsCreated && GameManager.Instance.Nest.IsFeed))
            {
                if (currentState != null)
                {
                    currentState.UpdateState(this);
                }

                if (ShowPath)
                {
                    DrawPath();
                }

                if (OnTriggerEnterScript.isTrigger && OnTriggerEnterScript.ObjectTouch != null && isTransporting == false && GameManager.Instance.Character.TrapResonnance != null)
                {
                    if (OnTriggerEnterScript.ObjectTouch.GetComponent<ObjectCollect>() != null && GameManager.Instance.Character.TrapResonnance.IsPlayerInside)
                    {
                        TargetObject = OnTriggerEnterScript.ObjectTouch;
                        Dialogue = OnTriggerEnterScript.ObjectTouch.GetComponent<ObjectCollect>().DialogueBabyReccup;
                        GetObj(OnTriggerEnterScript.ObjectTouch.GetComponent<ObjectCollect>().IsPortable);
                    }
                }

                if (Target == null)
                    Target = transform.parent;
            }
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

        Animator.SetFloat("Move", Agent.velocity.magnitude, 0.1f, Time.deltaTime);
    }

    public void GetObj(bool var)
    {
        if (var)
        {
            TargetObject.transform.SetParent(ParentCollect);
            TargetObject.GetComponent<BoxCollider>().excludeLayers -= LayerMask.GetMask("Player");
            isTransporting = true;

            Animator.SetTrigger("GetObj");
        }

        if (Speaker != null && Dialogue != null)
            Speaker.StartDialogue(Dialogue);
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