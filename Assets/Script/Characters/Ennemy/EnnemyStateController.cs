using DG.Tweening;
using UnityEngine;
using UnityEngine.AI;

public class EnnemyStateController : MonoBehaviour
{
    public IStateEnnemy currentState;
    public StateEnnemyRound StateFollowRound = new StateEnnemyRound();
    public StateEnnemyBranch StateFollowBranch = new StateEnnemyBranch();
    public StateEnnemyFish StateFollowFish = new StateEnnemyFish();
    public StateEnnemyChase StateChase = new StateEnnemyChase();
    //public StateBabyAnim StateAnim = new StateBabyAnim();

    public EnnemyAnimations Animations;
    public Jaws JawsController;
    public NavMeshAgent Character;
    public GameObject EnnemyMesh;
    public Transform[] RoundPositions;
    public float Speed = 10;
    public float DistanceSee = 4;
    public float TimeSinceNoSee = 10;

    public ObjectResonnance[] Resonance;
    public GameObject Fish;
    public GameObject Target;

    public string ParentTag;
    public string BabiesTag;

    public int _i;
    public float DistanceNext;
    public bool isEating = false;
    public bool isChasing = false;
    public GameObject Jaw;


    private void Start()
    {
        _i = 0;
        transform.position = RoundPositions[_i].position;
        EnnemyMesh.transform.position = new Vector3(transform.position.x, EnnemyMesh.transform.position.y, transform.position.z);

        ChangeState(StateFollowRound);
    }

    private void Update()
    {
        if (currentState != null)
        {
            currentState.UpdateState(this);
        }

        if (!isEating)
            Check();

        BodyFollow();
        //Debug.Log(currentState.ToString());
    }

    public void Check()
    {
        if (isChasing)
            ChangeState(StateChase);
        else
        {
            bool isThereSounds = false;

            for (int i = 0; i < Resonance.Length; i++)
            {
                if (Resonance[i].IsResonating)
                {
                    if (Fish != null && Fish.activeInHierarchy)
                    {
                        ChangeState(StateFollowFish);
                    }
                    else
                    {
                        ChangeState(StateFollowBranch);
                    }

                    isThereSounds = true;
                    break;
                }
            }

            if (isThereSounds == false)
            {
                ChangeState(StateFollowRound);
            }
        }
    }

    public void Move(Vector3 destinationPath)
    {
        transform.DOComplete();
        Vector3 destination = new Vector3(destinationPath.x, transform.position.y, destinationPath.z);
        Character.SetDestination(destination);
    }

    private void BodyFollow()
    {
        Vector3 Direction = Character.velocity;

        if (Direction != Vector3.zero)
        {
            Quaternion lookRotation = Quaternion.LookRotation(Direction);
            Vector3 rotation = Quaternion.Lerp(EnnemyMesh.transform.rotation, lookRotation, Time.deltaTime * 10).eulerAngles;
            EnnemyMesh.transform.rotation = Quaternion.Euler(0f, rotation.y, 0f);
        }

        EnnemyMesh.transform.DOKill();

        Vector3 destinationPos = new Vector3(transform.position.x, EnnemyMesh.transform.position.y, transform.position.z);
        //float distance = (new Vector3(RoundPositions[_i].position.x, 0, RoundPositions[_i].position.z) - new Vector3(transform.position.x, 0, transform.position.z)).magnitude;

        //EnnemyMesh.transform.DOMove(destinationPos, distance * Speed * Time.deltaTime);
        EnnemyMesh.transform.position = destinationPos;
    }

    private void OnDrawGizmos()
    {
        foreach (var position in RoundPositions)
        {
            Gizmos.DrawWireSphere(position.position, 0.5f);
        }

        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * DistanceSee);
        Gizmos.DrawRay(transform.position, transform.TransformDirection(Vector3.forward + Vector3.left) * DistanceSee);
        Gizmos.DrawRay(transform.position, transform.TransformDirection(Vector3.forward + Vector3.right) * DistanceSee);
    }

    public void ChangeState(IStateEnnemy newState)
    {
        if (currentState != newState)
        {
            if (currentState != null)
            {
                currentState.OnExit(this);
            }
            currentState = newState;
            currentState.OnEnter(this);
        }
    }
}

public interface IStateEnnemy
{
    public void OnEnter(EnnemyStateController controller);
    public void UpdateState(EnnemyStateController controller);
    public void OnExit(EnnemyStateController controller);
}