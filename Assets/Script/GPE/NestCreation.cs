using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NestCreation : MonoBehaviour
{
    [SerializeField] Material _material;
    [SerializeField] Color _colorInitial;
    [SerializeField] Color _colorValid;

    [SerializeField] Vector3 _scalePunch;

    [SerializeField] GameObject[] _itemsToConstruct;
    [SerializeField] bool[] _itemsVerification;

    [SerializeField] StonePathFalling _stones;

    [SerializeField] GameObject _transition;
    [SerializeField] Cinematic _cine;

    [SerializeField] ScriptableDialogue _dialogueMiamiam;

    UiTextDialogueSpeaker _speaker;

    public Transform[] Entries;
    public bool IsCreated = false;
    public bool IsFeed = false;

    public GameObject Fish;

    private void Start()
    {
        _material.color = _colorInitial;
        _transition.SetActive(false);

        _speaker = GameManager.Instance.Character.GetComponentInChildren<UiTextDialogueSpeaker>();

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

        List<GameObject> baby = GameManager.Instance.BabyManager.BabiesInScene;

        for (int i = 0; i < baby.Count; i++)
        {
            baby[i].GetComponentInChildren<StateBabyController>().Charges += 10;
            baby[i].GetComponentInChildren<StateBabyController>().ScaleMesh(Vector3.one);
            baby[i].GetComponentInChildren<StateBabyController>().ChangeState(baby[i].GetComponentInChildren<StateBabyController>().StateFollow);
        }

        if (_speaker != null && _dialogueMiamiam != null)
            _speaker.StartDialogue(_dialogueMiamiam);
    }

    void VerificationItem(Collider other)
    {
        for (int i = 0; i < _itemsToConstruct.Length; i++)
        {
            if (_itemsToConstruct[i] == other.gameObject)
            {
                var obj = other.GetComponent<ObjectCollect>();

                if (obj.IsPortable)
                other.GetComponentInParent<RefBaby>().controller.isGrab = false;
                else
                {
                    List<GameObject> baby = GameManager.Instance.BabyManager.BabiesInScene;

                    for (int j = 0; j < baby.Count; j++)
                    {
                        baby[j].GetComponentInChildren<StateBabyController>().isGrab = false;
                    }
                }

                _itemsVerification[i] = true;
                other.gameObject.SetActive(false);

                VerificationArray();
                transform.DOPunchScale(_scalePunch, 1);
                return;
            }
        }
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

        List<GameObject> baby = GameManager.Instance.BabyManager.BabiesInScene;

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
        for (int i = 0; i < GameManager.Instance.BabyManager.BabiesInScene.Count; i++)
        {
            GameManager.Instance.BabyManager.BabiesInScene[i].GetComponentInChildren<StateBabyController>().IsParalysed = true;
            GameManager.Instance.BabyManager.BabiesInScene[i].GetComponentInChildren<StateBabyController>().Target = Entries[i];
            GameManager.Instance.BabyManager.BabiesInScene[i].GetComponentInChildren<StateBabyController>().Agent.SetDestination(GameManager.Instance.BabyManager.BabiesInScene[i].GetComponentInChildren<StateBabyController>().Target.position);
        }
    }
}
