using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomStartAnim : MonoBehaviour
{
    Animator animator;
    float number;
    float time;

    private void Start()
    {
        time = 0;
        animator = GetComponent<Animator>();
        number = Random.Range(0, 10);
    }

    private void Update()
    {
        time += Time.deltaTime;

        if (time > number)
        {
            animator.SetTrigger("Decal");
            Destroy(this);
        }
    }
}
