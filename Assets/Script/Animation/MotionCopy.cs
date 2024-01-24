using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MotionCopy : MonoBehaviour
{
    public Transform targetLimb;
    ConfigurableJoint joint;

    private void Start()
    {
        joint = GetComponent<ConfigurableJoint>();
    }

    private void Update()
    {
        joint.targetRotation = targetLimb.rotation;
    }
}
