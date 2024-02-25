using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationManager : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] Rigidbody CharacterRb;

    private void Update()
    {
        animator.SetFloat("Move", CharacterRb.velocity.magnitude, 0.1f, Time.deltaTime);
    }
}
