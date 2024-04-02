using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateEnnemyFish : IStateEnnemy
{
    public void OnEnter(EnnemyStateController controller)
    {
        controller.Move(controller.Fish.transform.position);
    }

    public void UpdateState(EnnemyStateController controller)
    {
        //Debug.Log("Stay");
    }

    public void OnExit(EnnemyStateController controller)
    {
        
    }
}
