using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectResonnance : MonoBehaviour
{
    [SerializeField] AudioSource _source;
    [SerializeField] AudioClip _clip;

    private void OnTriggerEnter(Collider other)
    {
        StateBabyController babyController = other.transform.parent.GetComponentInChildren<StateBabyController>();

        if (babyController != null && babyController.currentState == babyController.StateAction)
        {
            babyController.IsParalysed = true;
            babyController.gameObject.transform.position = transform.position;
            PlaySound(_source, _clip);
        }
    }

    void PlaySound(AudioSource source, AudioClip clip)
    {
        if (source.isPlaying)
        {
            Debug.Log("AudioSource is playing");
            return;
        }
        else
        {
            Debug.Log("AudioSource play");
            source.clip = clip;
            source.Play();
        }
    }
}
