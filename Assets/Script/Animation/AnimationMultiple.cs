using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class AnimationMultiple : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] GameObject[] _babies;

    private void Update()
    {
        if ((_babies[0].transform.position - _babies[2].transform.position).magnitude < 1)
        {
            //_babies[0].transform.parent.gameObject.SetActive(false);
            //_babies[2].transform.parent.gameObject.SetActive(false);
            PlayAnim("Interactions");
        }

        //if (Animator.)
        //{

        //}
    }

    void PlayAnim(string name)
    {
        animator.Play(name);
    }
}
