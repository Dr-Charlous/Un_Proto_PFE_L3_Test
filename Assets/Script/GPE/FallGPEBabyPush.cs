using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallGPEBabyPush : MonoBehaviour
{
    [SerializeField] Animator _animator;
    [SerializeField] ObjectResonnance _resonance;
    [SerializeField] BoxCollider _collider;
    [SerializeField] Transform _camPos;
    [SerializeField] float _cineTime;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<RefBaby>() != null)
        {
            Fall();
        }
    }

    public void Fall()
    {
        _animator.SetTrigger("Fall");
        _resonance.PlayerGetOutside();
        _collider.enabled = false;
        StartCoroutine(CineFall());
    }

    IEnumerator CineFall()
    {
        GameManager.Instance.CamManager.ChangeCam(_camPos);
        yield return new WaitForSeconds(_cineTime * Time.deltaTime);
        GameManager.Instance.CamManager.ChangeCam(null);
    }
}
