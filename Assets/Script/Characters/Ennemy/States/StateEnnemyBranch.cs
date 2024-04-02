using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateEnnemyBranch : IStateEnnemy
{
    public void OnEnter(EnnemyStateController controller)
    {
        for (int i = 0; i < controller.Resonance.Length; i++)
        {
            if (controller.Resonance[i].IsResonating)
            {
                if (controller.Fish == null || !controller.Fish.activeInHierarchy)
                {
                    controller.Move(controller.Resonance[i].transform.position);
                }
                break;
            }
        }
    }

    public void UpdateState(EnnemyStateController controller)
    {
        //Debug.Log("Stay");
    }

    public void OnExit(EnnemyStateController controller)
    {
        
    }
}
