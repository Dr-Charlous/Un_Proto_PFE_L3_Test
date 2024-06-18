using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class StateBabyFollow : IState
{
    public void OnEnter(StateBabyController controller)
    {
        //Controller.transform.position = Controller.TargetParent.position;
        //Controller.Target = Controller.TargetParent;
        //Controller.Agent.SetDestination(Controller.Target.position);
        controller.Target = controller.TargetParent;
        controller.Agent.SetDestination(controller.Target.position);
    }

    public void UpdateState(StateBabyController controller)
    {
        //Debug.Log("Follow");

        if (GameManager.Instance.Nest == null || !GameManager.Instance.Nest.IsCreated || (GameManager.Instance.Nest.IsCreated && GameManager.Instance.Nest.IsFeed))
        {
            if (Vector3.Distance(controller.Target.position, controller.transform.position) > controller.DistanceUpdateMove)
                controller.Agent.SetDestination(controller.Target.position);
        }

        if (controller.isGoingToGrab)
            controller.isGoingToGrab = false;

        if (Random.Range(0, 500) == 0)
            controller.Scream();
    }

    public void OnExit(StateBabyController controller)
    {

    }
}
