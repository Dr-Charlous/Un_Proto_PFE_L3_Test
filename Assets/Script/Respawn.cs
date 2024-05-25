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
            if (Spawns[i].ObjectTouch != null && Spawns[i].ObjectTouch.GetComponent<CharaMove>() != null)
            {
                RespawnPoint = Spawns[i].transform;
            }
        }
    }

    public void RespawnEntities()
    {
        for (int i = 0; i < Entities.Length; i++)
        {
            if (Entities[i].GetComponentInChildren<StateBabyController>() != null)
            {
                Entities[i].GetComponentInChildren<StateBabyController>().Agent.transform.position = new Vector3(RespawnPoint.position.x, Entities[i].position.y, RespawnPoint.position.z);
            }
            else
            Entities[i].position = new Vector3(RespawnPoint.position.x, Entities[i].position.y, RespawnPoint.position.z);
        }

        GameManager.Instance.EndPoursuite.Initialize();
    }
}
