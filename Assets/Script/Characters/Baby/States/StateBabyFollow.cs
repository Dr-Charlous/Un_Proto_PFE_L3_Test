using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class StateBabyFollow : IState
{
    public void OnEnter(StateBabyController controller)
    {
        //controller.transform.position = controller.TargetParent.position;
        //controller.Target = controller.TargetParent;
        //controller.Agent.SetDestination(controller.Target.position);
    }

    public void UpdateState(StateBabyController controller)
    {
        //Debug.Log("Follow");

        if (controller.Nest == null || !controller.Nest.IsCreated || (controller.Nest.IsCreated && controller.Nest.IsFeed))
        {
            controller.Target = controller.TargetParent;
            controller.Agent.SetDestination(controller.Target.position);
        }
    }

    public void OnExit(StateBabyController controller)
    {

    }
}
