using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiFollowing : MonoBehaviour
{
    Camera _cam;
    SpriteRenderer _render;
    [SerializeField] float _distance;

    private void Start()
    {
        _cam = Camera.main;
        _render = GetComponentInChildren<SpriteRenderer>();
    }

    private void Update()
    {
        if (Vector3.Distance(GameManager.Instance.Character.transform.position, transform.position) < _distance)
        {
            _render.enabled = true;
            transform.LookAt(_cam.transform.position);
        }
        else
        {
            _render.enabled = false;
        }
    }
}
