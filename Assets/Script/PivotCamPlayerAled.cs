using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PivotCamPlayerAled : MonoBehaviour
{
    [SerializeField] Transform _pivotCam;
    [SerializeField] float _lerpDistance = 0.25f;
    [SerializeField] float _lerpRotate = 0.25f;

    private void Update()
    {
        transform.position = Vector3.Lerp(transform.position, _pivotCam.position, _lerpDistance);

        transform.LookAt(_pivotCam.position);
    }
}
