using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateBabyAction : IState
{
    Vector3 _target;
    public void OnEnter(StateBabyController controller)
    {
        controller.transform.position = controller.transform.position;
        controller.Target = controller.transform;
        controller.Agent.SetDestination(controller.Target.position);

        _target = controller.Target.position;
    }

    public void UpdateState(StateBabyController controller)
    {
        //Debug.Log("CheckForAction");

        if (controller.Target == null)
        {
            controller.Target = controller.transform.parent.transform;
            controller.ChangeState(controller.StateFollow);
        }

        if (_target != controller.Target.position)
        {
            _target = controller.Target.position;
            controller.Agent.SetDestination(_target);
        }

        if (GameManager.Instance.Nest != null && GameManager.Instance.Nest.IsCreated && !GameManager.Instance.Nest.IsFeed)
        {
            controller.Target = GameManager.Instance.Nest.transform;
        }
    }

    public void OnExit(StateBabyController controller)
    {
        controller.Target = controller.TargetParent;
        controller.Agent.SetDestination(controller.Target.position);
    }
}
