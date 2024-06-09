using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;

public class NestCreation : MonoBehaviour
{
    [SerializeField] Material _material;
    [SerializeField] Color _colorInitial;
    [SerializeField] Color _colorValid;

    [SerializeField] Vector3 _scalePunch;

    public GameObject[] ItemsToConstruct;
    [SerializeField] bool[] _itemsVerification;

    [SerializeField] StonePathFalling _stones;

    [SerializeField] GameObject _transition;
    [SerializeField] Cinematic _cine;

    [SerializeField] ScriptableDialogue _dialogueMiamiam;
    [SerializeField] UiFollowing _uiFollow;

    [SerializeField] GameObject _objBlocking;

    int _value = 0;

    public Transform[] Entries;
    public bool IsCreated = false;
    public bool IsFeed = false;

    public GameObject Fish;

    bool _isActionned = false;

    private void Start()
    {
        _material.color = _colorInitial;
        _transition.SetActive(false);

        _itemsVerification = new bool[ItemsToConstruct.Length];

        _uiFollow.UpdateText($"{_value} / {ItemsToConstruct.Length}");

        _isActionned = false;
    }

    private void Update()
    {
        if (IsFeed && IsCreated && !_isActionned)
        {
            _isActionned = true;
            FeedBabies();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        VerificationItem(other);

        if (other.gameObject == Fish)
        {
            IsFeed = true;
        }
    }

     public void FeedBabies()
    {
        if (Fish != null)
        {
            Fish.SetActive(false);
            _transition.SetActive(true);
        }

        List<GameObject> baby = GameManager.Instance.BabyManager.BabiesInScene;

        for (int i = 0; i < baby.Count; i++)
        {
            baby[i].GetComponentInChildren<StateBabyController>().Charges += 10;
            baby[i].GetComponentInChildren<StateBabyController>().ScaleMesh(Vector3.one);
            baby[i].GetComponentInChildren<StateBabyController>().ChangeState(baby[i].GetComponentInChildren<StateBabyController>().StateFollow);
        }

        if (_dialogueMiamiam != null)
            GameManager.Instance.Speaker.StartDialogue(_dialogueMiamiam);

        if (_objBlocking != null && _objBlocking.activeInHierarchy)
            _objBlocking.SetActive(false);
    }

    void VerificationItem(Collider other)
    {
        for (int i = 0; i < ItemsToConstruct.Length; i++)
        {
            if (ItemsToConstruct[i] == other.gameObject)
            {
                var obj = other.GetComponent<ObjectCollect>();

                if (obj != null && obj.IsPortable)
                {
                    other.GetComponentInParent<RefBaby>().Controller.isGoingToGrab = false;
                    other.GetComponentInParent<RefBaby>().Controller.isTransporting = false;
                }
                else
                {
                    List<GameObject> baby = GameManager.Instance.BabyManager.BabiesInScene;

                    for (int j = 0; j < baby.Count; j++)
                    {
                        baby[j].GetComponentInChildren<StateBabyController>().isGoingToGrab = false;
                        baby[j].GetComponentInChildren<StateBabyController>().isTransporting = false;
                    }
                }

                _itemsVerification[i] = true;
                other.gameObject.SetActive(false);

                VerificationArray();
                transform.DOPunchScale(_scalePunch, 1);
                _value++;

                if (_value < ItemsToConstruct.Length && !_uiFollow.gameObject.activeInHierarchy)
                    _uiFollow.gameObject.SetActive(true);

                if (_value < ItemsToConstruct.Length)
                    _uiFollow.UpdateText($"{_value} / {ItemsToConstruct.Length}");
                else if (_value >= ItemsToConstruct.Length)
                    _uiFollow.gameObject.SetActive(false);

                return;
            }
        }
    }

    void VerificationArray()
    {
        bool isEveryOne = true;

        for (int i = 0; i < ItemsToConstruct.Length; i++)
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
