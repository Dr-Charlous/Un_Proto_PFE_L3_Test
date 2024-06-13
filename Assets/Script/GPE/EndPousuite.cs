using System.Collections;
using UnityEngine;

public class EndPousuite : MonoBehaviour
{
    public OnTriggerEnterScript BeginCollider;
    public OnTriggerEnterScript EndCollider;
    public GameObject DeathPrefab;
    public GameObject Death;

    public GameObject BeginStone;
    public GameObject EndStone;

    public Transform CamTransform;
    public float TimeWait;

    bool isBeginActif;
    bool isEndActif;

    private void Start()
    {
        isBeginActif = true;
        isEndActif = true;
    }

    private void Update()
    {
        if (BeginCollider.ObjectTouch != null && isBeginActif)
        {
            Begin(BeginCollider.ObjectTouch.GetComponent<CamController>());
        }

        if (EndCollider.ObjectTouch != null && isEndActif)
        {
            if (EndCollider.ObjectTouch.GetComponent<CamController>() != null)
                End(EndCollider.ObjectTouch.GetComponent<CamController>());
            else if (EndCollider.ObjectTouch.GetComponent<RefBaby>() != null)
                End(GameManager.Instance.Character);
        }
    }

    public void Begin(CamController chara)
    {
        if (chara != null)
        {
            BeginCollider.gameObject.SetActive(false);
            StartCoroutine(WaitBegin());
            isBeginActif = false;
        }
    }

    void End(CamController chara)
    {
        if (chara != null)
        {
            EndCollider.gameObject.SetActive(false);
            EndStone.SetActive(true);
            Destroy(Death);
            isEndActif = false;
        }
    }

    public void Initialize()
    {
        isBeginActif = true;
        isEndActif = true;
        BeginCollider.gameObject.SetActive(true);
        EndCollider.gameObject.SetActive(true);
        if (BeginStone != null)
            BeginStone.SetActive(false);
        EndStone.SetActive(false);

        if (Death != null)
            Destroy(Death);
        if (GameManager.Instance.CamManager.TemporaryPos != null)
            GameManager.Instance.CamManager.ChangeCam(null);

        if (BeginStone == null)
        {
            Begin(GameManager.Instance.Character);
            GameManager.Instance.Begin.SetBool("End", false);
        }
    }

    IEnumerator WaitBegin()
    {
        if (BeginStone != null)
            GameManager.Instance.CamManager.ChangeCam(CamTransform);

        yield return new WaitForSeconds(TimeWait * Time.deltaTime);

        GameManager.Instance.CamManager.ChangeCam(null);

        if (Death != null)
            Destroy(Death);

        Death = Instantiate(DeathPrefab);

        if (BeginStone != null)
            BeginStone.SetActive(true);
    }
}
