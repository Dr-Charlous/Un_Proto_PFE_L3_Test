using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;
using UnityEngine.UIElements;

public class Cinematic : MonoBehaviour
{
    [SerializeField] Transform _camPos;
    [SerializeField] ScriptableDialogue _dialogueNidBuild;
    [SerializeField] float _value;

    public IEnumerator Cinematic1()
    {
        GameManager.Instance.Character.IsParalysed = true;
        GameManager.Instance.CamManager.TemporaryPos = _camPos;

        float speed = (GameManager.Instance.CamManager.Speed * 100 + _value) / Vector3.Distance(Camera.main.transform.position, _camPos.position);

        yield return new WaitForSeconds(speed);

        if (_dialogueNidBuild != null)
            GameManager.Instance.Speaker.StartDialogue(_dialogueNidBuild);

        GameManager.Instance.CamManager.TemporaryPos = null;
        GameManager.Instance.Character.IsParalysed = false;
    }
}
