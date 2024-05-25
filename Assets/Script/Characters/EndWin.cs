using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndWin : MonoBehaviour
{
    [SerializeField] string _nameScene;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<CharaMove>() != null)
        {
            GameManager.Instance.Win.ActiveUI();

            GameManager.Instance.Character.Animator.SetTrigger(_nameScene);
            GameManager.Instance.Character.IsParalysed = true;
        }
    }
}
