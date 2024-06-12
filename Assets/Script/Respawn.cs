using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawn : MonoBehaviour
{
    [SerializeField] Transform[] Entities;
    [SerializeField] OnTriggerEnterScript[] Spawns;
    public Transform RespawnPoint;

    private void Update()
    {
        for (int i = 0; i < Spawns.Length; i++)
        {
            if (Spawns[i].ObjectTouch != null && Spawns[i].ObjectTouch.GetComponent<CamController>() != null)
            {
                RespawnPoint = Spawns[i].transform;
            }
        }
    }

    public void RespawnEntities(bool isEnd)
    {
        for (int i = 0; i < Entities.Length; i++)
        {
            if (Entities[i].GetComponent<CamController>() != null)
                Entities[i].position = new Vector3(RespawnPoint.position.x, Entities[i].position.y, RespawnPoint.position.z);
            else
            {
                var babyState = Entities[i].GetComponent<RefBaby>().Controller;

                babyState.Agent.enabled = false;
                babyState.transform.position = new Vector3(RespawnPoint.position.x, Entities[i].position.y, RespawnPoint.position.z);
                babyState.Agent.enabled = true;
                babyState.Agent.SetDestination(new Vector3(RespawnPoint.position.x, Entities[i].position.y, RespawnPoint.position.z));

                babyState.ChangeState(babyState.StateFollow);
            }
        }

        if (isEnd)
            GameManager.Instance.EndPoursuite.Initialize();
    }
}
