using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateEnnemyBranch : IStateEnnemy
{
    int _i = 0;

    public void OnEnter(EnnemyStateController controller)
    {
        for (int i = 0; i < controller.Resonance.Length; i++)
        {
            if (controller.Resonance[i].IsResonating)
            {
                if (controller.Fish == null || !controller.Fish.activeInHierarchy)
                {
                    controller.Move(controller.Resonance[i].transform.position);
                    _i = i;
                }
                break;
            }
        }

        controller.Animations.AnimAttack();
    }

    public void UpdateState(EnnemyStateController controller)
    {
        if ((controller.Resonance[_i].transform.position - controller.transform.position).magnitude < controller.DistanceNext)
        {
            controller.DOComplete();
        }
    }

    public void OnExit(EnnemyStateController controller)
    {
        
    }
}
