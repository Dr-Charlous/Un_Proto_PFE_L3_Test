using System.Collections;
using UnityEngine;

public class Cinematic : MonoBehaviour
{
    [SerializeField] Transform[] _camPos;
    [SerializeField] ScriptableDialogue _dialogueNidBuild;
    [SerializeField] EndPousuite _endPoursuite;
    [SerializeField] float _value;

    public IEnumerator Cinematic1()
    {
        GameManager.Instance.Character.IsParalysed = true;

        GameManager.Instance.CamManager.ChangeCam(_camPos[0]);

        //float speed = (GameManager.Instance.CamManager.Speed * 100 + _value) / Vector3.Distance(Camera.main.transform.position, _camPos[i].position);

        Debug.Log(_value * Time.deltaTime);
        yield return new WaitForSeconds(_value * Time.deltaTime);

        if (_camPos.Length > 1)
        {
            GameManager.Instance.CamManager.ChangeCam(_camPos[1]);

            if (_endPoursuite != null)
                _endPoursuite.Begin(GameManager.Instance.Character);

            StartCoroutine(Cinematic2());
        }
        else
        {
            GameManager.Instance.CamManager.ChangeCam(null);
        }
    }

    IEnumerator Cinematic2()
    {
        //if (_endPoursuite != null)
        //    _endPoursuite.Begin(GameManager.Instance.Character);

        //float speed = (GameManager.Instance.CamManager.Speed * 100 + _value) / Vector3.Distance(Camera.main.transform.position, _camPos[i].position);

        yield return new WaitForSeconds(_value * Time.deltaTime);


        if (_dialogueNidBuild != null)
            GameManager.Instance.Speaker.StartDialogue(_dialogueNidBuild);

        GameManager.Instance.CamManager.ChangeCam(null);
        GameManager.Instance.Character.IsParalysed = false;
    }
}
