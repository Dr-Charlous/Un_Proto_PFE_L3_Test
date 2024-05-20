using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationManager : MonoBehaviour
{
    [SerializeField] Animator _animator;
    [SerializeField] StateBabyController _babyController;

    private void Update()
    {
        _animator.SetFloat("Move", _babyController.Agent.velocity.magnitude, 0.1f, Time.deltaTime);
    }

    public void CallTrigerAnim(string name)
    {
        _animator.SetTrigger(name);
    }
}
