using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawn : MonoBehaviour
{
    [SerializeField] Transform[] _entities;
    [SerializeField] OnTriggerEnterScript[] _spawns;
    [SerializeField] DeathTrap[] _traps;
    [SerializeField] Material _materialOutline;
    public Transform RespawnPoint;

    private void Update()
    {
        for (int i = 0; i < _spawns.Length; i++)
        {
            if (_spawns[i].ObjectTouch != null && _spawns[i].ObjectTouch.GetComponent<CamController>() != null)
            {
                RespawnPoint = _spawns[i].transform;
            }
        }
    }

    public void RespawnEntities(bool isEnd)
    {
        _materialOutline.SetInt("_IsActive", 0);

        for (int i = 0; i < _entities.Length; i++)
        {
            if (_entities[i].GetComponent<CamController>() != null)
            {
                _entities[i].position = new Vector3(RespawnPoint.position.x, _entities[i].position.y, RespawnPoint.position.z);
                GameManager.Instance.CamManager.ChangeCam(GameManager.Instance.CamPlayer, 0.1f, false);
            }
            else
            {
                var babyState = _entities[i].GetComponent<RefBaby>().Controller;

                babyState.Agent.enabled = false;
                babyState.transform.position = new Vector3(RespawnPoint.position.x, _entities[i].position.y, RespawnPoint.position.z);
                babyState.Agent.enabled = true;
                babyState.Agent.SetDestination(new Vector3(RespawnPoint.position.x, _entities[i].position.y, RespawnPoint.position.z));

                babyState.ChangeState(babyState.StateFollow);
            }
        }

        if (_traps.Length > 0)
        {
            for (int i = 0; i < _traps.Length; i++)
            {
                _traps[i].Init();
            }
        }

        if (isEnd)
            GameManager.Instance.EndPoursuite.Initialize();
    }
}
