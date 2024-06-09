using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateBabyAnim : MonoBehaviour
{
    public void OnEnter(StateBabyController controller)
    {
        //Controller.Target = Controller.AnimMultiple.transform.position;
        //Controller.Agent.SetDestination(Controller.Target);
    }

    public void UpdateState(StateBabyController controller)
    {
        //Debug.Log("Anim");
    }

    public void OnExit(StateBabyController controller)
    {

    }
}
