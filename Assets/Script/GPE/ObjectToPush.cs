using UnityEngine;

public class ObjectToPush : MonoBehaviour
{
    public ObjectToPush[] CheckBlockers;
    public BabyPosCheckAction[] CheckBabies;
    public TriggerIsParetnHere CheckParent;

    [SerializeField] bool[] _isBabyActionned;
    [SerializeField] Transform _destination;
    [SerializeField] GameObject _mesh;

    public float ValuePush;
    [SerializeField] float _speedPush;
    [SerializeField] int _decreasePushValue = 1;

    public UiFollowing _uiFollow;

    Transform _parent;
    bool _isActivated = false;
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
    }

    private void Update()
    {
        for (int i = 0; i < CheckBabies.Length; i++)
        {
            if (CheckBabies[i].IsBabyActionned)
            {
                _isBabyActionned[i] = true;
            }
            else
                _isBabyActionned[i] = false;
        }

        CheckForAction();

        _parent = transform.parent;

        if (ValuePush >= 1)
        {
            for (int i = 0; i < GameManager.Instance.BabyManager.BabiesInScene.Count; i++)
            {
                GameManager.Instance.BabyManager.BabiesInScene[i].GetComponentInChildren<StateBabyController>().Charges--;
            }

            _mesh.transform.parent = _parent;
            Destroy(this.gameObject);
        }
    }

    void CheckForAction()
    {
        bool isEveryOneHere = true;

        for (int i = 0; i < _isBabyActionned.Length; i++)
        {
            if (_isBabyActionned[i] == false || (_isBabyActionned[i] && GameManager.Instance.BabyManager.BabiesInScene[i].GetComponentInChildren<StateBabyController>().Charges < _decreasePushValue))
                isEveryOneHere = false;
        }

        if (CheckParent != null && (!CheckParent.isTrigger || GameManager.Instance.Character.Position >= 0))
            isEveryOneHere = false;

        if (CheckBlockers.Length > 0)
        {
            for (int i = 0; i < CheckBlockers.Length; i++)
            {
                if (CheckBlockers[i].ValuePush < 1)
                    isEveryOneHere = false;
            }
        }

        if (isEveryOneHere)
        {
            Action();
        }
        else
            _uiFollow.gameObject.SetActive(true);
    }

    void Action()
    {
        if (GameManager.Instance.BabyManager.BabiesInScene[0].GetComponentInChildren<StateBabyController>().Charges >= _decreasePushValue || CheckParent == null)
        {
            if (ValuePush < 1)
                ValuePush += _speedPush * Time.deltaTime;

            transform.position = Vector3.Lerp(_initPos, _endPos, ValuePush);
            transform.rotation = Quaternion.Lerp(_initRot, _endRot, ValuePush);

            _uiFollow.gameObject.SetActive(false);
        }
    }
}
