using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NestCreation : MonoBehaviour
{
    [SerializeField] Material _material;
    [SerializeField] Color _colorInitial;
    [SerializeField] Color _colorValid;


    [SerializeField] GameObject[] _itemsToConstruct;
    [SerializeField] bool[] _itemsVerification;

    [SerializeField] CharaMove _character;
    [SerializeField] BabyManager _babyManager;
    [SerializeField] StonePathFalling _stones;

    [SerializeField] GameObject _transition;
    [SerializeField] Cinematic _cine;

    public Transform[] Entries;
    public bool IsCreated = false;
    public bool IsFeed = false;

    public GameObject Fish;

    private void Start()
    {
        _material.color = _colorInitial;
        _transition.SetActive(false);

        _itemsVerification = new bool[_itemsToConstruct.Length];
    }

    private void OnTriggerEnter(Collider other)
    {
        VerificationItem(other);

        if (other.gameObject == Fish && IsCreated == true)
        {
            FeedBabies();
        }
    }

    void FeedBabies()
    {
        IsFeed = true;
        Fish.SetActive(false);
        _transition.SetActive(true);

        List<GameObject> baby = _character.BabyManager.BabiesInScene;

        for (int i = 0; i < baby.Count; i++)
        {
            baby[i].GetComponentInChildren<StateBabyController>().Charges += 10;
        }
    }

    void VerificationItem(Collider other)
    {
        for (int i = 0; i < _itemsToConstruct.Length; i++)
        {
            if (_itemsToConstruct[i] == other.gameObject)
            {
                _itemsVerification[i] = true;
                other.gameObject.SetActive(false);

                VerificationArray();
                return;
            }
        }

        //List<GameObject> baby = _character.BabyManager.BabiesInScene;
        //for (int i = 0; i < baby.Count; i++)
        //{
        //    baby[i].GetComponentInChildren<StateBabyController>().ChangeState(baby[i].GetComponentInChildren<StateBabyController>().StateFollow);
        //}
    }

    void VerificationArray()
    {
        bool isEveryOne = true;

        for (int i = 0; i < _itemsToConstruct.Length; i++)
        {
            if (_itemsVerification[i] == false)
            {
                isEveryOne = false;
            }
        }

        List<GameObject> baby = _character.BabyManager.BabiesInScene;

        for (int j = 0; j < baby.Count; j++)
        {
            baby[j].GetComponentInChildren<StateBabyController>().ChangeState(baby[j].GetComponentInChildren<StateBabyController>().StateFollow);
        }

        if (isEveryOne)
        {
            _material.color = _colorValid;

            IsCreated = true;

            _stones.Fall();

            StartCoroutine(_cine.Cinematic1());

            BabyGoToNest();
        }
    }

    public void BabyGoToNest()
    {
        for (int i = 0; i < _babyManager.BabiesInScene.Count; i++)
        {
            _babyManager.BabiesInScene[i].GetComponentInChildren<StateBabyController>().IsParalysed = true;
            _babyManager.BabiesInScene[i].GetComponentInChildren<StateBabyController>().Target = Entries[i];
            _babyManager.BabiesInScene[i].GetComponentInChildren<StateBabyController>().Agent.SetDestination(_babyManager.BabiesInScene[i].GetComponentInChildren<StateBabyController>().Target.position);
        }
    }
}
