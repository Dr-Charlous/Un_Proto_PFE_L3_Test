using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateBabyStay : IState
{
    public void OnEnter(StateBabyController controller)
    {

    }

    public void UpdateState(StateBabyController controller)
    {
        Debug.Log("Stay");
    }

    public void OnExit(StateBabyController controller)
    {

    }
}
