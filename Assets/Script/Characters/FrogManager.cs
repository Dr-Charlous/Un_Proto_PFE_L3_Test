using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrogManager : MonoBehaviour
{
    [Header("Sounds :")]
    [SerializeField] AudioSource _audioSource;
    [SerializeField] AudioClip _clip;

    [Header("Anims :")]
    [SerializeField] Animator _animator;
    [SerializeField] bool _isRoll;
    [SerializeField] bool _isSpeaking;
    [SerializeField] bool _isRandom;

    private void Start()
    {
        if (_isRoll)
            _animator.SetBool("Roll", true);
        if (_isSpeaking)
            _animator.SetBool("Speak", true);
        if (_isRandom)
            StartCoroutine(LaunchAnim());
    }

    IEnumerator LaunchAnim()
    {
        int rnd = Random.Range(0, 3);

        if (rnd == 0)
        {
            _animator.SetBool("Roll", true);
            _animator.SetBool("Speak", false);
        }
        if (rnd == 1)
        {
            _animator.SetBool("Speak", true);
            _animator.SetBool("Roll", false);
        }
        if (rnd == 2)
        {
            _animator.SetBool("Roll", false);
            _animator.SetBool("Speak", false);
        }

        yield return new WaitForSeconds(Random.Range(2, 8));
        StartCoroutine(LaunchAnim());
    }
}
