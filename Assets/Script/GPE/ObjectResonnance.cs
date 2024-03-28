using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectResonnance : MonoBehaviour
{
    public bool IsResonating = false;

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
            IsResonating = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        StateBabyController babyController = other.transform.parent.GetComponentInChildren<StateBabyController>();

        if (babyController != null)
        {
            _source.Stop();
            IsResonating = false;
        }
    }

    void PlaySound(AudioSource source, AudioClip clip)
    {
        if (source.isPlaying)
        {
            return;
        }
        else
        {
            source.clip = clip;
            source.Play();
        }
    }
}
