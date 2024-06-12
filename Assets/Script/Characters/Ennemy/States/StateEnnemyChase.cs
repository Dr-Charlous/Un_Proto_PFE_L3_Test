using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateEnnemyChase : IStateEnnemy
{
    public void OnEnter(StateEnnemyController controller)
    {
        controller.TimeChase = 0;
    }

    public void UpdateState(StateEnnemyController controller)
    {
        Debug.Log(this.ToString());

        RaycastHit hitMid;
        RaycastHit hitLeft;
        RaycastHit hitRight;
        bool RayMid = Physics.Raycast(controller.Animations.transform.position, controller.transform.TransformDirection(Vector3.forward), out hitMid, controller.DistanceSee);
        bool RayLeft = Physics.Raycast(controller.Animations.transform.position, controller.transform.TransformDirection(Vector3.forward + Vector3.left), out hitLeft, controller.DistanceSee);
        bool RayRight = Physics.Raycast(controller.Animations.transform.position, controller.transform.TransformDirection(Vector3.forward + Vector3.right), out hitRight, controller.DistanceSee);

        if (RayMid && (hitMid.transform.gameObject.GetComponent<RefBaby>() != null || hitMid.transform.gameObject.GetComponent<CamController>() != null) && controller.DistanceSee > Vector3.Distance(hitMid.transform.position, controller.transform.position))
        {
            controller.Target = hitMid.transform.gameObject;
            controller.TimeChase = 0;
        }
        else if (RayLeft && (hitLeft.transform.gameObject.GetComponent<RefBaby>() != null || hitLeft.transform.gameObject.GetComponent<CamController>() != null) && controller.DistanceSee > Vector3.Distance(hitLeft.transform.position, controller.transform.position))
        {
            controller.Target = hitLeft.transform.gameObject;
            controller.TimeChase = 0;
        }
        else if (RayRight && (hitRight.transform.gameObject.GetComponent<RefBaby>() != null || hitRight.transform.gameObject.GetComponent<CamController>() != null) && controller.DistanceSee > Vector3.Distance(hitRight.transform.position, controller.transform.position))
        {
            controller.Target = hitRight.transform.gameObject;
            controller.TimeChase = 0;
        }
        else
        {
            controller.TimeChase += Time.deltaTime;

            if (controller.TimeChase > controller.TimeSinceNoSee)
            {
                controller.IsChasing = false;
                controller.Target = null;
            }
        }

        if (controller.Target != null)
        {
            controller.Character.destination = controller.Target.transform.position;
            controller.Move(controller.Target.transform.position);
        }

        if (controller.JawsController.IsBitting)
            controller.Animations.AnimAttackBit();
        else
            controller.Animations.AnimAttack();
    }

    public void OnExit(StateEnnemyController controller)
    {

    }
}
