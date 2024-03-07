using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateBabyAction : IState
{
    public void OnEnter(StateBabyController controller)
    {

    }

    public void UpdateState(StateBabyController controller)
    {
        //Debug.Log("Action");

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
