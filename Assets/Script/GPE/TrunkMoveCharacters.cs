using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrunkMoveCharacters : MonoBehaviour
{
    [SerializeField] Transform _entry1;
    [SerializeField] Transform _entry2;
    [SerializeField] float _time;

    private void OnTriggerEnter(Collider other)
    {
        CamController chara = other.GetComponent<CamController>();
        StateBabyController baby = other.GetComponent<StateBabyController>();

        if (chara != null || baby != null)
        {
            if (chara != null)
                chara.IsParalysed = true;
            if (baby != null)
                baby.IsParalysed = true;

            Vector3 destinantion = other.transform.position;

            if (Vector3.Distance(other.transform.position, _entry1.position) > Vector3.Distance(other.transform.position, _entry2.position))
            {
                destinantion = _entry1.position;
            }
            else
            {
                destinantion = _entry2.position;
            }

            other.transform.DOMove(destinantion, _time * Time.deltaTime);

            StartCoroutine(WaitToDeparalysed(other, _time * Time.deltaTime));
        }
    }

    IEnumerator WaitToDeparalysed(Collider other, float time)
    {
        yield return new WaitForSeconds(time);

        CamController chara = other.GetComponent<CamController>();
        StateBabyController baby = other.GetComponent<StateBabyController>();

        if (chara != null)
            chara.IsParalysed = false;
        if (baby != null)
            baby.IsParalysed = false;
    }
}
