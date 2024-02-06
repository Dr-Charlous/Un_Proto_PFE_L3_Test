using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class StateBabyFollow : IState
{
    public void OnEnter(StateBabyController controller)
    {

    }

    public void UpdateState(StateBabyController controller)
    {
        Debug.Log("Follow");

        if (controller.Nest.isCreated && controller.Nest.isFeed)
        {
            if (Vector3.Distance(controller.Parent.position, controller.Target) > controller.Distance)
            {
                controller.Target = controller.Parent.position;
                controller.Agent.SetDestination(controller.Target);
            }
        }
        else
        {
            if (Vector3.Distance(controller.Parent.position, controller.Target) > controller.Distance)
            {
                controller.Target = controller.Nest.transform.position;
                controller.Agent.SetDestination(controller.Target);
            }
        }
    }

    public void OnExit(StateBabyController controller)
    {

    }
}
