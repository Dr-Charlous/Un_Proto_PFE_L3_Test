using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnnemyAnimations : MonoBehaviour
{
    [SerializeField] Animator _animator;

    public void AnimAttackBit()
    {
        _animator.SetFloat("Blend", 1);
    }

    public void AnimAttack()
    {
        _animator.SetFloat("Blend", 0.5f);
    }

    public void AnimSwim()
    {
        _animator.SetFloat("Blend", 0);
    }
}
