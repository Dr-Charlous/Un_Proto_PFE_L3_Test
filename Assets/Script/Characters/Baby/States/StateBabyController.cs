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
    #region IState
    public IState currentState;
    public StateBabyFollow StateFollow = new StateBabyFollow();
    public StateBabyAction StateAction = new StateBabyAction();
    public StateBabyCollect StateCollect = new StateBabyCollect();
    //public StateBabyAnim StateAnim = new StateBabyAnim();
    #endregion

    [Header("Components :")]
    public NavMeshAgent Agent;
    public LineRenderer Line;
    public List<Vector3> Point;
    public Animator Animator;
    public ScriptableDialogue Dialogue;

    [Header("Babies stuffs :")]
    public GameObject ObjectBaby;
    [SerializeField] Vector3 _offSet;
    public bool IsParalysed = false;
    public int Charges = 1;

    [Header("ParentTag follow :")]
    public Transform Parent;
    public Transform TargetParent;
    public Transform Target;
    public float Distance = 5;
    public float DistanceUpdateMove = 0.25f;
    public bool ShowPath = true;
    public bool IsInNest = false;

    [Header("Collect object")]
    public bool isTransporting = false;
    public bool isGoingToGrab = false;
    public GameObject ObjectTransporting;
    public GameObject TargetObject;
    public Transform ParentObject;
    public Transform ParentCollect;
    public OnTriggerEnterScript OnTriggerEnterScript;

    [Header("Sounds :")]
    public AudioSource Source;
    public AudioClip[] Clips;

    private void Start()
    {
        ChangeState(StateFollow);
        IsInNest = false;

        Parent = GameManager.Instance.Character.transform;
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
                        GetObj(OnTriggerEnterScript.ObjectTouch.GetComponent<ObjectCollect>().IsPortable);

                        for (int i = 0; i < GameManager.Instance.BabyManager.BabiesInScene.Count; i++)
                        {
                            if (this == GameManager.Instance.BabyManager.BabiesInScene[0].GetComponent<RefBaby>().Controller)
                                break;
                            else
                                GameManager.Instance.BabyManager.ChangeOrder();
                        }
                    }
                }

                if (TargetObject != null && !TargetObject.activeInHierarchy)
                    TargetObject = null;

                if (Target == null)
                    Target = transform.parent;
            }
        }
    }

    private void LateUpdate()
    {
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
        ObjectBaby.transform.DOMove(transform.position + _offSet, 0.1f);
        //ObjectBaby.transform.position = Vector3.Lerp(ObjectBaby.transform.position, transform.position, 0.5f);

        Animator.SetFloat("Move", Agent.velocity.magnitude, 0.1f, Time.deltaTime);

        if (Agent.velocity.magnitude < 1 * Time.deltaTime && Random.Range(0, 500) == 0)
            Animator.SetTrigger("Roulade");
    }

    public void GetObj(bool var)
    {
        if (var)
        {
            isTransporting = true;
            ObjectTransporting = TargetObject;
            TargetObject.transform.SetParent(ParentCollect);
            TargetObject.GetComponent<BoxCollider>().excludeLayers += LayerMask.GetMask("Player");
            TargetObject.GetComponent<BoxCollider>().excludeLayers += LayerMask.GetMask("Babies");

            Animator.SetTrigger("GetObj");
        }
        isGoingToGrab = false;

        Dialogue = OnTriggerEnterScript.ObjectTouch.GetComponent<ObjectCollect>().DialogueBabyReccup;

        if (Dialogue != null && !TargetObject.GetComponent<ObjectCollect>().isListened)
        {
            GameManager.Instance.Speaker.StartDialogue(Dialogue);
            TargetObject.GetComponent<ObjectCollect>().isListened = true;
        }
    }

    public void ScaleMesh(Vector3 scale)
    {
        ObjectBaby.transform.localScale = scale;
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

    public void Scream()
    {
        if (!Source.isPlaying)
        {
            Source.clip = Clips[Random.Range(0, Clips.Length)];
            Source.Play();
        }
    }
}

public interface IState
{
    public void OnEnter(StateBabyController controller);
    public void UpdateState(StateBabyController controller);
    public void OnExit(StateBabyController controller);
}