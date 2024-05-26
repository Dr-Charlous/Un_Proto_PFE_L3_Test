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
                    controller.Move(controller.Resonance[i].transform.position - ((controller.Resonance[i].transform.position - controller.transform.position) / 10) * controller.Resonance[i].DistanceFromTrunk);
                    _i = i;
                }
                break;
            }
        }

        controller.JawsController.gameObject.SetActive(false);
        controller.Animations.AnimAttack();
        controller.JawsController.CanBite = false;
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
        controller.JawsController.gameObject.SetActive(true);
        controller.JawsController.CanBite = true;
    }
}
