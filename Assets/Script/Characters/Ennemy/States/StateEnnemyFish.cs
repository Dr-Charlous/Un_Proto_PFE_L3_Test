using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateEnnemyFish : IStateEnnemy
{
    public void OnEnter(EnnemyStateController controller)
    {
        controller.Move(controller.Fish.transform.position);
        controller.isEating = true;
    }

    public void UpdateState(EnnemyStateController controller)
    {
        
    }

    public void OnExit(EnnemyStateController controller)
    {
        
    }
}
