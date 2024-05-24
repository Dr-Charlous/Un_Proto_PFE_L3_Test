using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class Cinematic : MonoBehaviour
{
    [SerializeField] Transform _camPos;
    [SerializeField] ScriptableDialogue _dialogueNidBuild;
    [SerializeField] float _timeDelay;

    public IEnumerator Cinematic1()
    {
        GameManager.Instance.Character.IsParalysed = true;
        GameManager.Instance.CamManager.TemporaryPos = _camPos;

        yield return new WaitForSeconds(GameManager.Instance.CamManager.Speed * Vector3.Distance(Camera.main.transform.position, _camPos.position) - _timeDelay);

        if (_dialogueNidBuild != null)
            GameManager.Instance.Speaker.StartDialogue(_dialogueNidBuild);

        GameManager.Instance.CamManager.TemporaryPos = null;
        GameManager.Instance.Character.IsParalysed = false;
    }
}
