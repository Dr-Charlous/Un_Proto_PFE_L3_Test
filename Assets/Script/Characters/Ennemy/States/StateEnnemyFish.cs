using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateEnnemyFish : IStateEnnemy
{
    public void OnEnter(StateEnnemyController controller)
    {
        controller.Move(controller.Fish.transform.position);
        controller.IsEating = true;
    }

    public void UpdateState(StateEnnemyController controller)
    {
        controller.Animations.AnimAttack();
    }

    public void OnExit(StateEnnemyController controller)
    {
        
    }
}
