using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateBabyCollect : IState
{
    public void OnEnter(StateBabyController controller)
    {
        if (controller.Nest == null || !controller.Nest.IsCreated || (controller.Nest.IsCreated && controller.Nest.IsFeed))
        {
            controller.ParentObject = controller.TargetObject.transform.parent;
            controller.Target = controller.TargetObject.transform.position;
            controller.Agent.SetDestination(controller.Target);
        }
        else
            controller.ChangeState(controller.StateFollow);
    }

    public void UpdateState(StateBabyController controller)
    {
        if (controller.OnTriggerEnterScript.isTrigger && controller.OnTriggerEnterScript.ObjectTouch.GetComponent<ObjectCollect>() != null && controller.isTransporting == false)
        {
            controller.TargetObject.transform.SetParent(controller.ParentCollect);
            controller.TargetObject.GetComponent<BoxCollider>().enabled = false;
            controller.isTransporting = true;
        }
    }

    public void OnExit(StateBabyController controller)
    {

    }
}
