using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateBabyCollect : MonoBehaviour
{
    public void OnEnter(StateBabyController controller)
    {
        controller.ParentObject = controller.TargetObject.GetComponentInParent<Transform>();
        controller.Target = controller.TargetObject.transform.position;
        controller.Agent.SetDestination(controller.Target);
    }

    public void UpdateState(StateBabyController controller)
    {
        if (Vector3.Distance(controller.TargetObject.transform.position, controller.Target) > controller.Distance)
        {
            controller.TargetObject.transform.SetParent(controller.ParentCollect);
            controller.isTransporting = true;
        }
    }

    public void OnExit(StateBabyController controller)
    {

    }
}
