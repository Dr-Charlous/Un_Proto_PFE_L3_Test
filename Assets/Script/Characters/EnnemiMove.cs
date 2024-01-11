using UnityEngine;
using UnityEngine.AI;

public class EnnemiMove : MonoBehaviour
{
    public NavMeshAgent Character;
    public Vector3[] RoundPositions;

    private int _i;

    private void Start()
    {
        Character.SetDestination(RoundPositions[0]);
        _i = 0;
    }

    private void Update()
    {
        if (Character.velocity.magnitude < 1 && Vector3.Distance(new Vector3(transform.position.x, 0, transform.position.z), new Vector3(RoundPositions[_i].x, 0, RoundPositions[_i].z)) < 1)
        {
            Debug.Log("Check");

            if (_i + 1 < RoundPositions.Length)
                _i += 1;
            else
                _i = 0;

            Character.SetDestination(RoundPositions[_i]);
        }
    }

    private void OnDrawGizmos()
    {
        foreach (var position in RoundPositions)
        {
            Gizmos.DrawCube(position, Vector3.one);
        }
    }
}
