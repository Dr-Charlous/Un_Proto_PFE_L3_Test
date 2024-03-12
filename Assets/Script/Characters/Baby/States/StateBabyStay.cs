using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateBabyStay : IState
{
    public void OnEnter(StateBabyController controller)
    {
        controller.Agent.SetDestination(controller.transform.position);
    }

    public void UpdateState(StateBabyController controller)
    {
        //Debug.Log("Stay");

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
