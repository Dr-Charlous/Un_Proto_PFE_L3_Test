using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallGPEBabyPush : MonoBehaviour
{
    [SerializeField] Animator _animator;
    [SerializeField] ObjectResonnance _resonance;
    [SerializeField] BoxCollider _collider;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<RefBaby>() != null)
        {
            _animator.SetTrigger("Fall");
            _resonance.PlayerGetOutside();
            _collider.enabled = false;
        }
    }
}
