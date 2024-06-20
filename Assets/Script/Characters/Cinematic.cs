using System.Collections;
using UnityEngine;

public class Cinematic : MonoBehaviour
{
    [SerializeField] Transform[] _camPos;
    [SerializeField] ScriptableDialogue _dialogueNidBuild;
    [SerializeField] EndPousuite _endPoursuite;
    [SerializeField] float _value;
    [SerializeField] float _value1;

    public IEnumerator Cinematic1()
    {
        GameManager.Instance.Character.IsParalysed = true;

        GameManager.Instance.CamManager.ChangeCam(_camPos[0], _value, true);

        //float speed = (GameManager.Instance.CamManager.Speed * 100 + _value) / Vector3.Distance(Camera.main.transform.position, _camPos[i].position);

        yield return new WaitForSeconds(_value);

        if (_camPos.Length > 1)
        {
            GameManager.Instance.CamManager.ChangeCam(_camPos[1], _value, true);

            if (_endPoursuite != null)
                _endPoursuite.Begin(GameManager.Instance.Character);

            yield return new WaitForSeconds(_value1);


            if (_dialogueNidBuild != null)
                GameManager.Instance.Speaker.StartDialogue(_dialogueNidBuild);

            GameManager.Instance.CamManager.ChangeCam(GameManager.Instance.CamPlayer, _value1, false);
            GameManager.Instance.Character.IsParalysed = false;
        }
        else
        {
            GameManager.Instance.CamManager.ChangeCam(GameManager.Instance.CamPlayer, _value, false);
        }
    }
}
