using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Unity.VisualScripting.Member;

public class FallGPEBabyPush : MonoBehaviour
{
    [SerializeField] Animator _animator;
    [SerializeField] AudioSource _source;
    [SerializeField] ObjectResonnance _resonance;
    [SerializeField] BoxCollider _collider;
    [SerializeField] Transform _camPos;
    [SerializeField] float _value;
    [SerializeField] bool _isActivated;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<RefBaby>() != null && !_isActivated)
        {
            Fall();

            GameManager.Instance.PlayerMeshFollow.Scream();
        }
    }

    public void Fall()
    {
        _source.Play();
        _isActivated = true;
        _animator.SetTrigger("Fall");
        _resonance.PlayerGetOutside();
        _collider.enabled = false;
        StartCoroutine(CineFall());
    }

    IEnumerator CineFall()
    {
        GameManager.Instance.CamManager.ChangeCam(_camPos, _value, true);
        yield return new WaitForSeconds(_value);
        GameManager.Instance.CamManager.ChangeCam(GameManager.Instance.CamPlayer, _value, false);
    }
}
