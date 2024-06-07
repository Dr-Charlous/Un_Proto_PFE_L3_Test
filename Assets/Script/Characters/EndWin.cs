using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndWin : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<CharaMove>() != null)
        {
            StartCoroutine(GameManager.Instance.Win.ActiveUI(false, false));

            GameManager.Instance.Character.IsParalysed = true;
        }
    }
}
