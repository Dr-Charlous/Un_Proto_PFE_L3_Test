using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;

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

    Vector3 LastPosPlayer;

    private void Start()
    {
        IsPlayerInside = false;
        LastPosPlayer = Vector3.zero;
    }

    private void Update()
    {
        if (IsPlayerInside)
        {
            PlaySound(_source, _clip);
            IsResonating = true;
        }
        else
        {
            IsResonating = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<CharaMove>() != null && IsPlayerInside == false)
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
        Vector3 farAway = Vector3.zero;
        float maxValue = 0;

        for (int i = 0; i < _entries.Length; i++)
        {
            if (Vector3.Distance(lastPos, _entries[i].position) > maxValue)
            {
                farAway = _entries[i].position;
                maxValue = Vector3.Distance(lastPos, _entries[i].position);
            }
        }

        return farAway;
    }

    void PlayerGetInside()
    {
        //Debug.Log("ObjectResonnance");

        GameManager.Instance.CamManager.TemporaryPos = _destinationCamera;

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
    }

    public void PlayerGetOutside()
    {
        //Debug.Log("ObjectResonnance");

        GameManager.Instance.CamManager.TemporaryPos = null;

        ChangePlayerPos();

        for (int i = 0; i < GameManager.Instance.BabyManager.BabiesInScene.Count; i++)
        {
            StateBabyController Baby = GameManager.Instance.BabyManager.BabiesInScene[0].GetComponentInChildren<StateBabyController>();

            if (!IsResonating)
            {
                Baby.ChangeState(Baby.StateFollow);

                GameManager.Instance.BabyManager.ChangeOrder();
            }
        }

        GameManager.Instance.Character.TrapResonnance = null;
        IsPlayerInside = false;
    }

    public void ChangePlayerPos()
    {
        if (LastPosPlayer == Vector3.zero)
        {
            LastPosPlayer = GameManager.Instance.Character.transform.position;

            float speed = (transform.position - GameManager.Instance.Character.transform.position).magnitude * _speed * Time.deltaTime;

            GameManager.Instance.Character.transform.DOMove(transform.position, speed);
        }
        else
        {
            float speed = (transform.position - GameManager.Instance.Character.transform.position).magnitude * _speed * Time.deltaTime;

            if (_isTraveling)
                GameManager.Instance.Character.transform.DOMove(NearestEntry(LastPosPlayer), speed);
            else
                GameManager.Instance.Character.transform.DOMove(LastPosPlayer, speed);

            GameManager.Instance.Character._rb.velocity = Vector3.zero;

            LastPosPlayer = Vector3.zero;
        }
    }
}
