using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MotionCopy : MonoBehaviour
{
    public Transform targetLimb;
    public ConfigurableJoint joint;
    [SerializeField] bool isZInvert = false;

    private void Start()
    {
        joint = GetComponent<ConfigurableJoint>();
    }

    private void Update()
    {
        if (isZInvert)
            joint.targetRotation = Quaternion.Euler(new Vector3(targetLimb.localRotation.eulerAngles.x, targetLimb.localRotation.eulerAngles.y, targetLimb.localRotation.eulerAngles.z + 180));
        else
            joint.targetRotation = Quaternion.Euler(new Vector3(targetLimb.localRotation.eulerAngles.x, targetLimb.localRotation.eulerAngles.y, targetLimb.localRotation.eulerAngles.z));
    }
}
