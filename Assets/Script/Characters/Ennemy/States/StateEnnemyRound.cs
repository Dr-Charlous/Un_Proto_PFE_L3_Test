using DG.Tweening;
using UnityEngine;

public class StateEnnemyRound : IStateEnnemy
{
    public void OnEnter(StateEnnemyController controller)
    {
        controller.Move(new Vector3(controller.RoundPositions[controller.Iteration].position.x, controller.transform.position.y, controller.RoundPositions[controller.Iteration].position.z));
    }

    public void UpdateState(StateEnnemyController controller)
    {
        if (controller.Character.velocity.magnitude < 1 && (new Vector3(controller.transform.position.x, 0, controller.transform.position.z) - new Vector3(controller.RoundPositions[controller.Iteration].position.x, 0, controller.RoundPositions[controller.Iteration].position.z)).magnitude < controller.DistanceNext)
        {
            if (controller.Iteration + 1 < controller.RoundPositions.Length)
                controller.Iteration += 1;
            else
                controller.Iteration = 0;

            controller.Move(new Vector3(controller.RoundPositions[controller.Iteration].position.x, controller.transform.position.y, controller.RoundPositions[controller.Iteration].position.z));

            if (!controller.JawsController.CanBite)
                controller.JawsController.CanBite = true;
        }

        if (controller.JawsController.CanBite)
        {
            RaycastHit hitMid;
            RaycastHit hitLeft;
            RaycastHit hitRight;

            bool RayMid = Physics.Raycast(controller.Animations.transform.position, controller.Animations.transform.forward, out hitMid, controller.DistanceSee);
            bool RayLeft = Physics.Raycast(controller.Animations.transform.position, (controller.Animations.transform.forward - controller.Animations.transform.right).normalized, out hitLeft, controller.DistanceSee);
            bool RayRight = Physics.Raycast(controller.Animations.transform.position, (controller.Animations.transform.forward + controller.Animations.transform.right).normalized, out hitRight, controller.DistanceSee);

            if (RayMid && controller.DistanceSee > Vector3.Distance(hitMid.transform.position, controller.transform.position))
            {
                if (hitMid.transform.gameObject.GetComponent<RefBaby>() != null || hitMid.transform.gameObject.GetComponent<CamController>() != null)
                {
                    Debug.Log("Mid");
                    controller.IsChasing = true;
                }
            }

            if (RayLeft && controller.DistanceSee > Vector3.Distance(hitLeft.transform.position, controller.transform.position))
            {
                if (hitLeft.transform.gameObject.GetComponent<RefBaby>() != null || hitLeft.transform.gameObject.GetComponent<CamController>() != null)
                {
                    Debug.Log("Left");
                    controller.IsChasing = true;
                }
            }

            if (RayRight && controller.DistanceSee > Vector3.Distance(hitRight.transform.position, controller.transform.position))
            {
                if (hitRight.transform.gameObject.GetComponent<RefBaby>() != null || hitRight.transform.gameObject.GetComponent<CamController>() != null)
                {
                    Debug.Log("Right");
                    controller.IsChasing = true;
                }
            }
        }

        controller.Animations.AnimSwim();
    }

    public void OnExit(StateEnnemyController controller)
    {

    }
}
