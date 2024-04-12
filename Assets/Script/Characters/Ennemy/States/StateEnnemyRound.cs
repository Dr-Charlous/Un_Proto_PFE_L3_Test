using DG.Tweening;
using UnityEngine;

public class StateEnnemyRound : IStateEnnemy
{
    public void OnEnter(EnnemyStateController controller)
    {
        controller.Move(new Vector3(controller.RoundPositions[controller._i].position.x, controller.transform.position.y, controller.RoundPositions[controller._i].position.z));
    }

    public void UpdateState(EnnemyStateController controller)
    {
        if (controller.Character.velocity.magnitude < 1 && (new Vector3(controller.transform.position.x, 0, controller.transform.position.z) - new Vector3(controller.RoundPositions[controller._i].position.x, 0, controller.RoundPositions[controller._i].position.z)).magnitude < controller.DistanceNext)
        {
            if (controller._i + 1 < controller.RoundPositions.Length)
                controller._i += 1;
            else
                controller._i = 0;

            controller.Move(new Vector3(controller.RoundPositions[controller._i].position.x, controller.transform.position.y, controller.RoundPositions[controller._i].position.z));
        }

        RaycastHit hit;

        if (Physics.Raycast(controller.transform.position, controller.transform.TransformDirection(Vector3.forward), out hit, controller.DistanceSee))
        {
            if (hit.transform.gameObject.tag == controller.BabiesTag || hit.transform.gameObject.tag == controller.ParentTag)
            {
                controller.isChasing = true;
            }
        }
    }

    public void OnExit(EnnemyStateController controller)
    {

    }
}
