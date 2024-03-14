using DG.Tweening;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class EnnemiMove : MonoBehaviour
{
    public NavMeshAgent Character;
    public GameObject EnnemyMesh;
    public Vector3[] RoundPositions;
    public float Speed = 10;

    private int _i;

    private void Start()
    {
        _i = 0;
    }

    private void Update()
    {
        if (Character.velocity.magnitude < 1 && Vector3.Distance(new Vector3(transform.position.x, 0, transform.position.z), new Vector3(RoundPositions[_i].x, 0, RoundPositions[_i].z)) < 1)
        {
            if (_i + 1 < RoundPositions.Length)
                _i += 1;
            else
                _i = 0;

            Vector3 destination = new Vector3(RoundPositions[_i].x, transform.position.y, RoundPositions[_i].z);
            Character.SetDestination(destination);
        }

        BodyFollow();
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
        float distance = (new Vector3(RoundPositions[_i].x, 0, RoundPositions[_i].z) - new Vector3(transform.position.x, 0, transform.position.z)).magnitude;
        EnnemyMesh.transform.DOMove(destinationPos, distance * Speed * Time.deltaTime);
        //EnnemyMesh.transform.position = destinationPos;
    }

    private void OnDrawGizmos()
    {
        foreach (var position in RoundPositions)
        {
            Gizmos.DrawWireSphere(position, 0.5f);
        }
    }
}
