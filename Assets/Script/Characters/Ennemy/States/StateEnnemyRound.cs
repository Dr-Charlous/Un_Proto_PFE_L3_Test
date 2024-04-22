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

        RaycastHit hitMid;
        RaycastHit hitLeft;
        RaycastHit hitRight;

        bool RayMid = Physics.Raycast(controller.transform.position, controller.transform.TransformDirection(Vector3.forward), out hitMid, controller.DistanceSee);
        bool RayLeft = Physics.Raycast(controller.transform.position, controller.transform.TransformDirection(Vector3.forward + Vector3.left), out hitLeft, controller.DistanceSee);
        bool RayRight = Physics.Raycast(controller.transform.position, controller.transform.TransformDirection(Vector3.forward + Vector3.right), out hitRight, controller.DistanceSee);

        if (RayMid)
        {
            if (hitMid.transform.gameObject.tag == controller.BabiesTag || hitMid.transform.gameObject.tag == controller.ParentTag)
            {
                Debug.Log("Mid");
                controller.isChasing = true;
            }
        }

        if (RayLeft)
        {
            if (hitLeft.transform.gameObject.tag == controller.BabiesTag || hitLeft.transform.gameObject.tag == controller.ParentTag)
            {
                Debug.Log("Left");
                controller.isChasing = true;
            }
        }

        if (RayRight)
        {
            if (hitRight.transform.gameObject.tag == controller.BabiesTag || hitRight.transform.gameObject.tag == controller.ParentTag)
            {
                Debug.Log("Right");
                controller.isChasing = true;
            }
        }

        controller.Animations.AnimSwim();
    }

    public void OnExit(EnnemyStateController controller)
    {

    }
}
