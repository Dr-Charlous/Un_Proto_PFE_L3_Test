using UnityEngine;

public class EndWin : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<CamController>() != null)
        {
            StartCoroutine(GameManager.Instance.Win.ActiveUI(false, false));

            GameManager.Instance.Character.IsParalysed = true;
        }
    }
}
