using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCamBegin : MonoBehaviour
{
    [SerializeField] float _speed;

    void Start()
    {
        GameManager.Instance.CamManager.ChangeCam(GameManager.Instance.CamPlayer, _speed, false);
        Destroy(gameObject);
    }
}
