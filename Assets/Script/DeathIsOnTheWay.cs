using UnityEngine;

public class DeathIsOnTheWay : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<CamController>() != null || other.GetComponent<RefBaby>() != null)
        {
            StartCoroutine(GameManager.Instance.Death.ActiveUI(true, true));

            if (GameManager.Instance.EndPoursuite.Death != null)
                Destroy(GameManager.Instance.EndPoursuite.Death);

            GameManager.Instance.Character.Animator.SetTrigger("Death");
            GameManager.Instance.Character.IsParalysed = true;
        }
    }
}
