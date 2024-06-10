using UnityEngine;

public class Jaws : MonoBehaviour
{
    public bool CanBite;
    public bool IsBitting = false;

    [SerializeField] StateEnnemyController _ennemyStateController;
    [SerializeField] float _time;

    private void OnTriggerStay(Collider other)
    {
        if (other.transform.gameObject.GetComponent<RefBaby>() != null || other.GetComponent<CharaMove>() != null && CanBite)
        {
            IsBitting = true;
            StartCoroutine(GameManager.Instance.Death.ActiveUI(true, false));

            CharaMove move = other.transform.gameObject.GetComponent<CharaMove>();
            if (move != null)
                move.Animator.SetTrigger("Death");

            StateBabyController stateBabyController = other.transform.gameObject.GetComponent<RefBaby>().Controller;
            if (stateBabyController != null)
                stateBabyController.Animator.SetTrigger("Death");
        }
    }
}
