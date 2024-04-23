using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndWin : MonoBehaviour
{
    [SerializeField] GameObject WinUi;
    [SerializeField] Animator Animator;

    private void Start()
    {
        WinUi.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<CharaMove>() != null)
        {
            Animator.SetBool("End", true);
            WinUi.SetActive(true);
        }
    }
}
