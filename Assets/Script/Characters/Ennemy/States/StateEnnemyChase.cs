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

        RaycastHit hit;
        
        if (Physics.Raycast(controller.transform.position, controller.transform.TransformDirection(Vector3.forward), out hit, controller.DistanceSee) && (hit.transform.gameObject.tag == controller.BabiesTag || hit.transform.gameObject.tag == controller.ParentTag))
        {
            controller.Character.destination = hit.transform.position;
            _time = 0;
        }
        else
        {
            _time += Time.deltaTime;

            if (_time > controller.TimeSinceNoSee)
            {
                controller.isChasing = false;
            }
        }
    }

    public void OnExit(EnnemyStateController controller)
    {

    }
}
