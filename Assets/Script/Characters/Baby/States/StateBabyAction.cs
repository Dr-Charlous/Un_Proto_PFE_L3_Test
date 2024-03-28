using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateBabyAction : IState
{
    public void OnEnter(StateBabyController controller)
    {
        controller.transform.position = controller.transform.position;
        controller.Target = controller.transform.position;
        controller.Agent.SetDestination(controller.Target);
    }

    public void UpdateState(StateBabyController controller)
    {
        //Debug.Log("CheckForAction");

        if (controller.Nest != null && controller.Nest.IsCreated && !controller.Nest.IsFeed)
        {
            if (Vector3.Distance(controller.Nest.transform.position, controller.Target) > controller.Distance)
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
