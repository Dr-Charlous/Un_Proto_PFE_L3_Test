using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PivotCamPlayerAled : MonoBehaviour
{
    [SerializeField] Transform _pivotCam;

    private void Update()
    {
        transform.position = _pivotCam.position;
    }
}
