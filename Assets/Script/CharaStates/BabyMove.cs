using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class BabyMove : MonoBehaviour
{
    [Header("Values :")]
    public float moveSpeed = 1f;
    public float rotateSpeed = 100f;
    public bool Swimming = false;

    [Header("Water :")]
    public bool IsInWater = false;
    public Transform water = null;

    [Header("Components :")]
    public Transform Body;
    public Rigidbody _rb;
    public NavMeshAgent agent;
    public LineRenderer line;
    public List<Vector3> point;

    [Header("Parent follow :")]
    public Transform Parent;
    public Vector3 target;
    public float distance = 5;

    void Start()
    {
        Body = GetComponent<Transform>();
        _rb = GetComponent<Rigidbody>();
        agent = GetComponent<NavMeshAgent>();
        target = transform.position;
    }

    void Update()
    {
        if (Vector3.Distance(Parent.position, target) > distance)
        {
            target = Parent.position;
            agent.SetDestination(target);
        }


        DrawPath();

        //if (Input.GetKeyDown(KeyCode.Space))
        //{
        //    SwitchSwim();
        //}

        FallingRotate();
        UpWater();
    }

    void FallingRotate()
    {
        if (_rb != null && (_rb.velocity.y < -10 || _rb.velocity.y > 10))
        {
            var rot = transform.eulerAngles;
            rot.x = 0;
            rot.z = 0;
            transform.eulerAngles = rot;
        }
    }

    void SwitchSwim()
    {
        Swimming = !Swimming;
    }

    void UpWater()
    {
        if (water != null)
        {
            if (IsInWater && Swimming && water.GetComponent<Water>().collider.enabled == false)
            {
                var rot = transform.eulerAngles;
                rot.x = 0;
                rot.z = 0;
                transform.eulerAngles = rot;

                transform.position = new Vector3(transform.position.x, water.position.y - 0.1f, transform.position.z);

                _rb.velocity = Vector3.zero;
                Destroy(_rb);
                _rb = null;

                water.GetComponent<Water>().collider.enabled = true;

                _rb = transform.AddComponent<Rigidbody>();
            }
            else if (Swimming == false)
            {
                water.GetComponent<Water>().collider.enabled = false;
            }
        }

        if (_rb == null)
            _rb = transform.AddComponent<Rigidbody>();
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
