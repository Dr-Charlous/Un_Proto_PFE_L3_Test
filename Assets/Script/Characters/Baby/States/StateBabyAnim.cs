using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateBabyAnim : MonoBehaviour
{
    public void OnEnter(StateBabyController controller)
    {
        //controller.Target = controller.AnimMultiple.transform.position;
        //controller.Agent.SetDestination(controller.Target);
    }

    public void UpdateState(StateBabyController controller)
    {
        //Debug.Log("Anim");
    }

    public void OnExit(StateBabyController controller)
    {

    }
}
