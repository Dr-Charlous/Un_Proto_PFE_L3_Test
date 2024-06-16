using UnityEngine;

public class DeathIsOnTheWay : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<CamController>() != null || other.GetComponent<RefBaby>() != null)
        {
            StartCoroutine(GameManager.Instance.Death.ActiveUI(true, true));

            GameManager.Instance.Character.Animator.SetTrigger("Death");
            GameManager.Instance.Character.IsParalysed = true;
        }
    }
}
