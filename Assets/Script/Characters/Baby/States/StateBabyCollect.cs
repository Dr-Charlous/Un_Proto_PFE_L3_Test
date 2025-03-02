using System.Numerics;

public class StateBabyCollect : IState
{
    public void OnEnter(StateBabyController controller)
    {
        if (GameManager.Instance.Nest == null || !GameManager.Instance.Nest.IsCreated || (GameManager.Instance.Nest.IsCreated && GameManager.Instance.Nest.IsFeed))
        {
            controller.ParentObject = controller.TargetObject.transform.parent;
            controller.Target = controller.TargetObject.transform;
            controller.Agent.SetDestination(controller.Target.position);
        }
        else
            controller.ChangeState(controller.StateFollow);
    }

    public void UpdateState(StateBabyController controller)
    {
        if (controller.OnTriggerEnterScript != null
            && controller.OnTriggerEnterScript.isTrigger
            && controller.OnTriggerEnterScript.ObjectTouch != null
            && controller.OnTriggerEnterScript.ObjectTouch.GetComponent<ObjectCollect>() != null
            && controller.OnTriggerEnterScript.ObjectTouch == controller.TargetObject
            && controller.isTransporting == false)
        {
            controller.GetObj(controller.OnTriggerEnterScript.ObjectTouch.GetComponent<ObjectCollect>().IsPortable);
            controller.Scream();
            controller.ChangeState(controller.StateFollow);
        }
    }

    public void OnExit(StateBabyController controller)
    {
        controller.isGoingToGrab = false;

        if (controller.ObjectTransporting == null)
        {
            controller.isTransporting = false;
            controller.TargetObject.GetComponent<ObjectCollect>().UiFollow.ShowUi(true);
        }
    }
}
