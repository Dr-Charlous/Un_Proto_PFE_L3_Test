using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateEnnemyChase : IStateEnnemy
{
    public void OnEnter(EnnemyStateController controller)
    {

    }

    public void UpdateState(EnnemyStateController controller)
    {
        //Debug.Log("Stay");
    }

    public void OnExit(EnnemyStateController controller)
    {

    }
}
