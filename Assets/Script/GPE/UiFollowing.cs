using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiFollowing : MonoBehaviour
{
    Camera _cam;

    private void Start()
    {
        _cam = Camera.main;
    }

    private void Update()
    {
        transform.LookAt(_cam.transform.position);
    }
}
