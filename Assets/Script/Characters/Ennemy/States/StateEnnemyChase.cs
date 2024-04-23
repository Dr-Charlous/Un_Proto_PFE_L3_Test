using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateEnnemyChase : IStateEnnemy
{
    float _time;

    public void OnEnter(EnnemyStateController controller)
    {
        _time = 0;
    }

    public void UpdateState(EnnemyStateController controller)
    {
        Debug.Log(this.ToString());

        RaycastHit hitMid;
        RaycastHit hitLeft;
        RaycastHit hitRight;
        bool RayMid = Physics.Raycast(controller.transform.position, controller.transform.TransformDirection(Vector3.forward), out hitMid, controller.DistanceSee);
        bool RayLeft = Physics.Raycast(controller.transform.position, controller.transform.TransformDirection(Vector3.forward + Vector3.left), out hitLeft, controller.DistanceSee);
        bool RayRight = Physics.Raycast(controller.transform.position, controller.transform.TransformDirection(Vector3.forward + Vector3.right), out hitRight, controller.DistanceSee);

        if (RayMid && (hitMid.transform.gameObject.GetComponent<StateBabyController>() != null || hitMid.transform.gameObject.GetComponent<CharaMove>() != null))
        {
            controller.Target = hitMid.transform.gameObject;
            _time = 0;
        }
        else if (RayLeft && (hitLeft.transform.gameObject.GetComponent<StateBabyController>() != null || hitLeft.transform.gameObject.GetComponent<CharaMove>() != null))
        {
            controller.Target = hitLeft.transform.gameObject;
            _time = 0;
        }
        else if (RayRight && (hitRight.transform.gameObject.GetComponent<StateBabyController>() != null || hitRight.transform.gameObject.GetComponent<CharaMove>() != null))
        {
            controller.Target = hitRight.transform.gameObject;
            _time = 0;
        }
        else
        {
            _time += Time.deltaTime;

            if (_time > controller.TimeSinceNoSee)
            {
                controller.isChasing = false;
                controller.Target = null;
            }
        }

        if (controller.Target != null)
            controller.Character.destination = controller.Target.transform.position;

        if (controller.JawsController.IsBitting)
            controller.Animations.AnimAttackBit();
        else
            controller.Animations.AnimAttack();
    }

    public void OnExit(EnnemyStateController controller)
    {

    }
}
