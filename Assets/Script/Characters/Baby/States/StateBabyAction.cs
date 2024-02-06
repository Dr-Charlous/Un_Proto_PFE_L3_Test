using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateBabyAction : IState
{
    public void OnEnter(StateBabyController controller)
    {

    }

    public void UpdateState(StateBabyController controller)
    {
        Debug.Log("Action");
    }

    public void OnExit(StateBabyController controller) 
    {
    
    }
}
