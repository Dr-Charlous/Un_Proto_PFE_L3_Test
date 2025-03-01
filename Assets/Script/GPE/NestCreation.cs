using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NestCreation : MonoBehaviour
{
    [SerializeField] Vector3 _scalePunch;

    public GameObject[] ItemsToConstruct;
    [SerializeField] bool[] _itemsVerification;

    [Header("Cinematic aspect & more : ")]
    [SerializeField] StonePathFalling _stones;
    [SerializeField] GameObject _transition;
    [SerializeField] Cinematic _cine;

    [SerializeField] ScriptableDialogue _dialogueMiamiam;
    [SerializeField] UiFollowing _uiFollow;

    [SerializeField] GameObject _objBlocking;
    [SerializeField] string _scene;

    int _value = 0;

    public Transform[] Entries;
    public bool IsCreated = false;
    public bool IsFeed = false;

    public GameObject Fish;

    bool _isActionned = false;
    bool _changeScene = false;

    private void Start()
    {
        _transition.SetActive(false);

        _itemsVerification = new bool[ItemsToConstruct.Length];

        _uiFollow.UpdateText($"{_value} / {ItemsToConstruct.Length}");

        _isActionned = false;
        _changeScene = false;
    }

    private void Update()
    {
        if (IsFeed && IsCreated && !_isActionned)
        {
            _isActionned = true;
            FeedBabies();
        }

        if (_value >= ItemsToConstruct.Length)
            _uiFollow.ShowUi(false);

        if (_changeScene)
            GameManager.Instance.Music.KillMusic();
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
        GameManager.Instance.PlayerMeshFollow.Scream();

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
        {
            //_objBlocking.SetActive(false);
            StartCoroutine(ChangeScene());
        }
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
                    other.GetComponentInParent<RefBaby>().Controller.ObjectTransporting = null;
                }
                else
                {
                    List<GameObject> baby = GameManager.Instance.BabyManager.BabiesInScene;

                    for (int j = 0; j < baby.Count; j++)
                    {
                        baby[j].GetComponentInChildren<RefBaby>().Controller.isGoingToGrab = false;

                        if (baby[j].GetComponentInParent<RefBaby>().Controller.ObjectTransporting == null)
                            baby[j].GetComponentInChildren<RefBaby>().Controller.isTransporting = false;
                    }
                }

                _itemsVerification[i] = true;
                other.gameObject.SetActive(false);

                VerificationArray();
                transform.DOPunchScale(_scalePunch, 1);
                _value++;
                _uiFollow.UpdateText($"{_value} / {ItemsToConstruct.Length}");

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
            for (int j = 0; j < baby.Count; j++)
            {
                baby[j].GetComponentInChildren<RefBaby>().Controller.isGoingToGrab = false;
                baby[j].GetComponentInChildren<RefBaby>().Controller.isTransporting = false;
                baby[j].GetComponentInParent<RefBaby>().Controller.ObjectTransporting = null;
            }

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

        GameManager.Instance.PlayerMeshFollow.Scream();
    }

    public IEnumerator ChangeScene()
    {
        _changeScene = true;
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene(_scene);
    }
}
