using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NestCreation : MonoBehaviour
{
    [SerializeField] Material _material;
    [SerializeField] Color _colorInitial;
    [SerializeField] Color _colorValid;
    public GameObject[] Items;
    public bool[] ItemsVerification;

    private void Start()
    {
        _material.color = _colorInitial;
    }

    private void OnTriggerStay(Collider other)
    {
        VerificationItem(other, true);
        VerificationArray();
    }

    private void OnTriggerExit(Collider other)
    {
        VerificationItem(other, false);
    }

    void VerificationItem(Collider other, bool prensent)
    {
        for (int i = 0; i < Items.Length; i++)
        {
            if (Items[i] == other.gameObject)
            {
                ItemsVerification[i] = prensent;
                return;
            }
        }
    }

    void VerificationArray()
    {
        bool isEveryOne = true;

        for(int i = 0;i < Items.Length;i++)
        {
            if (ItemsVerification[i] == false)
            {
                isEveryOne = false;
            }
        }

        if (isEveryOne)
        {
            _material.color = _colorValid;

            for (int i = 0; i < Items.Length; i++)
            {
                Items[i].SetActive(false);
            }
        }
    }
}
