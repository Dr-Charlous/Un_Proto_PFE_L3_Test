using System.Collections;
using System.Collections.Generic;
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
            CharaMove chara = BeginCollider.ObjectTouch.GetComponent<CharaMove>();

            if (chara != null)
            {
                BeginCollider.gameObject.SetActive(false);
                StartCoroutine(WaitBegin());
                isBeginActif = false;
            }
        }

        if (EndCollider.ObjectTouch != null && isEndActif)
        {
            CharaMove chara1 = EndCollider.ObjectTouch.GetComponent<CharaMove>();

            if (chara1 != null)
            {
                EndCollider.gameObject.SetActive(false);
                EndStone.SetActive(true);
                Destroy(Death);
                isEndActif = false;
            }
        }

        IEnumerator WaitBegin()
        {
            GameManager.Instance.CamManager.TemporaryPos = CamTransform;

            yield return new WaitForSeconds(GameManager.Instance.CamManager.Speed * Vector3.Distance(Camera.main.transform.position, CamTransform.position) + TimeWait);

            GameManager.Instance.CamManager.TemporaryPos = null;

            Death = Instantiate(DeathPrefab);
            BeginStone.SetActive(true);
        }
    }
}
