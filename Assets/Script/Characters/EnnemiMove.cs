using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class EnnemiMove : MonoBehaviour
{
    public NavMeshAgent Character;
    public GameObject EnnemyMesh;
    public Transform[] RoundPositions;
    public float Speed = 10;

    public ObjectResonnance[] Resonance;
    public GameObject Fish;

    private int _i;
    private float _timer = 0;

    private void Start()
    {
        _i = 0;
        transform.position = RoundPositions[_i].position;
        EnnemyMesh.transform.position = new Vector3(transform.position.x, EnnemyMesh.transform.position.y, transform.position.z);
    }

    private void Update()
    {
        bool isThereSounds = false;

        for (int i = 0; i < Resonance.Length; i++)
        {
            if (Resonance[i].IsResonating)
            {
                if (Fish != null && Fish.activeInHierarchy)
                {
                    Move(Fish.transform.position);
                }
                else
                {
                    Move(Resonance[i].transform.position);
                }

                isThereSounds = true;
                break;
            }
        }

        if (isThereSounds == false)
        {
            FollowPath();
        }
    }

    void FollowPath()
    {
        _timer += Time.deltaTime;

        if (Character.velocity.magnitude < 1 && Vector3.Distance(new Vector3(transform.position.x, 0, transform.position.z), new Vector3(RoundPositions[_i].position.x, 0, RoundPositions[_i].position.z)) < 1)
        {
            if (_i + 1 < RoundPositions.Length)
                _i += 1;
            else
                _i = 0;

            Move(new Vector3(RoundPositions[_i].position.x, transform.position.y, RoundPositions[_i].position.z));
        }

        BodyFollow();
    }

    void Move(Vector3 destinationPath)
    {
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
        float distance = (new Vector3(RoundPositions[_i].position.x, 0, RoundPositions[_i].position.z) - new Vector3(transform.position.x, 0, transform.position.z)).magnitude;

        //EnnemyMesh.transform.DOMove(destinationPos, distance * Speed * Time.deltaTime);
        EnnemyMesh.transform.position = destinationPos;
    }

    private void OnDrawGizmos()
    {
        foreach (var position in RoundPositions)
        {
            Gizmos.DrawWireSphere(position.position, 0.5f);
        }
    }
}
