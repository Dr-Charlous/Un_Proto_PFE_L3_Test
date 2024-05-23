using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathIsOnTheWay : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<CharaMove>() != null)
        {
            GameManager.Instance.Death.ActiveUI();

            GameManager.Instance.Character.Animator.SetTrigger("Death");
            GameManager.Instance.Character.IsParalysed = true;
        }
    }
}
