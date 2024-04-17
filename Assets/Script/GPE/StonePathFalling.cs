using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StonePathFalling : MonoBehaviour
{
    [SerializeField] Animator _animator;

    public void Fall()
    {
        _animator.SetBool("Fall", true);
    }
}
