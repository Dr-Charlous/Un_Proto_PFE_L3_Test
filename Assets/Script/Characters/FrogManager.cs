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

    [Header("Values :")]
    [SerializeField][Range(2, 10)] int _randomRatio = 4;
    [SerializeField][Tooltip("X = TimeMin / Y = TimeMax")] Vector2Int _timeBetween = new Vector2Int(2, 8);

    private void Start()
    {
        if (_isRoll)
            _animator.SetBool("Roll", true);
        if (_isSpeaking)
        {
            _animator.SetBool("Speak", true);

            _audioSource.Play();
            _audioSource.loop = true;
        }
        if (_isRandom)
            StartCoroutine(LaunchAnim());
    }

    IEnumerator LaunchAnim()
    {
        int rnd = Random.Range(0, _randomRatio);

        if (rnd == 0)
        {
            _animator.SetBool("Roll", true);
            _animator.SetBool("Speak", false);
            _audioSource.loop = false;
        }
        else if (rnd == 1)
        {
            _animator.SetBool("Roll", false);
            _animator.SetBool("Speak", true);

            if (!_audioSource.isPlaying)
                _audioSource.Play();
            _audioSource.loop = true;
        }
        else
        {
            _animator.SetBool("Roll", false);
            _animator.SetBool("Speak", false);
            _audioSource.loop = false;
        }

        yield return new WaitForSeconds(Random.Range(_timeBetween.x, _timeBetween.y));
        StartCoroutine(LaunchAnim());
    }
}
