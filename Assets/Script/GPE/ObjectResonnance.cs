using UnityEngine;
using DG.Tweening;

public class ObjectResonnance : MonoBehaviour
{
    public Transform BabyTarget;
    public Transform BabyPos;
    public float SpeedBabyTarget = 5;
    public float DistanceFromTrunk = 3;
    public bool IsResonating = false;
    public bool IsPlayerInside;

    [SerializeField] AudioSource _source;
    [SerializeField] AudioClip _clip;
    [SerializeField] Transform _destinationCamera;
    [SerializeField] Transform[] _entries;
    [SerializeField] bool _isTraveling = false;
    [SerializeField] float _speed = 5;
    [SerializeField] float _speedCam;
    [SerializeField] UiFollowing _uiFlollowing;
    [SerializeField] OnTriggerEnterScript _babyZone;
    [SerializeField] GameObject _renderer;
    [SerializeField] BoxCollider _trunkCollider;

    Vector3 LastPosPlayer;
    Quaternion LastRotPlayer;

    private void Start()
    {
        IsPlayerInside = false;
        LastPosPlayer = Vector3.zero;
        LastRotPlayer = Quaternion.Euler(Vector3.zero);

        _renderer.SetActive(false);
    }

    private void Update()
    {
        if (IsPlayerInside)
        {
            GameManager.Instance.Character.IsParalysed = true;

            if (_source != null && _clip != null)
                PlaySound(_source, _clip);

            IsResonating = true;
            BabyOut();
        }
        else
        {
            IsResonating = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<CamController>() != null/* && IsPlayerInside == false*/)
        {
            PlayerGetInside();
        }
    }

    void PlaySound(AudioSource source, AudioClip clip)
    {
        if (source.isPlaying)
        {
            return;
        }
        else
        {
            source.clip = clip;
            source.Play();
        }
    }

    Vector3 NearestEntry(Vector3 lastPos)
    {
        Vector3 farAway = lastPos;
        float maxValue = 0;

        if (_entries.Length > 0)
        {
            for (int i = 0; i < _entries.Length; i++)
            {
                if (Vector3.Distance(lastPos, _entries[i].position) > maxValue)
                {
                    farAway = _entries[i].position;
                    maxValue = Vector3.Distance(lastPos, _entries[i].position);
                }
            }
        }

        return farAway;
    }

    void PlayerGetInside()
    {
        //Debug.Log("ObjectResonnance");

        GameManager.Instance.PlayerMeshFollow.Scream();

        if (_babyZone.ObjectLastExit != null)
            _babyZone.ObjectLastExit = null;

        GameManager.Instance.CamManager.ChangeCam(_destinationCamera, _speedCam);

        ChangePlayerPos();

        BabyTarget.position = BabyPos.position;
        BabyTarget.rotation = BabyPos.rotation;

        for (int i = 0; i < GameManager.Instance.BabyManager.BabiesInScene.Count; i++)
        {
            StateBabyController Baby = GameManager.Instance.BabyManager.BabiesInScene[0].GetComponentInChildren<StateBabyController>();

            if (!IsResonating)
            {
                Baby.ChangeState(Baby.StateAction);
                Baby.Target = BabyTarget;

                GameManager.Instance.BabyManager.ChangeOrder();
            }
        }

        GameManager.Instance.Character.TrapResonnance = this;
        IsPlayerInside = true;

        if (_uiFlollowing != null)
            _uiFlollowing.gameObject.SetActive(false);

        _renderer.SetActive(true);

        if (_trunkCollider != null)
            _trunkCollider.excludeLayers += LayerMask.GetMask("Player");
    }

    public void PlayerGetOutside()
    {
        //Debug.Log("ObjectResonnance");

        GameManager.Instance.PlayerMeshFollow.Scream();

        if (_babyZone.ObjectLastExit != null)
            _babyZone.ObjectLastExit = null;

        ChangePlayerPos();

        if (GameManager.Instance.CamManager.ActualPos != GameManager.Instance.CamPlayer)
            GameManager.Instance.CamManager.ChangeCam(GameManager.Instance.CamPlayer, _speedCam);

        for (int i = 0; i < GameManager.Instance.BabyManager.BabiesInScene.Count; i++)
        {
            StateBabyController Baby = GameManager.Instance.BabyManager.BabiesInScene[0].GetComponent<RefBaby>().Controller;

            Baby.ChangeState(Baby.StateFollow);

            GameManager.Instance.BabyManager.ChangeOrder();
        }
        GameManager.Instance.BabyManager.ChangeOrder();

        GameManager.Instance.Character.TrapResonnance = null;
        IsPlayerInside = false;

        if (_uiFlollowing != null)
            _uiFlollowing.gameObject.SetActive(true);

        _babyZone.ObjectLastExit = null;
        _renderer.SetActive(false);

        if (_trunkCollider != null)
            _trunkCollider.excludeLayers -= LayerMask.GetMask("Player");
    }

    public void ChangePlayerPos()
    {
        if (LastPosPlayer == Vector3.zero)
        {
            LastPosPlayer = GameManager.Instance.Character.transform.position;
            LastRotPlayer = GameManager.Instance.Character.transform.rotation;

            float speed = (transform.position - GameManager.Instance.Character.transform.position).magnitude * _speed * Time.deltaTime;

            GameManager.Instance.Character.Rb.velocity = Vector3.zero;
            GameManager.Instance.Character.transform.DOKill();
            GameManager.Instance.Character.transform.DOMove(transform.position, speed);
        }
        else
        {
            float speed = (transform.position - GameManager.Instance.Character.transform.position).magnitude * _speed * Time.deltaTime;

            if (_isTraveling)
            {
                GameManager.Instance.Character.transform.DOKill();
                GameManager.Instance.Character.transform.DOMove(NearestEntry(LastPosPlayer), speed);
            }
            else
            {
                GameManager.Instance.Character.transform.DOKill();
                GameManager.Instance.Character.transform.DOMove(LastPosPlayer, speed);
            }

            GameManager.Instance.Character.Rb.velocity = Vector3.zero;
            GameManager.Instance.Character.transform.rotation = LastRotPlayer;

            LastPosPlayer = Vector3.zero;
            LastRotPlayer = Quaternion.Euler(Vector3.zero);
        }

        GameManager.Instance.Character.Rb.velocity = Vector3.zero;
        //GameManager.Instance.CamManager.Reset();
    }

    void BabyOut()
    {
        if (_babyZone.ObjectLastExit != null && _babyZone.ObjectLastExit.GetComponent<RefBaby>() != null)
        {
            PlayerGetOutside();
            _babyZone.ObjectLastExit = null;
        }
    }
}
