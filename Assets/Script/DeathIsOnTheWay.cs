using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathIsOnTheWay : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
     {
        var prout = other;

        if (other.GetComponent<CharaMove>() != null || other.GetComponent<RefBaby>() != null)
        {
            StartCoroutine(GameManager.Instance.Death.ActiveUI(true));

            GameManager.Instance.Character.Animator.SetTrigger("Death");
            GameManager.Instance.Character.IsParalysed = true;
        }
    }
}
