using UnityEngine;

public class ObjectToPush : MonoBehaviour
{
    [Header("Check :")]
    public ObjectToPush[] CheckBlockers;
    public BabyPosCheckAction[] CheckBabies;
    public TriggerIsParetnHere CheckParent;

    [Header("Verif :")]
    [SerializeField] Transform _destination;
    [SerializeField] GameObject _mesh;
    [HideInInspector] public bool[] IsBabyActionned;
    [HideInInspector] public bool IsParentActionned;

    [Header("Push :")]
    public float ValuePush;
    public int DecreasePushValue = 1;
    [SerializeField] float _speedPush;

    [Header("Ui & Sound:")]
    public UiFollowing _uiFollow;
    public AudioSource _source;

    Transform _parent;
    Vector3 _initPos;
    Vector3 _endPos;
    Quaternion _initRot;
    Quaternion _endRot;

    private void Start()
    {
        _initPos = transform.position;
        _endPos = _destination.position;
        _initRot = transform.rotation;
        _endRot = _destination.rotation;

        IsBabyActionned = new bool[CheckBabies.Length];
    }

    private void Update()
    {
        for (int i = 0; i < CheckBabies.Length; i++)
        {
            if (CheckBabies[i].IsBabyActionned)
            {
                IsBabyActionned[i] = true;
            }
            else
                IsBabyActionned[i] = false;
        }

        CheckForAction();

        _parent = transform.parent;

        if (ValuePush >= 1)
        {
            for (int i = 0; i < GameManager.Instance.BabyManager.BabiesInScene.Count; i++)
            {
                GameManager.Instance.BabyManager.BabiesInScene[i].GetComponentInChildren<StateBabyController>().Charges--;
            }

            transform.position = _endPos;
            _mesh.transform.parent = _parent;
            Destroy(this.gameObject);
        }
    }

    void CheckForAction()
    {
        bool isEveryOneHere = true;

        if (CheckParent != null && (!CheckParent.isTrigger || GameManager.Instance.Character.Position >= 0))
        {
            isEveryOneHere = false;
            IsParentActionned = false;

            _uiFollow._objUiGamePad1.SetActive(false);
            _uiFollow._objUiKeyboard1.SetActive(false);
            _uiFollow._objUiGamePad2.SetActive(true);
            _uiFollow._objUiKeyboard2.SetActive(true);
        }
        else if (CheckParent != null)
            IsParentActionned = true;

        for (int i = 0; i < IsBabyActionned.Length; i++)
        {
            if (IsBabyActionned[i] == false || (IsBabyActionned[i] && GameManager.Instance.BabyManager.BabiesInScene[i].GetComponentInChildren<StateBabyController>().Charges < DecreasePushValue))
            {
                isEveryOneHere = false;
                _uiFollow._objUiGamePad1.SetActive(true);
                _uiFollow._objUiKeyboard1.SetActive(true);
                _uiFollow._objUiGamePad2.SetActive(false);
                _uiFollow._objUiKeyboard2.SetActive(false);
            }
        }

        if (CheckBlockers.Length > 0)
        {
            for (int i = 0; i < CheckBlockers.Length; i++)
            {
                if (CheckBlockers[i].ValuePush < DecreasePushValue)
                    isEveryOneHere = false;
            }
        }

        if (isEveryOneHere)
        {
            Action();
            SoundCall(true);

            _uiFollow._objUiGamePad1.SetActive(false);
            _uiFollow._objUiKeyboard1.SetActive(false);
            _uiFollow._objUiGamePad2.SetActive(false);
            _uiFollow._objUiKeyboard2.SetActive(false);
        }
        else
        {
            _uiFollow.gameObject.SetActive(true);
            SoundCall(false);
        }
    }

    void Action()
    {
        if (GameManager.Instance.BabyManager.BabiesInScene[0].GetComponentInChildren<StateBabyController>().Charges >= DecreasePushValue || CheckParent == null)
        {
            if (ValuePush < 1)
                ValuePush += _speedPush * Time.deltaTime;

            transform.position = Vector3.Lerp(_initPos, _endPos, ValuePush);
            transform.rotation = Quaternion.Lerp(_initRot, _endRot, ValuePush);

            _uiFollow.gameObject.SetActive(false);
        }
    }

    void SoundCall(bool isActive)
    {
        if (isActive)
        {
            if (!_source.isPlaying)
            {
                _source.Play();
            }

            if (_source.time >= _source.clip.length - 0.1f)
            {
                _source.pitch = Random.Range(1, 1.6f);
                _source.time = 0;
            }

            if (_source.pitch == 0)
                Random.Range(1, 2.1f);
        }
        else
            _source.Pause();
    }
}
