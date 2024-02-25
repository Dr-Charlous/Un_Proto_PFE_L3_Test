using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NestCreation : MonoBehaviour
{
    [SerializeField] Material _material;
    [SerializeField] Color _colorInitial;
    [SerializeField] Color _colorValid;


    [SerializeField] GameObject[] ItemsToConstruct;
    [SerializeField] bool[] ItemsVerification;

    public bool IsCreated = false;
    public bool IsFeed = false;

    public GameObject Fish;

    private void Start()
    {
        _material.color = _colorInitial;
    }

    private void OnTriggerEnter(Collider other)
    {
        VerificationItem(other);

        if (other.gameObject == Fish && IsCreated == true)
            CreateNest();
    }

    void CreateNest()
    {
        IsFeed = true;
        Fish.SetActive(false);
    }

    void VerificationItem(Collider other)
    {
        for (int i = 0; i < ItemsToConstruct.Length; i++)
        {
            if (ItemsToConstruct[i] == other.gameObject)
            {
                ItemsVerification[i] = true;
                other.gameObject.SetActive(false);
                VerificationArray();
                return;
            }
        }
    }

    void VerificationArray()
    {
        bool isEveryOne = true;

        for (int i = 0; i < ItemsToConstruct.Length; i++)
        {
            if (ItemsVerification[i] == false)
            {
                isEveryOne = false;
            }
        }

        if (isEveryOne)
        {
            _material.color = _colorValid;
            IsCreated = true;
        }
    }
}
