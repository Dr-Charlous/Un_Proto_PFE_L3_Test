using DG.Tweening;
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
        if (other.GetComponent<CharaMove>() != null)
        {
            Vector3 destinantion = other.transform.position;

            if (Vector3.Distance(other.transform.position, _entry1.position) > Vector3.Distance(other.transform.position, _entry2.position))
            {
                destinantion = _entry1.position;
            }
            else
            {
                destinantion = _entry2.position;
            }

            other.transform.DOMove(destinantion, _time * Time.deltaTime, false);
        }
    }
}
