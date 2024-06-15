using UnityEngine;

public class Jaws : MonoBehaviour
{
    public bool CanBite;
    public bool IsBitting = false;

    [SerializeField] StateEnnemyController _ennemyStateController;
    [SerializeField] float _time;

    private void OnTriggerStay(Collider other)
    {
        if (other.transform.gameObject.GetComponent<RefBaby>() != null || other.GetComponent<CamController>() != null && CanBite)
        {
            IsBitting = true;
            StartCoroutine(GameManager.Instance.Death.ActiveUI(true, false));

            CamController move = other.transform.gameObject.GetComponent<CamController>();
            if (move != null)
                move.Animator.SetTrigger("Death");

            StateBabyController stateBabyController = other.GetComponent<RefBaby>().Controller;
            if (stateBabyController != null)
                stateBabyController.Animator.SetTrigger("Death");
        }
    }
}
