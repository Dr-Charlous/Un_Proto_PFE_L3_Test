using System.Collections.Generic;
using System.Linq;
using Unity.AI.Navigation;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

//[RequireComponent(typeof(BoatController))]
public class BabyMove : MonoBehaviour
{
    [Header("Components :")]
    public NavMeshAgent agent;
    public LineRenderer line;
    public List<Vector3> point;
    private BoatController _BoatController;

    [Header("Parent follow :")]
    public Transform Parent;
    public Vector3 target;
    public float distance = 5;
    public bool follow = true;
    public bool showPath = true;

    void Start()
    {
        //_BoatController = GetComponent<BoatController>();
        agent = GetComponent<NavMeshAgent>();
        target = transform.position;
    }

    void Update()
    {
        if (Vector3.Distance(Parent.position, target) > distance && follow)
        {
            target = Parent.position;
            agent.SetDestination(target);
        }

        if (showPath)
        {
            DrawPath();
        }

        FallingRotate();
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
