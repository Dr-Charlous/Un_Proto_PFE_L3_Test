using UnityEngine;

public class DeathTrap : MonoBehaviour
{
    [SerializeField] Animator _animator;

    private void OnTriggerEnter(Collider other)
    {
        CamController cam = other.GetComponent<CamController>();

        if (cam != null)
        {
            _animator.SetTrigger("Attack");
            gameObject.GetComponent<BoxCollider>().enabled = false;
        }
    }

    public void Init()
    {
        gameObject.GetComponent<BoxCollider>().enabled = true;
        _animator.SetTrigger("Return");
    }
}
