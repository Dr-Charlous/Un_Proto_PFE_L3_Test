using UnityEngine;

public class Jaws : MonoBehaviour
{
    public bool CanBite;
    [SerializeField] EnnemyStateController _ennemyStateController;
    [SerializeField] float _time;
    public bool IsBitting = false;

    private void OnTriggerStay(Collider other)
    {
        if (other.transform.gameObject.GetComponent<StateBabyController>() != null || other.GetComponent<CharaMove>() != null && CanBite)
        {
            IsBitting = true;
            StartCoroutine(GameManager.Instance.Death.ActiveUI(true));

            CharaMove move = other.transform.gameObject.GetComponent<CharaMove>();
            if (move != null)
                move.Animator.SetTrigger("Death");

            StateBabyController stateBabyController = other.transform.gameObject.GetComponent<StateBabyController>();
            if (stateBabyController != null)
                stateBabyController.Animator.SetTrigger("Death");
        }
    }
}
